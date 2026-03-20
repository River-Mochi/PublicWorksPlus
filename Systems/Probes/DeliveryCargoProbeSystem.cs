// File: Systems/Probes/DeliveryCargoProbeSystem.cs
// Purpose: Runtime proof logger for delivery cargo loads vs vanilla caps.
// Notes:
// - Reads Game.Vehicles.DeliveryTruck (m_Amount) on live vehicles.
// - Uses PrefabRef to look up the vehicle prefab.
// - Reads vanilla cap from managed prefab component (Game.Prefabs.DeliveryTruck) via PrefabSystem.
// - Classifies into the same buckets as IndustrySystem for readable summaries.
// - Work is gated by EnableDebugLogging in OnUpdate.
// - GetUpdateInterval is throttling only; it must always return a power-of-2 (or 1).

namespace PublicWorksPlus
{
    using Colossal.Serialization.Entities;
    using Game;
    using Game.Economy;
    using Game.Prefabs;
    using Game.Simulation;
    using System.Collections.Generic;
    using Unity.Entities;

    public sealed partial class DeliveryCargoProbeSystem : GameSystemBase
    {
        // 262144 sim frames/day. UpdatesPerDay must be power-of-2 so interval stays power-of-2.
        public static readonly int UpdatesPerDay = 64; // interval = 4096 sim frames

        // VehicleHelpers.DeliveryBucket enum:
        // Other=0, Semi=1, Van=2, RawMaterials=3, Motorbike=4
        private const int kBucketCount = 5;

        private PrefabSystem m_PrefabSystem = null!;
        private SimulationSystem m_Sim = null!;

        private readonly Dictionary<Entity, int> m_VanillaCapByPrefab = new Dictionary<Entity, int>();
        private readonly BucketStats[] m_Stats = new BucketStats[kBucketCount];

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            if (phase == SystemUpdatePhase.GameSimulation)
            {
                return 262144 / UpdatesPerDay;
            }

            return 1;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();
            m_Sim = World.GetOrCreateSystemManaged<SimulationSystem>();

            EntityQuery q = SystemAPI.QueryBuilder()
                .WithAll<Game.Vehicles.DeliveryTruck, PrefabRef>()
                .Build();

            RequireForUpdate(q);

            ClearStats();
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            bool isRealGame =
                mode == GameMode.Game &&
                (purpose == Purpose.NewGame || purpose == Purpose.LoadGame);

            if (!isRealGame)
                return;

            m_VanillaCapByPrefab.Clear();
            ClearStats();
        }

        protected override void OnUpdate()
        {
            if (Mod.Settings == null || !Mod.Settings.EnableDebugLogging)
                return;

            ClearStats();

            ComponentLookup<DeliveryTruckData> dtdLookup = SystemAPI.GetComponentLookup<DeliveryTruckData>(isReadOnly: true);
            ComponentLookup<CarTractorData> tractorLookup = SystemAPI.GetComponentLookup<CarTractorData>(isReadOnly: true);
            ComponentLookup<CarTrailerData> trailerLookup = SystemAPI.GetComponentLookup<CarTrailerData>(isReadOnly: true);

            int scanned = 0;

            foreach ((RefRO<Game.Vehicles.DeliveryTruck> truckRef, RefRO<PrefabRef> prRef) in SystemAPI
                         .Query<RefRO<Game.Vehicles.DeliveryTruck>, RefRO<PrefabRef>>())
            {
                scanned++;

                Game.Vehicles.DeliveryTruck truck = truckRef.ValueRO;
                int amount = truck.m_Amount;

                Entity prefab = prRef.ValueRO.m_Prefab;

                int vanillaCap = GetVanillaCap(prefab);
                if (vanillaCap <= 0)
                    continue;

                Resource transported = Resource.NoResource;
                if (dtdLookup.TryGetComponent(prefab, out DeliveryTruckData dtd))
                {
                    transported = dtd.m_TransportedResources;
                }

                VehicleHelpers.GetTrailerTypeInfo(
                    in tractorLookup,
                    in trailerLookup,
                    prefab,
                    out bool hasTractor,
                    out CarTrailerType tractorType,
                    out bool hasTrailer,
                    out CarTrailerType trailerType);

                string prefabName = PrefabNameUtil.GetNameSafe(m_PrefabSystem, prefab);

                VehicleHelpers.DeliveryBucket bucket = VehicleHelpers.ClassifyDeliveryTruckPrefab(
                    prefabName,
                    vanillaCap,
                    transported,
                    hasTractor,
                    tractorType,
                    hasTrailer,
                    trailerType);

                int bi = (int)bucket;
                if (bi < 0 || bi >= kBucketCount)
                    bi = (int)VehicleHelpers.DeliveryBucket.Other;

                ref BucketStats s = ref m_Stats[bi];
                s.Seen++;

                if (amount > 0)
                {
                    s.Carrying++;

                    if (amount > vanillaCap)
                        s.OverVanilla++;

                    if (amount > s.MaxAmount)
                    {
                        s.MaxAmount = amount;
                        s.MaxPrefabName = prefabName;
                    }
                }
            }

            Mod.s_Log.Info($"{Mod.ModTag} DeliveryCargoProbe: scanned={scanned} frame={m_Sim.frameIndex}");

            LogBucket("Semi", m_Stats[(int)VehicleHelpers.DeliveryBucket.Semi]);
            LogBucket("Van", m_Stats[(int)VehicleHelpers.DeliveryBucket.Van]);
            LogBucket("Raw", m_Stats[(int)VehicleHelpers.DeliveryBucket.RawMaterials]);
            LogBucket("Motorbike", m_Stats[(int)VehicleHelpers.DeliveryBucket.Motorbike]);
            LogBucket("Other", m_Stats[(int)VehicleHelpers.DeliveryBucket.Other]);
        }

        private void ClearStats()
        {
            for (int i = 0; i < m_Stats.Length; i++)
            {
                m_Stats[i] = default;
                m_Stats[i].MaxPrefabName = string.Empty; // never allow null
            }
        }

        private int GetVanillaCap(Entity prefab)
        {
            if (m_VanillaCapByPrefab.TryGetValue(prefab, out int cap))
                return cap;

            cap = 0;

            if (PrefabComponentUtil.TryGetComponent(m_PrefabSystem, prefab, out Game.Prefabs.DeliveryTruck truck))
            {
                cap = truck.m_CargoCapacity;
            }

            m_VanillaCapByPrefab[prefab] = cap;
            return cap;
        }

        private static void LogBucket(string name, BucketStats s)
        {
            if (s.Seen == 0)
            {
                Mod.s_Log.Info($"{Mod.ModTag} DeliveryCargoProbe {name}: none");
                return;
            }

            float pct = s.Carrying > 0 ? (100f * s.OverVanilla / s.Carrying) : 0f;

            Mod.s_Log.Info(
                $"{Mod.ModTag} DeliveryCargoProbe {name}: seen={s.Seen} carrying={s.Carrying} " +
                $"overVanilla={s.OverVanilla} ({pct:0.#}% of carrying) " +
                $"maxAmount={FmtTons(s.MaxAmount)} prefab='{s.MaxPrefabName}'");
        }

        private static string FmtTons(int amount)
        {
            float tons = amount / 1000f;
            return $"{amount} (~{tons:0.##}t)";
        }

        private struct BucketStats
        {
            public int Seen;
            public int Carrying;
            public int OverVanilla;
            public int MaxAmount;
            public string MaxPrefabName;
        }
    }
}
