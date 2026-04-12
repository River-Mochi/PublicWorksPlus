// File: Systems/StorageTransfer/StationTransferCapacitySystem.cs
// Purpose: First-pass non-Harmony fix for station / OC storage transfer chunk sizing.
// Notes:
// - Targets only station and outside-connection storage-transfer entities.
// - Promotes transfer amount to at least one full selectable truck for that resource.
// - Narrow scope by design: no ships/trains, no station preference UI, no route scoring changes.

namespace PublicWorksPlus
{
    using Game;
    using Game.City;
    using Game.Common;
    using Game.Prefabs;
    using Game.Tools;
    using Unity.Entities;

    public sealed partial class StationTransferCapacitySystem : GameSystemBase
    {
        private VehicleCapacitySystem m_VehicleCapacitySystem = null!;
        private EntityQuery m_TransferQuery;

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            if (phase == SystemUpdatePhase.GameSimulation)
            {
                return 16;
            }

            return 1;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            m_VehicleCapacitySystem = World.GetOrCreateSystemManaged<VehicleCapacitySystem>();

            m_TransferQuery = SystemAPI.QueryBuilder()
                .WithAll<Game.Companies.StorageTransfer>()
                .WithNone<Deleted, Temp>()
                .Build();

            RequireForUpdate(m_TransferQuery);
        }

        protected override void OnUpdate()
        {
            DeliveryTruckSelectData truckSelectData = m_VehicleCapacitySystem.GetDeliveryTruckSelectData();

            ComponentLookup<CityServiceUpkeep> stationLookup =
                SystemAPI.GetComponentLookup<CityServiceUpkeep>(isReadOnly: true);

            ComponentLookup<Game.Objects.OutsideConnection> ocLookup =
                SystemAPI.GetComponentLookup<Game.Objects.OutsideConnection>(isReadOnly: true);

            ComponentLookup<Deleted> deletedLookup =
                SystemAPI.GetComponentLookup<Deleted>(isReadOnly: true);

            ComponentLookup<Temp> tempLookup =
                SystemAPI.GetComponentLookup<Temp>(isReadOnly: true);

            int changed = 0;

            foreach ((RefRW<Game.Companies.StorageTransfer> transferRef, Entity entity) in SystemAPI
                         .Query<RefRW<Game.Companies.StorageTransfer>>()
                         .WithEntityAccess())
            {
                if (deletedLookup.HasComponent(entity) || tempLookup.HasComponent(entity))
                {
                    continue;
                }

                bool isStation = stationLookup.HasComponent(entity);
                bool isOC = ocLookup.HasComponent(entity);

                if (!isStation && !isOC)
                {
                    continue;
                }

                Game.Companies.StorageTransfer transfer = transferRef.ValueRO;

                if (!StationTransferAmountUtil.TryPromoteToAtLeastOneFullTruck(
                        truckSelectData,
                        transfer.m_Resource,
                        transfer.m_Amount,
                        out int adjustedAmount))
                {
                    continue;
                }

                transfer.m_Amount = adjustedAmount;
                transferRef.ValueRW = transfer;
                changed++;
            }

            if (changed > 0 && Mod.Settings != null && Mod.Settings.EnableDebugLogging)
            {
                Mod.s_Log.Info(
                    $"{Mod.ModTag} StationTransferCapacity: promoted {changed} station/OC storage-transfer amount(s) to at least one full truck.");
            }
        }
    }
}
