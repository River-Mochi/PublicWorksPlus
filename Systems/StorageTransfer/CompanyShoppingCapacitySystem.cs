// File: Systems/StorageTransfer/CompanyShoppingCapacitySystem.cs
// Purpose: Promote industrial/company shopping requests toward one full truck load.
// Notes:
// - Runs after BuyingCompanySystem creates ResourceBuyer.
// - Runs before ResourceBuyerSystem turns ResourceBuyer into TripNeeded.
// - Targets company buyers only (entities with BuyingCompany).
// - Uses current truck capacities from VehicleCapacitySystem.
// - Caps promoted request size to a safe selected truck capacity so live buying trucks
//   do not exceed the actual prefab cap after loading.

namespace PublicWorksPlus
{
    using Game;
    using Game.Citizens;
    using Game.Common;
    using Game.Companies;
    using Game.Economy;
    using Game.Prefabs;
    using Game.Tools;
    using Game.Vehicles;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;

    public sealed partial class CompanyShoppingCapacitySystem : GameSystemBase
    {
        private PrefabSystem m_PrefabSystem = null!;
        private ResourceSystem m_ResourceSystem = null!;
        private VehicleCapacitySystem m_VehicleCapacitySystem = null!;
        private EntityQuery m_BuyerQuery;

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            return 1;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_ResourceSystem = World.GetOrCreateSystemManaged<ResourceSystem>();
            m_VehicleCapacitySystem = World.GetOrCreateSystemManaged<VehicleCapacitySystem>();

            m_BuyerQuery = SystemAPI.QueryBuilder()
                .WithAll<ResourceBuyer, BuyingCompany, PrefabRef>()
                .WithNone<Deleted, Temp>()
                .Build();

            RequireForUpdate(m_BuyerQuery);
        }

        protected override void OnUpdate()
        {
            DeliveryTruckSelectData truckSelectData = m_VehicleCapacitySystem.GetDeliveryTruckSelectData();

            ComponentLookup<ResourceBuyer> buyerLookup =
                SystemAPI.GetComponentLookup<ResourceBuyer>(isReadOnly: false);

            ComponentLookup<PrefabRef> prefabLookup =
                SystemAPI.GetComponentLookup<PrefabRef>(isReadOnly: true);

            ComponentLookup<IndustrialProcessData> processLookup =
                SystemAPI.GetComponentLookup<IndustrialProcessData>(isReadOnly: true);

            ComponentLookup<StorageLimitData> limitLookup =
                SystemAPI.GetComponentLookup<StorageLimitData>(isReadOnly: true);

            ComponentLookup<ResourceData> resourceDataLookup =
                SystemAPI.GetComponentLookup<ResourceData>(isReadOnly: true);

            ComponentLookup<Game.Vehicles.DeliveryTruck> truckLookup =
                SystemAPI.GetComponentLookup<Game.Vehicles.DeliveryTruck>(isReadOnly: true);

            BufferLookup<Resources> resourcesLookup =
                SystemAPI.GetBufferLookup<Resources>(isReadOnly: true);

            BufferLookup<OwnedVehicle> ownedVehicleLookup =
                SystemAPI.GetBufferLookup<OwnedVehicle>(isReadOnly: true);

            BufferLookup<TripNeeded> tripLookup =
                SystemAPI.GetBufferLookup<TripNeeded>(isReadOnly: true);

            BufferLookup<LayoutElement> layoutLookup =
                SystemAPI.GetBufferLookup<LayoutElement>(isReadOnly: true);

            ResourcePrefabs resourcePrefabs = m_ResourceSystem.GetPrefabs();
            bool verbose = Mod.Settings != null && Mod.Settings.EnableDebugLogging;

            bool IsWeightedResource(Resource resource)
            {
                if (resource == Resource.NoResource)
                {
                    return false;
                }

                Entity resourcePrefab = resourcePrefabs[resource];
                if (resourcePrefab == Entity.Null)
                {
                    return false;
                }

                if (!resourceDataLookup.TryGetComponent(resourcePrefab, out ResourceData data))
                {
                    return false;
                }

                return data.m_Weight > 0f;
            }

            int GetKnownInputAmount(Entity companyEntity, Resource resource)
            {
                int amount = 0;

                if (resourcesLookup.HasBuffer(companyEntity))
                {
                    amount += EconomyUtils.GetResources(resource, resourcesLookup[companyEntity]);
                }

                if (ownedVehicleLookup.HasBuffer(companyEntity))
                {
                    DynamicBuffer<OwnedVehicle> vehicles = ownedVehicleLookup[companyEntity];
                    for (int i = 0; i < vehicles.Length; i++)
                    {
                        amount += VehicleUtils.GetBuyingTrucksLoad(
                            vehicles[i].m_Vehicle,
                            resource,
                            ref truckLookup,
                            ref layoutLookup);
                    }
                }

                if (tripLookup.HasBuffer(companyEntity))
                {
                    DynamicBuffer<TripNeeded> trips = tripLookup[companyEntity];
                    for (int i = 0; i < trips.Length; i++)
                    {
                        TripNeeded trip = trips[i];
                        if (trip.m_Resource != resource)
                        {
                            continue;
                        }

                        if (trip.m_Purpose == Purpose.Shopping || trip.m_Purpose == Purpose.CompanyShopping)
                        {
                            amount += trip.m_Data;
                        }
                    }
                }

                return amount;
            }

            int GetKnownOutputAmount(Entity companyEntity, Resource resource)
            {
                if (!resourcesLookup.HasBuffer(companyEntity))
                {
                    return 0;
                }

                return EconomyUtils.GetResources(resource, resourcesLookup[companyEntity]);
            }

            int GetTotalKnownWeightedStorageUsed(Entity companyEntity, IndustrialProcessData process)
            {
                int used = 0;

                Resource input1 = process.m_Input1.m_Resource;
                Resource input2 = process.m_Input2.m_Resource;
                Resource output = process.m_Output.m_Resource;

                if (IsWeightedResource(input1))
                {
                    used += GetKnownInputAmount(companyEntity, input1);
                }

                if (input2 != Resource.NoResource &&
                    input2 != input1 &&
                    IsWeightedResource(input2))
                {
                    used += GetKnownInputAmount(companyEntity, input2);
                }

                if (output != Resource.NoResource &&
                    output != input1 &&
                    output != input2 &&
                    IsWeightedResource(output))
                {
                    used += GetKnownOutputAmount(companyEntity, output);
                }

                return used;
            }

            using NativeArray<Entity> entities = m_BuyerQuery.ToEntityArray(Allocator.Temp);

            int changed = 0;

            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];

                ResourceBuyer buyer = buyerLookup[entity];
                if (buyer.m_AmountNeeded <= 0)
                {
                    continue;
                }

                Resource resource = buyer.m_ResourceNeeded;
                if (resource == Resource.NoResource || !IsWeightedResource(resource))
                {
                    continue;
                }

                if (!prefabLookup.TryGetComponent(entity, out PrefabRef prefabRef))
                {
                    continue;
                }

                Entity prefab = prefabRef.m_Prefab;
                if (!processLookup.TryGetComponent(prefab, out IndustrialProcessData process))
                {
                    continue;
                }

                if (resource != process.m_Input1.m_Resource && resource != process.m_Input2.m_Resource)
                {
                    continue;
                }

                truckSelectData.GetCapacityRange(resource, out _, out int rangeMaxCapacity);
                if (rangeMaxCapacity <= 0 || rangeMaxCapacity <= buyer.m_AmountNeeded)
                {
                    continue;
                }

                int storageLimit = int.MaxValue;
                if (limitLookup.TryGetComponent(prefab, out StorageLimitData limitData))
                {
                    storageLimit = limitData.m_Limit;
                }

                int totalKnownWeightedUsed = GetTotalKnownWeightedStorageUsed(entity, process);
                int storageLeft = storageLimit == int.MaxValue
                    ? int.MaxValue
                    : math.max(0, storageLimit - totalKnownWeightedUsed);

                if (storageLeft <= buyer.m_AmountNeeded)
                {
                    continue;
                }

                int knownForTargetResource = GetKnownInputAmount(entity, resource);

                int rawDesiredLevel = storageLeft == int.MaxValue
                    ? rangeMaxCapacity
                    : math.min(rangeMaxCapacity, knownForTargetResource + storageLeft);

                int rawDesiredRequest = rawDesiredLevel - knownForTargetResource;

                if (rawDesiredRequest <= buyer.m_AmountNeeded)
                {
                    continue;
                }

                if (!StationTransferAmountUtil.TryGetSafeSelectedTruckCapacity(
                        truckSelectData,
                        resource,
                        rawDesiredRequest,
                        out int safeSelectedCapacity))
                {
                    continue;
                }

                int desiredRequest = math.min(rawDesiredRequest, safeSelectedCapacity);
                if (desiredRequest <= buyer.m_AmountNeeded)
                {
                    continue;
                }

                int oldAmount = buyer.m_AmountNeeded;
                buyer.m_AmountNeeded = desiredRequest;
                buyerLookup[entity] = buyer;
                changed++;

                if (verbose)
                {
                    string prefabName = PrefabNameUtil.GetNameSafe(m_PrefabSystem, prefab);

                    Mod.s_Log.Info(
                        $"{Mod.ModTag} [DISPATCH][CompanyShopping] ENTITY ID {entity.Index}:{entity.Version} " +
                        $"prefab='{prefabName}' Resource={resource} Request={desiredRequest} SafeTruckCap={safeSelectedCapacity}");
                }
            }

            if (changed > 0 && verbose)
            {
                Mod.s_Log.Info(
                    $"{Mod.ModTag} CompanyShoppingCapacity: promoted {changed} company buyer request(s) toward full truck size.");
            }
        }
    }
}
