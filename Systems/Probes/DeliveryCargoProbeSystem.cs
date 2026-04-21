// File: Systems/Probes/DeliveryCargoProbeSystem.cs
// Purpose: Runtime proof logger for delivery cargo loads vs vanilla/current caps.
// Notes:
// - Reads Game.Vehicles.DeliveryTruck (m_Amount, m_Resource, m_State) on live vehicles.
// - Uses PrefabRef to look up the vehicle prefab.
// - Reads vanilla cap from managed prefab component (Game.Prefabs.DeliveryTruck) via PrefabSystem.
// - Reads current cap from live prefab-entity DeliveryTruckData.
// - Classifies into the same buckets as IndustrySystem for readable summaries.
// - Runs only when Verbose debug logs is enabled.
// - Section title reduces repeated text on each line.

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
        private readonly List<BucketHit>[] m_TopOverVanilla = new List<BucketHit>[kBucketCount];
        private readonly List<BucketHit>[] m_TopOverCurrent = new List<BucketHit>[kBucketCount];

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

            for (int i = 0; i < kBucketCount; i++)
            {
                m_TopOverVanilla[i] = new List<BucketHit>(kTopN);
                m_TopOverCurrent[i] = new List<BucketHit>(kTopN);
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
            int companyShoppingCarrying = 0;
            int companyShoppingOverVanilla = 0;

            int storageTransferCarrying = 0;
            int storageTransferOverVanilla = 0;

            int facilityOwnedDispatchCarrying = 0;
            int facilityOwnedDispatchOverVanilla = 0;

            foreach ((RefRO<Game.Vehicles.DeliveryTruck> truckRef, RefRO<PrefabRef> prRef, Entity entity) in SystemAPI
                         .Query<RefRO<Game.Vehicles.DeliveryTruck>, RefRO<PrefabRef>>()
                         .WithEntityAccess())
            {
                scanned++;

                Game.Vehicles.DeliveryTruck truck = truckRef.ValueRO;
                int amount = truck.m_Amount;
                Resource carriedResource = truck.m_Resource;
                string stateText = truck.m_State.ToString();

                Entity prefab = prRef.ValueRO.m_Prefab;

                int vanillaCap = GetVanillaCap(prefab);
                if (vanillaCap <= 0)
                {
                    continue;
                }

                int currentCap = vanillaCap;
                Resource transportedFlags = Resource.NoResource;

                if (dtdLookup.TryGetComponent(prefab, out DeliveryTruckData dtd))
                {
                    currentCap = dtd.m_CargoCapacity;
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
                    s.MaxCurrentCap = currentCap;
                    s.MaxResource = carriedResource;
                    s.MaxStateText = stateText;
                }

                if (amount > vanillaCap)
                {
                    s.OverVanilla++;

                    AddTopHit(
                        m_TopOverVanilla[bi],
                        new BucketHit
                        {
                            Entity = entity,
                            Amount = amount,
                            VanillaCap = vanillaCap,
                            CurrentCap = currentCap,
                            CarriedResource = carriedResource,
                            PrefabName = prefabName,
                            StateText = stateText,
                        });
                }


                bool isOverVanilla = amount > vanillaCap;

                // Category hints from live DeliveryTruck flags.
                // - CompanyShopping = Buying and not StorageTransfer / UpkeepDelivery
                // - StorageTransfer = StorageTransfer
                // - FacilityOwnedDispatch = UpkeepDelivery
                // - OC-Transfer is not isolated cleanly in this one-shot live probe
                if ((truck.m_State & Game.Vehicles.DeliveryTruckFlags.StorageTransfer) != 0)
                {
                    storageTransferCarrying++;
                    if (isOverVanilla) storageTransferOverVanilla++;
                }
                else if ((truck.m_State & Game.Vehicles.DeliveryTruckFlags.UpkeepDelivery) != 0)
                {
                    facilityOwnedDispatchCarrying++;
                    if (isOverVanilla) facilityOwnedDispatchOverVanilla++;
                }
                else if ((truck.m_State & Game.Vehicles.DeliveryTruckFlags.Buying) != 0)
                {
                    companyShoppingCarrying++;
                    if (isOverVanilla) companyShoppingOverVanilla++;
                }


                if (amount > currentCap)
                {
                    s.OverCurrentCap++;

                    AddTopHit(
                        m_TopOverCurrent[bi],
                        new BucketHit
                        {
                            Entity = entity,
                            Amount = amount,
                            VanillaCap = vanillaCap,
                            CurrentCap = currentCap,
                            CarriedResource = carriedResource,
                            PrefabName = prefabName,
                            StateText = stateText,
                        });
                }
            }

            int totalRelevant = 0;
            int totalCarrying = 0;
            int totalOverVanilla = 0;
            int totalOverCurrentCap = 0;

            for (int i = 0; i < kBucketCount; i++)
            {
                totalRelevant += m_Stats[i].Seen;
                totalCarrying += m_Stats[i].Carrying;
                totalOverVanilla += m_Stats[i].OverVanilla;
                totalOverCurrentCap += m_Stats[i].OverCurrentCap;
            }

            LogUtils.Info(Mod.s_Log, () => "============================================================");
            LogUtils.Info(Mod.s_Log, () => $"{Mod.ModTag} DELIVERY CARGO PROBE");
            LogUtils.Info(Mod.s_Log, () => "============================================================");
            LogUtils.Info(
                Mod.s_Log,
                () =>
                $"{Mod.ModTag} Live sample: scanned={scanned} relevant={totalRelevant} carrying={totalCarrying} " +
                $"overVanilla={totalOverVanilla} overCurrentCap={totalOverCurrentCap} simFrame={m_Sim.frameIndex}");

            if (totalOverCurrentCap > 0)
            {
                LogUtils.Info(Mod.s_Log, () => $"{Mod.ModTag} BAD: found live trucks above slider max.");
            }
            else if (totalOverVanilla > 0)
            {
                LogUtils.Info(Mod.s_Log, () => $"{Mod.ModTag} GOOD: {totalOverVanilla} trucks > vanilla capacity. GOOD: none above slider max.");
            }
            else
            {
                LogUtils.Info(Mod.s_Log, () => $"{Mod.ModTag} No live trucks above vanilla in this sample.");
            }

            LogBucket("Semi", m_Stats[(int)VehicleHelpers.DeliveryBucket.Semi], m_TopOverVanilla[(int)VehicleHelpers.DeliveryBucket.Semi], m_TopOverCurrent[(int)VehicleHelpers.DeliveryBucket.Semi]);
            LogBucket("Van", m_Stats[(int)VehicleHelpers.DeliveryBucket.Van], m_TopOverVanilla[(int)VehicleHelpers.DeliveryBucket.Van], m_TopOverCurrent[(int)VehicleHelpers.DeliveryBucket.Van]);
            LogBucket("Raw", m_Stats[(int)VehicleHelpers.DeliveryBucket.RawMaterials], m_TopOverVanilla[(int)VehicleHelpers.DeliveryBucket.RawMaterials], m_TopOverCurrent[(int)VehicleHelpers.DeliveryBucket.RawMaterials]);
            LogBucket("Motorbike", m_Stats[(int)VehicleHelpers.DeliveryBucket.Motorbike], m_TopOverVanilla[(int)VehicleHelpers.DeliveryBucket.Motorbike], m_TopOverCurrent[(int)VehicleHelpers.DeliveryBucket.Motorbike]);
            LogBucket("Other", m_Stats[(int)VehicleHelpers.DeliveryBucket.Other], m_TopOverVanilla[(int)VehicleHelpers.DeliveryBucket.Other], m_TopOverCurrent[(int)VehicleHelpers.DeliveryBucket.Other]);

            LogUtils.Info(
                Mod.s_Log,
                () =>
                $"{Mod.ModTag} SUMMARY " +
                $"Semi={FmtBucketSummary(m_Stats[(int)VehicleHelpers.DeliveryBucket.Semi])} " +
                $"Van={FmtBucketSummary(m_Stats[(int)VehicleHelpers.DeliveryBucket.Van])} " +
                $"Raw={FmtBucketSummary(m_Stats[(int)VehicleHelpers.DeliveryBucket.RawMaterials])} " +
                $"OverCap={totalOverCurrentCap}");


            LogUtils.Info(
                Mod.s_Log,
                () =>
                $"{Mod.ModTag} CATEGORY SUMMARY " +
                $"CompanyShopping={FmtCategorySummary(companyShoppingOverVanilla, companyShoppingCarrying)} " +
                $"StorageTransfer={FmtCategorySummary(storageTransferOverVanilla, storageTransferCarrying)} " +
                $"OC-Transfer=not isolated " +
                $"FacilityOwnedDispatch={FmtCategorySummary(facilityOwnedDispatchOverVanilla, facilityOwnedDispatchCarrying)}");


            LogUtils.Info(Mod.s_Log, () => "============================================================");
        }

        private void ClearStats()
        {
            for (int i = 0; i < kBucketCount; i++)
            {
                m_Stats[i] = default;
                m_Stats[i].MaxPrefabName = string.Empty;
                m_Stats[i].MaxStateText = string.Empty;
                m_Stats[i].MaxEntity = Entity.Null;
                m_TopOverVanilla[i].Clear();
                m_TopOverCurrent[i].Clear();
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

        private static void LogBucket(string name, BucketStats s, List<BucketHit> topOverVanilla, List<BucketHit> topOverCurrent)
        {
            if (s.Seen == 0)
            {
                LogUtils.Info(Mod.s_Log, () => $"{Mod.ModTag} {name}: none");
                return;
            }

            float pctVanilla = s.Carrying > 0 ? (100f * s.OverVanilla / s.Carrying) : 0f;
            float pctCurrent = s.Carrying > 0 ? (100f * s.OverCurrentCap / s.Carrying) : 0f;

            LogUtils.Info(
                Mod.s_Log,
                () =>
                $"{Mod.ModTag} {name}: seen={s.Seen} carrying={s.Carrying} " +
                $"overVanilla={s.OverVanilla} ({pctVanilla:0.#}%) overCurrentCap={s.OverCurrentCap} ({pctCurrent:0.#}%) " +
                $"Highest={FmtTons(s.MaxAmount)} ENTITY ID {FmtEntity(s.MaxEntity)} " +
                $"has={FormatResource(s.MaxResource)} State={s.MaxStateText} " +
                $"CurrentCap={FmtTons(s.MaxCurrentCap)} VanillaCap={FmtTons(s.MaxVanillaCap)} " +
                $"Prefab='{FormatPrefabDisplayName(s.MaxPrefabName)}'");

            if (topOverCurrent.Count > 0)
            {
                for (int i = 0; i < topOverCurrent.Count; i++)
                {
                    BucketHit hit = topOverCurrent[i];

                    LogUtils.Info(
                        Mod.s_Log,
                        () =>
                        $"{Mod.ModTag} {name} OverCap {i + 1}: ENTITY ID {FmtEntity(hit.Entity)} " +
                        $"Amt={FmtTons(hit.Amount)} CurrentCap={FmtTons(hit.CurrentCap)} VanillaCap={FmtTons(hit.VanillaCap)} " +
                        $"has={FormatResource(hit.CarriedResource)} State={hit.StateText} " +
                        $"Prefab='{FormatPrefabDisplayName(hit.PrefabName)}'");
                }

                return;
            }

            for (int i = 0; i < topOverVanilla.Count; i++)
            {
                BucketHit hit = topOverVanilla[i];

                LogUtils.Info(
                    Mod.s_Log,
                    () =>
                    $"{Mod.ModTag} {name} Top{i + 1}: ENTITY ID {FmtEntity(hit.Entity)} " +
                    $"Amt={FmtTons(hit.Amount)} CurrentCap={FmtTons(hit.CurrentCap)} VanillaCap={FmtTons(hit.VanillaCap)} " +
                    $"has={FormatResource(hit.CarriedResource)} State={hit.StateText} " +
                    $"Prefab='{FormatPrefabDisplayName(hit.PrefabName)}'");
            }
        }

        private static string FmtBucketSummary(BucketStats s)
        {
            if (s.Carrying <= 0)
            {
                return "0/0 (0%)";
            }

            float pct = 100f * s.OverVanilla / s.Carrying;
            return $"{s.OverVanilla}/{s.Carrying} ({pct:0.#}%)";
        }

        private static string FmtCategorySummary(int overVanilla, int carrying)
        {
            if (carrying <= 0)
            {
                return "none";
            }

            float pct = 100f * overVanilla / carrying;
            return $"{overVanilla}/{carrying} ({pct:0.#}%)";
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

        private static string FormatPrefabDisplayName(string prefabName)
        {
            if (string.Equals(prefabName, "CoalTruck01", StringComparison.OrdinalIgnoreCase))
            {
                return "DumpTruck (CoalTruck01)";
            }

            if (string.Equals(prefabName, "OilTruck01", StringComparison.OrdinalIgnoreCase))
            {
                return "RawTruck (OilTruck01)";
            }

            return prefabName;
        }

        private struct BucketStats
        {
            public int Seen;
            public int Carrying;
            public int OverVanilla;
            public int OverCurrentCap;

            public int MaxAmount;
            public string MaxPrefabName;
            public Entity MaxEntity;
            public int MaxVanillaCap;
            public int MaxCurrentCap;
            public Resource MaxResource;
            public string MaxStateText;
        }

        private struct BucketHit
        {
            public Entity Entity;
            public int Amount;
            public int VanillaCap;
            public int CurrentCap;
            public Resource CarriedResource;
            public string PrefabName;
            public string StateText;
        }
    }
}
