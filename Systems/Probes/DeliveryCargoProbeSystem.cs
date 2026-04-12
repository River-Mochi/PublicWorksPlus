// File: Systems/Probes/DeliveryCargoProbeSystem.cs
// Purpose: Runtime proof logger for delivery cargo loads vs vanilla caps.
// Notes:
// - Reads Game.Vehicles.DeliveryTruck (m_Amount, m_Resource) on live vehicles.
// - Uses PrefabRef to look up the vehicle prefab.
// - Reads vanilla cap from managed prefab component (Game.Prefabs.DeliveryTruck) via PrefabSystem.
// - Classifies into the same buckets as IndustrySystem for readable summaries.
// - Work is gated by EnableDebugLogging in OnUpdate.
// - GetUpdateInterval is throttling only; it must always return a power-of-2 (or 1).
// - Logs TOP 5 over-vanilla vehicles per bucket with exact ENTITY ID and carried resource.

namespace PublicWorksPlus
{
    using Colossal.Serialization.Entities;
    using Game;
    using Game.Economy;
    using Game.Prefabs;
    using Game.Simulation;
    using System;
    using System.Collections.Generic;
    using Unity.Entities;

    public sealed partial class DeliveryCargoProbeSystem : GameSystemBase
    {
        public static readonly int UpdatesPerDay = 64; // interval = 4096 sim frames

        private const int kBucketCount = 5;
        private const int kTopN = 5;

        private PrefabSystem m_PrefabSystem = null!;
        private SimulationSystem m_Sim = null!;

        private readonly Dictionary<Entity, int> m_VanillaCapByPrefab = new Dictionary<Entity, int>();
        private readonly BucketStats[] m_Stats = new BucketStats[kBucketCount];
        private readonly List<BucketHit>[] m_TopOver = new List<BucketHit>[kBucketCount];

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

            for (int i = 0; i < m_TopOver.Length; i++)
            {
                m_TopOver[i] = new List<BucketHit>(kTopN);
            }

            ClearStats();
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            bool isRealGame =
                mode == GameMode.Game &&
                (purpose == Purpose.NewGame || purpose == Purpose.LoadGame);

            if (!isRealGame)
            {
                return;
            }

            m_VanillaCapByPrefab.Clear();
            ClearStats();
        }

        protected override void OnUpdate()
        {
            if (Mod.Settings == null || !Mod.Settings.EnableDebugLogging)
            {
                return;
            }

            ClearStats();

            ComponentLookup<DeliveryTruckData> dtdLookup = SystemAPI.GetComponentLookup<DeliveryTruckData>(isReadOnly: true);
            ComponentLookup<CarTractorData> tractorLookup = SystemAPI.GetComponentLookup<CarTractorData>(isReadOnly: true);
            ComponentLookup<CarTrailerData> trailerLookup = SystemAPI.GetComponentLookup<CarTrailerData>(isReadOnly: true);

            int scanned = 0;

            foreach ((RefRO<Game.Vehicles.DeliveryTruck> truckRef, RefRO<PrefabRef> prRef, Entity entity) in SystemAPI
                         .Query<RefRO<Game.Vehicles.DeliveryTruck>, RefRO<PrefabRef>>()
                         .WithEntityAccess())
            {
                scanned++;

                Game.Vehicles.DeliveryTruck truck = truckRef.ValueRO;
                int amount = truck.m_Amount;
                Resource carriedResource = truck.m_Resource;

                Entity prefab = prRef.ValueRO.m_Prefab;

                int vanillaCap = GetVanillaCap(prefab);
                if (vanillaCap <= 0)
                {
                    continue;
                }

                Resource transportedFlags = Resource.NoResource;
                if (dtdLookup.TryGetComponent(prefab, out DeliveryTruckData dtd))
                {
                    transportedFlags = dtd.m_TransportedResources;
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
                    transportedFlags,
                    hasTractor,
                    tractorType,
                    hasTrailer,
                    trailerType);

                int bi = (int)bucket;
                if (bi < 0 || bi >= kBucketCount)
                {
                    bi = (int)VehicleHelpers.DeliveryBucket.Other;
                }

                ref BucketStats s = ref m_Stats[bi];
                s.Seen++;

                if (amount <= 0)
                {
                    continue;
                }

                s.Carrying++;

                if (amount > s.MaxAmount)
                {
                    s.MaxAmount = amount;
                    s.MaxPrefabName = prefabName;
                    s.MaxEntity = entity;
                    s.MaxVanillaCap = vanillaCap;
                    s.MaxResource = carriedResource;
                }

                if (amount > vanillaCap)
                {
                    s.OverVanilla++;

                    if (amount > s.MaxOverAmount)
                    {
                        s.MaxOverAmount = amount;
                        s.MaxOverPrefabName = prefabName;
                        s.MaxOverEntity = entity;
                        s.MaxOverVanillaCap = vanillaCap;
                        s.MaxOverResource = carriedResource;
                    }

                    AddTopHit(
                        m_TopOver[bi],
                        new BucketHit
                        {
                            Entity = entity,
                            Amount = amount,
                            VanillaCap = vanillaCap,
                            CarriedResource = carriedResource,
                            PrefabName = prefabName,
                        });
                }
            }

            int totalSeen = 0;
            int totalCarrying = 0;
            int totalOverVanilla = 0;
            int globalMaxAmount = 0;
            string globalMaxPrefabName = string.Empty;
            Entity globalMaxEntity = Entity.Null;
            int globalMaxVanillaCap = 0;
            Resource globalMaxResource = Resource.NoResource;

            int globalMaxOverAmount = 0;
            string globalMaxOverPrefabName = string.Empty;
            Entity globalMaxOverEntity = Entity.Null;
            int globalMaxOverVanillaCap = 0;
            Resource globalMaxOverResource = Resource.NoResource;

            for (int i = 0; i < m_Stats.Length; i++)
            {
                totalSeen += m_Stats[i].Seen;
                totalCarrying += m_Stats[i].Carrying;
                totalOverVanilla += m_Stats[i].OverVanilla;

                if (m_Stats[i].MaxAmount > globalMaxAmount)
                {
                    globalMaxAmount = m_Stats[i].MaxAmount;
                    globalMaxPrefabName = m_Stats[i].MaxPrefabName;
                    globalMaxEntity = m_Stats[i].MaxEntity;
                    globalMaxVanillaCap = m_Stats[i].MaxVanillaCap;
                    globalMaxResource = m_Stats[i].MaxResource;
                }

                if (m_Stats[i].MaxOverAmount > globalMaxOverAmount)
                {
                    globalMaxOverAmount = m_Stats[i].MaxOverAmount;
                    globalMaxOverPrefabName = m_Stats[i].MaxOverPrefabName;
                    globalMaxOverEntity = m_Stats[i].MaxOverEntity;
                    globalMaxOverVanillaCap = m_Stats[i].MaxOverVanillaCap;
                    globalMaxOverResource = m_Stats[i].MaxOverResource;
                }
            }

            Mod.s_Log.Info("============================================================");
            Mod.s_Log.Info($"{Mod.ModTag} DELIVERY CARGO PROBE");
            Mod.s_Log.Info("============================================================");
            Mod.s_Log.Info(
                $"{Mod.ModTag} Delivery cargo live sample: scanned={scanned} " +
                $"seen={totalSeen} carrying={totalCarrying} overVanilla={totalOverVanilla} " +
                $"frame={m_Sim.frameIndex}");

            if (totalCarrying == 0)
            {
                Mod.s_Log.Info($"{Mod.ModTag} Delivery cargo proof: no carrying delivery trucks found in this sample.");
            }
            else if (totalOverVanilla > 0)
            {
                Mod.s_Log.Info(
                    $"{Mod.ModTag} Delivery cargo proof: FOUND live trucks above vanilla capacity. " +
                    $"overVanilla={totalOverVanilla}/{totalCarrying} " +
                    $"topOver={FmtTons(globalMaxOverAmount)} prefab='{globalMaxOverPrefabName}' " +
                    $"ENTITY ID {FmtEntity(globalMaxOverEntity)} " +
                    $"Carrying={FormatResource(globalMaxOverResource)} " +
                    $"VanillaCap={FmtTons(globalMaxOverVanillaCap)}");
            }
            else
            {
                Mod.s_Log.Info(
                    $"{Mod.ModTag} Delivery cargo proof: no live trucks above vanilla capacity found in this sample. " +
                    $"HighestObserved={FmtTons(globalMaxAmount)} prefab='{globalMaxPrefabName}' " +
                    $"ENTITY ID {FmtEntity(globalMaxEntity)} " +
                    $"Carrying={FormatResource(globalMaxResource)} " +
                    $"VanillaCap={FmtTons(globalMaxVanillaCap)}");
            }

            LogBucket("Semi", m_Stats[(int)VehicleHelpers.DeliveryBucket.Semi], m_TopOver[(int)VehicleHelpers.DeliveryBucket.Semi]);
            LogBucket("Van", m_Stats[(int)VehicleHelpers.DeliveryBucket.Van], m_TopOver[(int)VehicleHelpers.DeliveryBucket.Van]);
            LogBucket("Raw", m_Stats[(int)VehicleHelpers.DeliveryBucket.RawMaterials], m_TopOver[(int)VehicleHelpers.DeliveryBucket.RawMaterials]);
            LogBucket("Motorbike", m_Stats[(int)VehicleHelpers.DeliveryBucket.Motorbike], m_TopOver[(int)VehicleHelpers.DeliveryBucket.Motorbike]);
            LogBucket("Other", m_Stats[(int)VehicleHelpers.DeliveryBucket.Other], m_TopOver[(int)VehicleHelpers.DeliveryBucket.Other]);
            Mod.s_Log.Info("============================================================");
        }

        private void ClearStats()
        {
            for (int i = 0; i < m_Stats.Length; i++)
            {
                m_Stats[i] = default;
                m_Stats[i].MaxPrefabName = string.Empty;
                m_Stats[i].MaxOverPrefabName = string.Empty;
                m_Stats[i].MaxEntity = Entity.Null;
                m_Stats[i].MaxOverEntity = Entity.Null;
                m_TopOver[i].Clear();
            }
        }

        private int GetVanillaCap(Entity prefab)
        {
            if (m_VanillaCapByPrefab.TryGetValue(prefab, out int cap))
            {
                return cap;
            }

            cap = 0;

            if (PrefabComponentUtil.TryGetComponent(m_PrefabSystem, prefab, out Game.Prefabs.DeliveryTruck truck))
            {
                cap = truck.m_CargoCapacity;
            }

            m_VanillaCapByPrefab[prefab] = cap;
            return cap;
        }

        private static void AddTopHit(List<BucketHit> list, BucketHit hit)
        {
            int insertAt = list.Count;

            for (int i = 0; i < list.Count; i++)
            {
                if (hit.Amount > list[i].Amount)
                {
                    insertAt = i;
                    break;
                }
            }

            if (insertAt >= kTopN && list.Count >= kTopN)
            {
                return;
            }

            list.Insert(insertAt, hit);

            if (list.Count > kTopN)
            {
                list.RemoveAt(list.Count - 1);
            }
        }

        private static void LogBucket(string name, BucketStats s, List<BucketHit> topOver)
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
                $"HighestObserved={FmtTons(s.MaxAmount)} prefab='{s.MaxPrefabName}' " +
                $"ENTITY ID {FmtEntity(s.MaxEntity)} " +
                $"Carrying={FormatResource(s.MaxResource)}");

            for (int i = 0; i < topOver.Count; i++)
            {
                BucketHit hit = topOver[i];

                Mod.s_Log.Info(
                    $"{Mod.ModTag} DeliveryCargoProbe {name} TOP {i + 1}: " +
                    $"ENTITY ID {FmtEntity(hit.Entity)} " +
                    $"Amount={FmtTons(hit.Amount)} " +
                    $"VanillaCap={FmtTons(hit.VanillaCap)} " +
                    $"Carrying={FormatResource(hit.CarriedResource)} " +
                    $"Prefab='{hit.PrefabName}'");
            }
        }

        private static string FmtEntity(Entity entity)
        {
            if (entity == Entity.Null)
            {
                return "(null)";
            }

            return $"{entity.Index}:{entity.Version}";
        }

        private static string FmtTons(int amount)
        {
            float tons = amount / 1000f;
            return $"{amount} (~{tons:0.##}t)";
        }

        private static string FormatResource(Resource resource)
        {
            if (resource == Resource.NoResource)
            {
                return "NoResource";
            }

            string text = resource.ToString();
            if (!ulong.TryParse(text, out _))
            {
                return text;
            }

            ulong raw = Convert.ToUInt64(resource);
            List<string> names = new List<string>();

            foreach (Resource value in Enum.GetValues(typeof(Resource)))
            {
                ulong bits = Convert.ToUInt64(value);
                if (bits == 0 || !IsSingleBit(bits))
                {
                    continue;
                }

                if ((raw & bits) == bits)
                {
                    names.Add(value.ToString());
                }
            }

            if (names.Count > 0)
            {
                return string.Join("|", names);
            }

            return text;
        }

        private static bool IsSingleBit(ulong value)
        {
            return (value & (value - 1)) == 0;
        }

        private struct BucketStats
        {
            public int Seen;
            public int Carrying;
            public int OverVanilla;

            public int MaxAmount;
            public string MaxPrefabName;
            public Entity MaxEntity;
            public int MaxVanillaCap;
            public Resource MaxResource;

            public int MaxOverAmount;
            public string MaxOverPrefabName;
            public Entity MaxOverEntity;
            public int MaxOverVanillaCap;
            public Resource MaxOverResource;
        }

        private struct BucketHit
        {
            public Entity Entity;
            public int Amount;
            public int VanillaCap;
            public Resource CarriedResource;
            public string PrefabName;
        }
    }
}
