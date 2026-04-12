// File: Systems/Probes/PrefabScanSystem.cs
// Purpose: One-shot prefab scan triggered by OptionsUI button.
// Output: Writes report to {EnvPath.kUserDataPath}/ModsData/PublicWorksPlus/ScanReport-Prefabs.txt
// Notes:
// - Runs only when requested (PrefabScanState.RequestScan()).
// - Uses SystemAPI.Query + SystemAPI.QueryBuilder.
// - Deduped + capped to prevent giant outputs and logger issues.
// - Logs only a summary line to the mod log (no spam).

namespace PublicWorksPlus
{
    using Colossal.PSI.Environment; // EnvPath
    using Game;
    using Game.Companies;           // TransportCompanyData
    using Game.Economy;             // Resource
    using Game.Net;                 // LaneCondition (LIVE lanes)
    using Game.Prefabs;             // PrefabSystem, PrefabBase, *Data, CarTrailerType, PrefabRef
    using Game.Routes;              // RouteModifierData, RouteModifierType, TransportType
    using Game.SceneFlow;           // GameManager
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;       // Stopwatch
    using System.IO;
    using System.Text;
    using Unity.Collections;        // Allocator, NativeArray
    using Unity.Entities;

    public sealed partial class PrefabScanSystem : GameSystemBase
    {
        private PrefabSystem m_PrefabSystem = null!;
        private EntityQuery m_ConfigQuery;

        private const int kMaxLines = 10000;
        private const int kMaxChars = 1 * 1024 * 1024; // ~1MB
        private const int kMaxKeywordMatches = 600;    // keywords are hints only
        private const int kDeliveryBucketCount = 5;

        // Keep keywords narrow; broad terms explode output.
        private static readonly string[] s_Keywords = new string[]
        {
            "deliveryvan",
            "trucktractor",
            "motorbike",
            "roadmaintenance",
            "parkmaintenance",
            "industrialaquaculturehub",
            "aquaculture",
        };

        // Exclude common prop/LOD/name noise to keep keyword results useful.
        private static readonly string[] s_ExcludeTokens = new string[]
        {
            "Crane", "Tomestone", "StandingStone", "Crapfish", "PileStone", "Pilecoal", "Billboard", "Sign", "Poster", "NetBasket", "NetBox",
            "GasStation", "FarmCage", "FarmPontoon", "FishTub", "FlyFish", "FarmFilterSystem"
        };

        private struct TransitDefaultsStats
        {
            public int Count;
            public float MinInterval;
            public float MaxInterval;
            public float MinStop;
            public float MaxStop;

            public void InitFrom(TransportLineData d)
            {
                Count = 1;
                MinInterval = d.m_DefaultVehicleInterval;
                MaxInterval = d.m_DefaultVehicleInterval;
                MinStop = d.m_StopDuration;
                MaxStop = d.m_StopDuration;
            }

            public void Add(TransportLineData d)
            {
                Count++;

                float interval = d.m_DefaultVehicleInterval;
                float stop = d.m_StopDuration;

                if (interval < MinInterval) MinInterval = interval;
                if (interval > MaxInterval) MaxInterval = interval;

                if (stop < MinStop) MinStop = stop;
                if (stop > MaxStop) MaxStop = stop;
            }
        }

        private struct LiveDeliveryBucketStats
        {
            public int Seen;
            public int Carrying;
            public int OverVanilla;
            public int MaxAmount;
            public string MaxPrefabName;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();

            // Needed for VehicleCountPolicy section; do not delete.
            m_ConfigQuery = SystemAPI.QueryBuilder()
                .WithAll<UITransportConfigurationData>()
                .Build();

            // Ensure this system only exists when prefab world exists.
            RequireForUpdate(SystemAPI.QueryBuilder().WithAll<PrefabData>().Build());

            Enabled = false;
        }

        protected override void OnUpdate()
        {
            if (PrefabScanState.CurrentPhase != PrefabScanState.Phase.Requested)
            {
                Enabled = false;
                return;
            }

            GameManager gm = GameManager.instance;
            if (gm == null || !gm.gameMode.IsGame())
            {
                PrefabScanState.MarkFailed(PrefabScanState.FailCode.NoCityLoaded, null);
                Enabled = false;
                return;
            }

            PrefabScanState.MarkRunning();

            Stopwatch sw = Stopwatch.StartNew();

            int transitLinePrefabTotal = 0;
            int keywordMatches = 0;
            int extractorCompanies = 0;
            int deliveryTotal = 0;
            int depotTotal = 0;
            int cargoTotal = 0;
            int laneTotal = 0;
            int mvTotal = 0;

            try
            {
                StringBuilder sb = new StringBuilder(256 * 1024);
                int lines = 0;
                bool truncated = false;

                void Append(string line)
                {
                    AppendCapped(sb, ref lines, ref truncated, line);
                }

                string NameOf(Entity e) => PrefabNameUtil.GetNameSafe(m_PrefabSystem, e);

                // -----------------------------
                // Header
                // -----------------------------
                Append($"Prefab Scan Report for: {Mod.ModName} {Mod.ModVersion}");
                Append($"Timestamp (local): {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Append("");

                // -----------------------------
                // Lane wear prefabs
                // -----------------------------
                const float kUpdatesPerDay = 16f;
                const int kMaxLaneDetails = 250;

                Append("== Lane wear (LaneDeteriorationData prefabs) ==");
                Append("Wear sources:");
                Append("- Time wear: LaneCondition.m_Wear += (1/16) * TimeFactor per deterioration tick.");
                Append("- Traffic wear: Car/Train Navigation adds SideEffects.x * TrafficFactor when vehicles traverse lanes.");
                Append("");

                int laneListed = 0;

                float minTf = float.MaxValue;
                float maxTf = float.MinValue;
                float minTraf = float.MaxValue;
                float maxTraf = float.MinValue;

                foreach ((RefRO<LaneDeteriorationData> laneRef, Entity e) in SystemAPI
                             .Query<RefRO<LaneDeteriorationData>>()
                             .WithAll<PrefabData>()
                             .WithEntityAccess())
                {
                    if (truncated) break;

                    laneTotal++;

                    LaneDeteriorationData cur = laneRef.ValueRO;
                    float tf = cur.m_TimeFactor;
                    float traf = cur.m_TrafficFactor;

                    if (tf < minTf) minTf = tf;
                    if (tf > maxTf) maxTf = tf;

                    if (traf < minTraf) minTraf = traf;
                    if (traf > maxTraf) maxTraf = traf;

                    if (laneListed < kMaxLaneDetails)
                    {
                        float vanTf = float.NaN;
                        float vanTraf = float.NaN;

                        if (m_PrefabSystem.TryGetPrefab(e, out PrefabBase pb) &&
                            pb.TryGet(out Game.Prefabs.LaneDeterioration author))
                        {
                            vanTf = author.m_TimeDeterioration;
                            vanTraf = author.m_TrafficDeterioration;
                        }

                        float xTime = (!float.IsNaN(vanTf) && vanTf > 0f) ? (tf / vanTf) : float.NaN;
                        float xTraf2 = (!float.IsNaN(vanTraf) && vanTraf > 0f) ? (traf / vanTraf) : float.NaN;

                        float expPerTick = tf / kUpdatesPerDay;

                        Append(
                            $"- {NameOf(e)} ({e.Index}:{e.Version}) " +
                            $"Vanilla (Time={Fmt(vanTf)}, Traffic={Fmt(vanTraf)}) " +
                            $"Current (Time={tf:0.###}, Traffic={traf:0.###}) " +
                            $"xTime={Fmt(xTime)} xTraffic={Fmt(xTraf2)} ExpΔ(Time)/Tick={expPerTick:0.###}");

                        laneListed++;
                    }
                }

                if (laneTotal > laneListed)
                {
                    Append($"(details capped) Printed={laneListed} of Total={laneTotal} (cap={kMaxLaneDetails}).");
                }

                Append("");
                Append(laneTotal > 0
                    ? $"== Lane wear summary: Total={laneTotal} TimeFactor(min={minTf:0.###}, max={maxTf:0.###}) TrafficFactor(min={minTraf:0.###}, max={maxTraf:0.###})"
                    : "Lane wear summary: Total=0");
                Append("");

                AppendLiveLaneUsage(sb, ref lines, ref truncated);
                Append("");

                // -----------------------------
                // Transit line defaults
                // -----------------------------
                Append("== Transit lines (vanilla timing inputs) ==");
                Append("Vehicle targets are based on route time estimate (segment durations + stop count).");
                Append("");

                Dictionary<TransportType, TransitDefaultsStats> perType = new Dictionary<TransportType, TransitDefaultsStats>();

                foreach ((RefRO<TransportLineData> lineRef, Entity entity) in SystemAPI
                             .Query<RefRO<TransportLineData>>()
                             .WithAll<PrefabData>()
                             .WithEntityAccess())
                {
                    if (truncated) break;

                    transitLinePrefabTotal++;

                    TransportLineData d = lineRef.ValueRO;
                    TransportType type = d.m_TransportType;

                    if (!perType.TryGetValue(type, out TransitDefaultsStats stats))
                    {
                        stats = default;
                        stats.InitFrom(d);
                        perType[type] = stats;
                    }
                    else
                    {
                        stats.Add(d);
                        perType[type] = stats;
                    }
                }

                Append("-- TransportLineData prefab defaults (by TransportType) --");
                if (perType.Count == 0)
                {
                    Append("No TransportLineData prefabs found.");
                }
                else
                {
                    foreach (KeyValuePair<TransportType, TransitDefaultsStats> kvp in perType)
                    {
                        TransportType type = kvp.Key;
                        TransitDefaultsStats s2 = kvp.Value;

                        Append(
                            $"- {type}: Count={s2.Count} " +
                            $"DefaultVehicleInterval(min={s2.MinInterval:0.###}, max={s2.MaxInterval:0.###}) " +
                            $"StopDuration(min={s2.MinStop:0.###}, max={s2.MaxStop:0.###})");
                    }
                }

                Append("");

                // -----------------------------
                // VehicleCountPolicy
                // -----------------------------
                Append("-- Transit Line Slider Limits Policy (RouteModifierType.VehicleInterval) --");
                if (m_ConfigQuery.IsEmptyIgnoreFilter)
                {
                    Append("UITransportConfigurationData not found; policy entity could not be resolved.");
                }
                else
                {
                    UITransportConfigurationPrefab config =
                        m_PrefabSystem.GetSingletonPrefab<UITransportConfigurationPrefab>(m_ConfigQuery);

                    Entity policyEntity = m_PrefabSystem.GetEntity(config.m_VehicleCountPolicy);

                    if (policyEntity == Entity.Null || !SystemAPI.Exists(policyEntity))
                    {
                        Append("VehicleCountPolicy entity could not be resolved.");
                    }
                    else if (!SystemAPI.HasBuffer<RouteModifierData>(policyEntity))
                    {
                        Append("VehicleCountPolicy has no RouteModifierData buffer.");
                    }
                    else
                    {
                        DynamicBuffer<RouteModifierData> buf = SystemAPI.GetBuffer<RouteModifierData>(policyEntity);
                        bool foundVehicleInterval = false;

                        for (int i = 0; i < buf.Length; i++)
                        {
                            RouteModifierData item = buf[i];
                            if (item.m_Type != RouteModifierType.VehicleInterval)
                                continue;

                            foundVehicleInterval = true;
                            Append($"Mode={item.m_Mode} Range(input)={item.m_Range.min:0.###}..{item.m_Range.max:0.###}");

                            if (item.m_Mode == ModifierValueMode.InverseRelative)
                            {
                                float appliedMin = InverseRelativeAppliedFromInput(item.m_Range.min);
                                float appliedMax = InverseRelativeAppliedFromInput(item.m_Range.max);
                                Append($"Range(appliedΔ @ endpoints) inputMin→{appliedMin:0.###}, inputMax→{appliedMax:0.###}");
                            }

                            break;
                        }

                        if (!foundVehicleInterval)
                        {
                            Append("No VehicleInterval modifier found in VehicleCountPolicy.");
                        }
                    }
                }

                Append("");

                // -----------------------------
                // Delivery truck prefabs
                // -----------------------------
                int semi = 0;
                int van = 0;
                int raw = 0;
                int bike = 0;
                int other = 0;

                int unclassifiedPrinted = 0;
                const int kMaxUnclassifiedDetails = 100;

                ComponentLookup<DeliveryTruckData> deliveryLookup = SystemAPI.GetComponentLookup<DeliveryTruckData>(isReadOnly: true);
                ComponentLookup<CarTractorData> tractorLookup = SystemAPI.GetComponentLookup<CarTractorData>(isReadOnly: true);
                ComponentLookup<CarTrailerData> trailerLookup = SystemAPI.GetComponentLookup<CarTrailerData>(isReadOnly: true);

                Append("== DeliveryTruckData Prefabs ==");

                foreach ((RefRO<DeliveryTruckData> truckRef, Entity e) in SystemAPI
                             .Query<RefRO<DeliveryTruckData>>()
                             .WithAll<PrefabData>()
                             .WithEntityAccess())
                {
                    if (truncated) break;

                    deliveryTotal++;
                    DeliveryTruckData dt = truckRef.ValueRO;

                    int vanillaCap = dt.m_CargoCapacity;
                    if (m_PrefabSystem.TryGetPrefab(e, out PrefabBase pb) &&
                        pb.TryGet(out Game.Prefabs.DeliveryTruck baseTruck))
                    {
                        vanillaCap = baseTruck.m_CargoCapacity;
                    }

                    VehicleHelpers.GetTrailerTypeInfo(
                        tractorLookup,
                        trailerLookup,
                        e,
                        out bool hasTractor,
                        out CarTrailerType tractorType,
                        out bool hasTrailer,
                        out CarTrailerType trailerType);

                    string name = NameOf(e);

                    VehicleHelpers.DeliveryBucket bucket = VehicleHelpers.ClassifyDeliveryTruckPrefab(
                        name,
                        vanillaCap,
                        dt.m_TransportedResources,
                        hasTractor,
                        tractorType,
                        hasTrailer,
                        trailerType);

                    switch (bucket)
                    {
                        case VehicleHelpers.DeliveryBucket.Semi: semi++; break;
                        case VehicleHelpers.DeliveryBucket.Van: van++; break;
                        case VehicleHelpers.DeliveryBucket.RawMaterials: raw++; break;
                        case VehicleHelpers.DeliveryBucket.Motorbike: bike++; break;
                        default: other++; break;
                    }

                    Append(
                        $"- {name} ({e.Index}:{e.Version}) Bucket={bucket} " +
                        $"VanillaCap={vanillaCap} CurCap={dt.m_CargoCapacity} " +
                        $"Resources={dt.m_TransportedResources} " +
                        $"Tractor={hasTractor}:{tractorType} Trailer={hasTrailer}:{trailerType}");

                    if (bucket == VehicleHelpers.DeliveryBucket.Other && unclassifiedPrinted < kMaxUnclassifiedDetails)
                    {
                        Append(
                            $"!! UNCLASSIFIED DeliveryTruckData: name='{name}' " +
                            $"VanillaCap={vanillaCap} CurCap={dt.m_CargoCapacity} " +
                            $"Resources={dt.m_TransportedResources} " +
                            $"Tractor={hasTractor}:{tractorType} Trailer={hasTrailer}:{trailerType}");

                        unclassifiedPrinted++;
                    }
                }

                if (other > 0)
                {
                    Append($"(Unclassified details printed: {unclassifiedPrinted} / {other} cap={kMaxUnclassifiedDetails})");
                }

                Append($"Delivery summary: Total={deliveryTotal} Semi={semi} Van={van} Raw={raw} Motorbike={bike} Other={other}");
                Append("");

                AppendLiveDeliveryCargoSnapshot(
                    sb,
                    ref lines,
                    ref truncated,
                    deliveryLookup,
                    tractorLookup,
                    trailerLookup);

                // -----------------------------
                // Maintenance vehicles
                // -----------------------------
                Append("== MaintenanceVehicleData Prefabs ==");
                foreach ((RefRO<MaintenanceVehicleData> mvRef, Entity e) in SystemAPI
                             .Query<RefRO<MaintenanceVehicleData>>()
                             .WithAll<PrefabData>()
                             .WithEntityAccess())
                {
                    if (truncated) break;

                    mvTotal++;
                    MaintenanceVehicleData mv = mvRef.ValueRO;

                    int vanillaCap = mv.m_MaintenanceCapacity;
                    int vanillaRate = mv.m_MaintenanceRate;

                    if (m_PrefabSystem.TryGetPrefab(e, out PrefabBase pb) &&
                        pb.TryGet(out Game.Prefabs.MaintenanceVehicle baseMv))
                    {
                        vanillaCap = baseMv.m_MaintenanceCapacity;
                        vanillaRate = baseMv.m_MaintenanceRate;
                    }

                    Append($"- {NameOf(e)} ({e.Index}:{e.Version}) Type={mv.m_MaintenanceType} VanillaCap={vanillaCap} CurCap={mv.m_MaintenanceCapacity} VanillaRate={vanillaRate} CurRate={mv.m_MaintenanceRate}");
                }
                Append($"MaintenanceVehicle summary: Total={mvTotal}");
                Append("");

                // -----------------------------
                // Maintenance depots
                // -----------------------------
                Append("== MaintenanceDepotData Prefabs ==");
                foreach ((RefRO<MaintenanceDepotData> depotRef, Entity e) in SystemAPI
                             .Query<RefRO<MaintenanceDepotData>>()
                             .WithAll<PrefabData>()
                             .WithEntityAccess())
                {
                    if (truncated) break;

                    depotTotal++;
                    MaintenanceDepotData md = depotRef.ValueRO;

                    int vanillaVehicles = md.m_VehicleCapacity;
                    if (m_PrefabSystem.TryGetPrefab(e, out PrefabBase pb) &&
                        pb.TryGet(out Game.Prefabs.MaintenanceDepot baseDepot))
                    {
                        vanillaVehicles = baseDepot.m_VehicleCapacity;
                    }

                    Append($"- {NameOf(e)} ({e.Index}:{e.Version}) Type={md.m_MaintenanceType} VanillaVehicles={vanillaVehicles} CurVehicles={md.m_VehicleCapacity}");
                }
                Append($"MaintenanceDepot summary: Total={depotTotal}");
                Append("");

                // -----------------------------
                // Cargo stations (company fleet)
                // -----------------------------
                Append("== Cargo Transport Stations (CargoTransportStationData + TransportCompanyData) ==");
                foreach ((RefRO<TransportCompanyData> tcRef, Entity e) in SystemAPI
                             .Query<RefRO<TransportCompanyData>>()
                             .WithAll<CargoTransportStationData, PrefabData>()
                             .WithEntityAccess())
                {
                    if (truncated) break;

                    cargoTotal++;
                    TransportCompanyData tc = tcRef.ValueRO;

                    int vanillaMax = tc.m_MaxTransports;
                    if (m_PrefabSystem.TryGetPrefab(e, out PrefabBase pb) &&
                        pb.TryGet(out CargoTransportStation station))
                    {
                        vanillaMax = station.transports;
                    }

                    Append($"- {NameOf(e)} ({e.Index}:{e.Version}) VanillaMaxTransports={vanillaMax} CurMaxTransports={tc.m_MaxTransports}");
                }
                Append($"Cargo station summary: Total={cargoTotal}");
                Append("");

                // -----------------------------
                // Industrial extractor companies (fleet slider targets)
                // -----------------------------
                Append("== Industrial Extractor TransportCompanies (for Extractor trucks slider) ==");
                Append("Filter: name starts with Industrial_ AND contains Extractor/Coal/Stone/Mine/Quarry. Skips CurMaxTransports=0. Deduped by name.");

                HashSet<string> seenExtractors = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach ((RefRO<TransportCompanyData> tcRef, Entity e) in SystemAPI
                             .Query<RefRO<TransportCompanyData>>()
                             .WithAll<PrefabData>()
                             .WithEntityAccess())
                {
                    if (truncated) break;

                    string name = NameOf(e);
                    if (IsExcludedName(name))
                        continue;

                    if (!IsTargetIndustrialExtractorCompany(name))
                        continue;

                    TransportCompanyData tc = tcRef.ValueRO;

                    if (tc.m_MaxTransports == 0)
                        continue;

                    if (!seenExtractors.Add(name))
                        continue;

                    extractorCompanies++;
                    Append($"- {name} ({e.Index}:{e.Version}) CurMaxTransports={tc.m_MaxTransports}");
                }

                Append($"Industrial extractor summary: Unique={extractorCompanies}");
                Append("");

#if DEBUG
                // -----------------------------
                // Keyword scan
                // -----------------------------
                Append("== Keyword Matches (deduped, capped) ==");
                Append("Hints for discovering relevant prefabs. Keep narrow ('truck' returns too much)");

                HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach ((RefRO<PrefabData> _, Entity e) in SystemAPI
                             .Query<RefRO<PrefabData>>()
                             .WithEntityAccess())
                {
                    if (truncated) break;
                    if (keywordMatches >= kMaxKeywordMatches) break;

                    string n = NameOf(e);
                    if (string.IsNullOrEmpty(n)) continue;

                    if (IsExcludedName(n))
                        continue;

                    string lower = n.ToLowerInvariant();

                    int hitIndex = -1;
                    for (int i = 0; i < s_Keywords.Length; i++)
                    {
                        if (lower.Contains(s_Keywords[i]))
                        {
                            hitIndex = i;
                            break;
                        }
                    }

                    if (hitIndex < 0) continue;
                    if (!seen.Add(n)) continue;

                    keywordMatches++;
                    Append($"- {n} ({e.Index}:{e.Version}) hit='{s_Keywords[hitIndex]}'");
                }

                Append($"Keyword match summary: UniqueMatches={keywordMatches} Cap={kMaxKeywordMatches}");
                Append("");
#endif

                // -----------------------------
                // Write report
                // -----------------------------
                string reportPath = GetReportPath();
                string dir = Path.GetDirectoryName(reportPath) ?? string.Empty;
                if (dir.Length > 0)
                {
                    Directory.CreateDirectory(dir);
                }

                File.WriteAllText(reportPath, sb.ToString(), Encoding.UTF8);

                sw.Stop();

                PrefabScanState.MarkDone(sw.Elapsed, reportPath);

                Mod.s_Log.Info($"{Mod.ModTag} Prefab scan done in {sw.Elapsed.TotalSeconds:0.0}s. Report: {reportPath}");
                Mod.s_Log.Info(
                    $"{Mod.ModTag} PrefabScan counts (prefab entities): " +
                    $"TransitLines={transitLinePrefabTotal}, DeliveryTrucks={deliveryTotal}, " +
                    $"MaintVehicles={mvTotal}, MaintDepots={depotTotal}, CargoStations={cargoTotal}, " +
                    $"ExtractorCompanies={extractorCompanies}, LaneWearPrefabs={laneTotal}, KeywordHits={keywordMatches}");
            }
            catch (Exception ex)
            {
                sw.Stop();
                PrefabScanState.MarkFailed(PrefabScanState.FailCode.Exception, $"{ex.GetType().Name}: {ex.Message}");
                Mod.s_Log.Warn($"{Mod.ModTag} Prefab scan failed: {ex.GetType().Name}: {ex.Message}");
            }

            Enabled = false;
        }

        private void AppendLiveLaneUsage(StringBuilder sb, ref int lines, ref bool truncated)
        {
            if (truncated)
                return;

            AppendCapped(sb, ref lines, ref truncated, "== Live lane usage (LaneCondition + PrefabRef) ==");
            AppendCapped(sb, ref lines, ref truncated, "Counts live lane entities grouped by PrefabRef.m_Prefab (lane prefab).");
            AppendCapped(sb, ref lines, ref truncated, "Proof: small set of lane prefabs power many road types.");
            AppendCapped(sb, ref lines, ref truncated, "");

            HashSet<Entity> wearPrefabs = new HashSet<Entity>();

            foreach ((RefRO<LaneDeteriorationData> _, Entity prefabEntity) in SystemAPI
                         .Query<RefRO<LaneDeteriorationData>>()
                         .WithAll<PrefabData>()
                         .WithEntityAccess())
            {
                wearPrefabs.Add(prefabEntity);
            }

            Dictionary<Entity, int> counts = new Dictionary<Entity, int>(64);
            long liveLaneTotal = 0;

            foreach (RefRO<PrefabRef> prefabRefRO in SystemAPI
                         .Query<RefRO<PrefabRef>>()
                         .WithAll<LaneCondition>()
                         .WithNone<PrefabData>())
            {
                Entity prefab = prefabRefRO.ValueRO.m_Prefab;
                liveLaneTotal++;

                if (counts.TryGetValue(prefab, out int c))
                    counts[prefab] = c + 1;
                else
                    counts[prefab] = 1;
            }

            if (liveLaneTotal == 0 || counts.Count == 0)
            {
                AppendCapped(sb, ref lines, ref truncated, "No live lanes found (unexpected).");
                return;
            }

            long covered = 0;
            foreach (KeyValuePair<Entity, int> kvp in counts)
            {
                if (wearPrefabs.Contains(kvp.Key))
                    covered += kvp.Value;
            }

            float pct = (float)covered * 100f / (float)liveLaneTotal;

            AppendCapped(sb, ref lines, ref truncated, "------------------------------------------");
            AppendCapped(sb, ref lines, ref truncated, $"Live lanes summary: LiveLanes={liveLaneTotal:n0} UniqueLanePrefabs={counts.Count:n0}");
            AppendCapped(sb, ref lines, ref truncated, $"Mod Coverage of LaneDeteriorationData prefabs: {covered:n0}/{liveLaneTotal:n0} ({pct:0.0}%)");
            AppendCapped(sb, ref lines, ref truncated, "");

            const int kTop = 30;

            List<KeyValuePair<Entity, int>> top = new List<KeyValuePair<Entity, int>>(counts);
            top.Sort((a, b) => b.Value.CompareTo(a.Value));

            int printed = 0;
            for (int i = 0; i < top.Count && printed < kTop; i++)
            {
                KeyValuePair<Entity, int> kvp = top[i];
                string name = PrefabNameUtil.GetNameSafe(m_PrefabSystem, kvp.Key);
                bool isWear = wearPrefabs.Contains(kvp.Key);

                AppendCapped(sb, ref lines, ref truncated, $"- {name} ({kvp.Key.Index}:{kvp.Key.Version}) UsedByLanes={kvp.Value:n0} WearPrefab={isWear}");
                printed++;
            }
        }

        private void AppendLiveDeliveryCargoSnapshot(
            StringBuilder sb,
            ref int lines,
            ref bool truncated,
            ComponentLookup<DeliveryTruckData> deliveryLookup,
            ComponentLookup<CarTractorData> tractorLookup,
            ComponentLookup<CarTrailerData> trailerLookup)
        {
            if (truncated)
                return;

            AppendCapped(sb, ref lines, ref truncated, "== Live delivery cargo snapshot (current city) ==");
            AppendCapped(sb, ref lines, ref truncated, "Reads live DeliveryTruck.m_Amount and compares it to the vanilla prefab cargo cap.");
            AppendCapped(sb, ref lines, ref truncated, "This is a one-time snapshot of currently spawned vehicles, not a long-running average.");
            AppendCapped(sb, ref lines, ref truncated, "");

            EntityQuery q = GetEntityQuery(
                ComponentType.ReadOnly<Game.Vehicles.DeliveryTruck>(),
                ComponentType.ReadOnly<PrefabRef>());

            if (q.IsEmptyIgnoreFilter)
            {
                AppendCapped(sb, ref lines, ref truncated, "No live delivery trucks found in this snapshot.");
                AppendCapped(sb, ref lines, ref truncated, "");
                return;
            }

            LiveDeliveryBucketStats[] bucketStats = new LiveDeliveryBucketStats[kDeliveryBucketCount];
            for (int i = 0; i < bucketStats.Length; i++)
            {
                bucketStats[i].MaxPrefabName = string.Empty;
            }

            int scanned = 0;
            int relevant = 0;
            int carrying = 0;
            int overVanilla = 0;
            int globalMaxAmount = 0;
            string globalMaxPrefabName = string.Empty;

            using (NativeArray<Entity> entities = q.ToEntityArray(Allocator.Temp))
            {
                for (int i = 0; i < entities.Length; i++)
                {
                    if (truncated)
                        break;

                    Entity liveEntity = entities[i];
                    scanned++;

                    Game.Vehicles.DeliveryTruck truck = EntityManager.GetComponentData<Game.Vehicles.DeliveryTruck>(liveEntity);
                    int amount = truck.m_Amount;

                    PrefabRef prefabRef = EntityManager.GetComponentData<PrefabRef>(liveEntity);
                    Entity prefab = prefabRef.m_Prefab;

                    int vanillaCap = GetVanillaDeliveryVanillaCap(prefab);
                    if (vanillaCap <= 0)
                        continue;

                    relevant++;

                    Resource transported = Resource.NoResource;
                    if (deliveryLookup.TryGetComponent(prefab, out DeliveryTruckData deliveryData))
                    {
                        transported = deliveryData.m_TransportedResources;
                    }

                    VehicleHelpers.GetTrailerTypeInfo(
                        tractorLookup,
                        trailerLookup,
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

                    int bucketIndex = (int)bucket;
                    if (bucketIndex < 0 || bucketIndex >= kDeliveryBucketCount)
                    {
                        bucketIndex = (int)VehicleHelpers.DeliveryBucket.Other;
                    }

                    LiveDeliveryBucketStats stats = bucketStats[bucketIndex];
                    stats.Seen++;

                    if (amount > 0)
                    {
                        carrying++;
                        stats.Carrying++;

                        if (amount > vanillaCap)
                        {
                            overVanilla++;
                            stats.OverVanilla++;
                        }

                        if (amount > stats.MaxAmount)
                        {
                            stats.MaxAmount = amount;
                            stats.MaxPrefabName = prefabName;
                        }

                        if (amount > globalMaxAmount)
                        {
                            globalMaxAmount = amount;
                            globalMaxPrefabName = prefabName;
                        }
                    }

                    bucketStats[bucketIndex] = stats;
                }
            }

            AppendCapped(sb, ref lines, ref truncated, $"Live delivery summary: Scanned={scanned} Relevant={relevant} Carrying={carrying} OverVanilla={overVanilla}");

            if (carrying == 0)
            {
                AppendCapped(sb, ref lines, ref truncated, "Result: no carrying delivery trucks found in this snapshot.");
            }
            else if (overVanilla > 0)
            {
                AppendCapped(sb, ref lines, ref truncated, $"Result: FOUND live trucks above vanilla capacity. HighestObserved={FmtTons(globalMaxAmount)} Prefab='{globalMaxPrefabName}'");
            }
            else
            {
                AppendCapped(sb, ref lines, ref truncated, $"Result: no live trucks above vanilla capacity found in this snapshot. HighestObserved={FmtTons(globalMaxAmount)} Prefab='{globalMaxPrefabName}'");
            }

            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Semi", bucketStats[(int)VehicleHelpers.DeliveryBucket.Semi]);
            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Van", bucketStats[(int)VehicleHelpers.DeliveryBucket.Van]);
            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Raw", bucketStats[(int)VehicleHelpers.DeliveryBucket.RawMaterials]);
            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Motorbike", bucketStats[(int)VehicleHelpers.DeliveryBucket.Motorbike]);
            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Other", bucketStats[(int)VehicleHelpers.DeliveryBucket.Other]);
            AppendCapped(sb, ref lines, ref truncated, "");
        }

        private int GetVanillaDeliveryVanillaCap(Entity prefab)
        {
            if (PrefabComponentUtil.TryGetComponent(m_PrefabSystem, prefab, out Game.Prefabs.DeliveryTruck truck))
            {
                return truck.m_CargoCapacity;
            }

            return 0;
        }

        private static void AppendLiveDeliveryBucket(
            StringBuilder sb,
            ref int lines,
            ref bool truncated,
            string bucketName,
            LiveDeliveryBucketStats stats)
        {
            if (stats.Seen == 0)
            {
                AppendCapped(sb, ref lines, ref truncated, $"- {bucketName}: none");
                return;
            }

            float pct = stats.Carrying > 0
                ? (100f * stats.OverVanilla / stats.Carrying)
                : 0f;

            AppendCapped(
                sb,
                ref lines,
                ref truncated,
                $"- {bucketName}: seen={stats.Seen} carrying={stats.Carrying} " +
                $"overVanilla={stats.OverVanilla} ({pct:0.#}% of carrying) " +
                $"maxAmount={FmtTons(stats.MaxAmount)} prefab='{stats.MaxPrefabName}'");
        }

        private static void AppendCapped(StringBuilder sb, ref int lines, ref bool truncated, string line)
        {
            if (truncated)
                return;

            if (lines >= kMaxLines || sb.Length >= kMaxChars)
            {
                truncated = true;
                sb.AppendLine("!! TRUNCATED: Output hit cap (lines or size). Reduce scope (keywords/detail).");
                lines++;
                return;
            }

            sb.AppendLine(line);
            lines++;
        }

        private static float InverseRelativeAppliedFromInput(float input)
        {
            return (-input) / (1f + input);
        }

        private static string Fmt(float v)
        {
            return float.IsNaN(v) ? "n/a" : v.ToString("0.###");
        }

        private static string FmtTons(int amount)
        {
            float tons = amount / 1000f;
            return $"{amount} (~{tons:0.##}t)";
        }

        private static bool IsTargetIndustrialExtractorCompany(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            if (!name.StartsWith("Industrial_", StringComparison.OrdinalIgnoreCase))
                return false;

            if (name.IndexOf("Extractor", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            if (name.IndexOf("Coal", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            if (name.IndexOf("Stone", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            if (name.IndexOf("Mine", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            if (name.IndexOf("Quarry", StringComparison.OrdinalIgnoreCase) >= 0) return true;

            return false;
        }

        private static bool IsExcludedName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            // High-noise buckets (models/people/LODs).
            if (name.StartsWith("Male_", StringComparison.OrdinalIgnoreCase)) return true;
            if (name.StartsWith("Female_", StringComparison.OrdinalIgnoreCase)) return true;
            if (name.IndexOf("_LOD", StringComparison.OrdinalIgnoreCase) >= 0) return true;
            if (name.IndexOf("Mesh", StringComparison.OrdinalIgnoreCase) >= 0) return true;

            for (int i = 0; i < s_ExcludeTokens.Length; i++)
            {
                if (name.IndexOf(s_ExcludeTokens[i], StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            }

            return false;
        }

        private static string GetReportPath()
        {
            string root = EnvPath.kUserDataPath;
            return Path.Combine(root, "ModsData", Mod.ModId, "ScanReport-Prefabs.txt");
        }
    }
}
