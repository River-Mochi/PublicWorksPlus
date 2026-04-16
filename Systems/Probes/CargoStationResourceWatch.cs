// File: Systems/Probes/CargoStationResourceWatch.cs
// Purpose: One-shot cargo station resource watch for scan report.
// Notes:
// - Separate helper so it can be removed later without touching core systems.
// - Release-safe because it only runs from the existing scan-report button.
// - Focuses on cargo stations / ports and highlights garbage + transfer request flags.

namespace PublicWorksPlus
{
    using Game.Buildings;
    using Game.Companies;
    using Game.Economy;
    using Game.Prefabs;
    using System;
    using System.IO;
    using System.Text;
    using Unity.Collections;
    using Unity.Entities;

    internal static class CargoStationResourceWatch
    {
        internal static void Append(
            PrefabScanSystem system,
            PrefabSystem prefabSystem,
            StringBuilder sb,
            ref int lines,
            ref bool truncated)
        {
            if (truncated)
            {
                return;
            }

            EntityQuery q = system.GetEntityQuery(
                ComponentType.ReadOnly<CargoTransportStation>(),
                ComponentType.ReadOnly<StorageCompany>(),
                ComponentType.ReadOnly<PrefabRef>(),
                ComponentType.ReadOnly<Game.Economy.Resources>(),
                ComponentType.ReadOnly<StorageTransferRequest>());

            AppendCapped(sb, ref lines, ref truncated, "== Cargo station resource watch ==");
            AppendCapped(sb, ref lines, ref truncated, "One-time snapshot of live cargo stations / ports.");
            AppendCapped(sb, ref lines, ref truncated, "Highlights Garbage storage and Garbage transfer requests.");
            AppendCapped(sb, ref lines, ref truncated, "");

            if (q.IsEmptyIgnoreFilter)
            {
                AppendCapped(sb, ref lines, ref truncated, "No live cargo stations found.");
                AppendCapped(sb, ref lines, ref truncated, "");
                return;
            }

            int totalStations = 0;
            int stationsWithGarbage = 0;
            long totalGarbage = 0;

            using NativeArray<Entity> entities = q.ToEntityArray(Allocator.Temp);

            foreach (Entity entity in entities)
            {
                if (truncated)
                {
                    break;
                }

                totalStations++;

                PrefabRef prefabRef = system.EntityManager.GetComponentData<PrefabRef>(entity);
                string prefabName = PrefabNameUtil.GetNameSafe(prefabSystem, prefabRef.m_Prefab);

                DynamicBuffer<Game.Economy.Resources> resources = system.EntityManager.GetBuffer<Game.Economy.Resources>(entity);
                DynamicBuffer<StorageTransferRequest> requests = system.EntityManager.GetBuffer<StorageTransferRequest>(entity);

                long garbageStored = EconomyUtils.GetResources(Resource.Garbage, resources);
                totalGarbage += garbageStored;

                if (garbageStored > 0)
                {
                    stationsWithGarbage++;
                }

                int garbageReqOutgoingCar = 0;
                int garbageReqIncomingCar = 0;
                int garbageReqOutgoingTrack = 0;
                int garbageReqIncomingTrack = 0;
                int garbageReqOutgoingTransport = 0;
                int garbageReqIncomingTransport = 0;

                for (int i = 0; i < requests.Length; i++)
                {
                    StorageTransferRequest req = requests[i];
                    if (req.m_Resource != Resource.Garbage)
                    {
                        continue;
                    }

                    bool incoming = (req.m_Flags & StorageTransferFlags.Incoming) != 0;
                    bool car = (req.m_Flags & StorageTransferFlags.Car) != 0;
                    bool track = (req.m_Flags & StorageTransferFlags.Track) != 0;
                    bool transport = (req.m_Flags & StorageTransferFlags.Transport) != 0;

                    if (car)
                    {
                        if (incoming) garbageReqIncomingCar += req.m_Amount;
                        else garbageReqOutgoingCar += req.m_Amount;
                    }

                    if (track)
                    {
                        if (incoming) garbageReqIncomingTrack += req.m_Amount;
                        else garbageReqOutgoingTrack += req.m_Amount;
                    }

                    if (transport)
                    {
                        if (incoming) garbageReqIncomingTransport += req.m_Amount;
                        else garbageReqOutgoingTransport += req.m_Amount;
                    }
                }

                // Print every station with garbage or any garbage request.
                if (garbageStored > 0 ||
                    garbageReqOutgoingCar != 0 || garbageReqIncomingCar != 0 ||
                    garbageReqOutgoingTrack != 0 || garbageReqIncomingTrack != 0 ||
                    garbageReqOutgoingTransport != 0 || garbageReqIncomingTransport != 0)
                {
                    AppendCapped(
                        sb,
                        ref lines,
                        ref truncated,
                        $"- {prefabName} ENTITY {entity.Index}:{entity.Version} " +
                        $"GarbageStored={FmtTons((int)garbageStored)} " +
                        $"Car(out={FmtTons(garbageReqOutgoingCar)}, in={FmtTons(garbageReqIncomingCar)}) " +
                        $"Track(out={FmtTons(garbageReqOutgoingTrack)}, in={FmtTons(garbageReqIncomingTrack)}) " +
                        $"Transport(out={FmtTons(garbageReqOutgoingTransport)}, in={FmtTons(garbageReqIncomingTransport)})");
                }
            }

            AppendCapped(
                sb,
                ref lines,
                ref truncated,
                $"Cargo station watch summary: Stations={totalStations} StationsWithGarbage={stationsWithGarbage} TotalGarbage={FmtTons((int)totalGarbage)}");

            AppendCapped(sb, ref lines, ref truncated, "");
        }

        private static void AppendCapped(StringBuilder sb, ref int lines, ref bool truncated, string line)
        {
            if (truncated)
            {
                return;
            }

            sb.AppendLine(line);
            lines++;
        }

        private static string FmtTons(int amount)
        {
            float tons = amount / 1000f;
            return $"{amount} (~{tons:0.##}t)";
        }
    }
}
