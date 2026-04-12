// File: Systems/StorageTransfer/StationTransferCapacitySystem.cs
// Purpose: Promote storage-company / OC outbound car storage-transfer requests
//          up to at least one full currently selectable truck.
// Notes:
// - Targets runtime StorageCompany entities and outside-connection entities.
// - Targets outbound CAR requests only.
// - Mirrors the same promoted amount into the matching incoming request
//   when the counterpart buffer exists.
// - Narrow scope by design: no ships, trains, or station preference logic.

namespace PublicWorksPlus
{
    using Game;
    using Game.Common;
    using Game.Prefabs;
    using Game.Tools;
    using Unity.Collections;
    using Unity.Entities;

    public sealed partial class StationTransferCapacitySystem : GameSystemBase
    {
        private VehicleCapacitySystem m_VehicleCapacitySystem = null!;
        private EntityQuery m_RequestQuery;

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

            m_RequestQuery = SystemAPI.QueryBuilder()
                .WithAll<Game.Companies.StorageTransferRequest>()
                .WithNone<Deleted, Temp>()
                .Build();

            RequireForUpdate(m_RequestQuery);
        }

        protected override void OnUpdate()
        {
            DeliveryTruckSelectData truckSelectData = m_VehicleCapacitySystem.GetDeliveryTruckSelectData();

            ComponentLookup<Game.Companies.StorageCompany> storageCompanyLookup =
                SystemAPI.GetComponentLookup<Game.Companies.StorageCompany>(isReadOnly: true);

            ComponentLookup<Game.Objects.OutsideConnection> ocLookup =
                SystemAPI.GetComponentLookup<Game.Objects.OutsideConnection>(isReadOnly: true);

            ComponentLookup<Deleted> deletedLookup =
                SystemAPI.GetComponentLookup<Deleted>(isReadOnly: true);

            ComponentLookup<Temp> tempLookup =
                SystemAPI.GetComponentLookup<Temp>(isReadOnly: true);

            BufferLookup<Game.Companies.StorageTransferRequest> requestLookup =
                SystemAPI.GetBufferLookup<Game.Companies.StorageTransferRequest>(isReadOnly: false);

            bool verbose = Mod.Settings != null && Mod.Settings.EnableDebugLogging;

            using NativeArray<Entity> entities = m_RequestQuery.ToEntityArray(Allocator.Temp);

            int changed = 0;
            int mirrored = 0;

            for (int e = 0; e < entities.Length; e++)
            {
                Entity entity = entities[e];

                if (deletedLookup.HasComponent(entity) || tempLookup.HasComponent(entity))
                {
                    continue;
                }

                bool isStorageCompany = storageCompanyLookup.HasComponent(entity);
                bool isOC = ocLookup.HasComponent(entity);

                if (!isStorageCompany && !isOC)
                {
                    continue;
                }

                if (!requestLookup.HasBuffer(entity))
                {
                    continue;
                }

                DynamicBuffer<Game.Companies.StorageTransferRequest> requests = requestLookup[entity];

                for (int i = 0; i < requests.Length; i++)
                {
                    Game.Companies.StorageTransferRequest request = requests[i];

                    if (!StationTransferAmountUtil.IsEligibleOutgoingCarRequest(request.m_Flags))
                    {
                        continue;
                    }

                    int oldAmount = request.m_Amount;

                    if (!StationTransferAmountUtil.TryPromoteToAtLeastOneFullTruck(
                            truckSelectData,
                            request.m_Resource,
                            request.m_Amount,
                            out int adjustedAmount))
                    {
                        continue;
                    }

                    request.m_Amount = adjustedAmount;
                    requests[i] = request;
                    changed++;

                    bool mirroredThisOne = TryPromoteMatchingIncomingRequest(
                        requestLookup,
                        entity,
                        request.m_Target,
                        request.m_Resource,
                        request.m_Flags,
                        adjustedAmount);

                    if (mirroredThisOne)
                    {
                        mirrored++;
                    }

                    if (verbose)
                    {
                        string kind = isOC ? "OutsideConnection" : "StorageCompany";

                        Mod.s_Log.Info(
                            $"{Mod.ModTag} StationTransferCapacity: " +
                            $"sourceIndex={entity.Index} sourceVersion={entity.Version} " +
                            $"targetIndex={request.m_Target.Index} targetVersion={request.m_Target.Version} " +
                            $"kind={kind} Resource={request.m_Resource} Flags={request.m_Flags} " +
                            $"OldAmount={oldAmount} NewAmount={adjustedAmount} Mirrored={mirroredThisOne}");
                    }
                }
            }

            if (changed > 0 && verbose)
            {
                Mod.s_Log.Info(
                    $"{Mod.ModTag} StationTransferCapacity: promoted {changed} storage-company/OC outbound car request(s) to full truck size; mirrored {mirrored} matching incoming request(s).");
            }
        }

        private static bool TryPromoteMatchingIncomingRequest(
            BufferLookup<Game.Companies.StorageTransferRequest> requestLookup,
            Entity sourceEntity,
            Entity targetEntity,
            Game.Economy.Resource resource,
            Game.Companies.StorageTransferFlags outgoingFlags,
            int adjustedAmount)
        {
            if (!requestLookup.HasBuffer(targetEntity))
            {
                return false;
            }

            DynamicBuffer<Game.Companies.StorageTransferRequest> targetRequests = requestLookup[targetEntity];
            Game.Companies.StorageTransferFlags expectedIncomingFlags =
                outgoingFlags | Game.Companies.StorageTransferFlags.Incoming;

            for (int i = 0; i < targetRequests.Length; i++)
            {
                Game.Companies.StorageTransferRequest incoming = targetRequests[i];

                if (incoming.m_Target != sourceEntity)
                {
                    continue;
                }

                if (incoming.m_Resource != resource)
                {
                    continue;
                }

                if (incoming.m_Flags != expectedIncomingFlags)
                {
                    continue;
                }

                if (incoming.m_Amount >= adjustedAmount)
                {
                    return false;
                }

                incoming.m_Amount = adjustedAmount;
                targetRequests[i] = incoming;
                return true;
            }

            return false;
        }
    }
}
