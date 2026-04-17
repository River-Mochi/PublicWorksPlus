// File: Systems/Probes/PrefabScanSystem.cs
// Purpose: One-shot prefab scan triggered by Options UI button.
// Output: Writes report to {EnvPath.kUserDataPath}/ModsData/PublicWorksPlus/ScanReport-Prefabs.txt
// Notes:
// - Runs only when requested.
// - Main scan flow stays here.
// - Helper/report methods live in PrefabScanSystem.ReportSections.cs to keep this file shorter.

namespace PublicWorksPlus
{
    using Game;
    using Game.Companies;
    using Game.Net;
    using Game.Prefabs;
    using Game.Routes;
    using Game.SceneFlow;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Unity.Collections;
    using Unity.Entities;

    public sealed partial class PrefabScanSystem : GameSystemBase
    {
        private PrefabSystem m_PrefabSystem = null!;
        private EntityQuery m_ConfigQuery;

        private const int kMaxLines = 10000;
        private const int kMaxChars = 1 * 1024 * 1024; // ~1MB
        private const int kMaxKeywordMatches = 600;
        private const int kDeliveryBucketCount = 5;

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

            m_ConfigQuery = SystemAPI.QueryBuilder()
                .WithAll<UITransportConfigurationData>()
                .Build();

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

                // Header
                Append($"Prefab Scan Report for: {Mod.ModName} {Mod.ModVersion}");
                Append($"Timestamp (local): {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Append("");

                // Lane wear prefabs
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

                // Transit line defaults
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

                // VehicleCountPolicy
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

                // Delivery truck prefabs
                int semi = 0;
                int van = 0;
                int raw = 0;
                int bike = 0;
                int other = 0;

                int unclassifiedPrinted = 0;
                const int kMaxUnclassifiedDetails = 100;

                int semiVanillaCap = 0;
                int semiCurCap = 0;

                int vanVanillaCap = 0;
                int vanCurCap = 0;

                int rawVanillaCap = 0;
                int rawCurCap = 0;

                int bikeVanillaCap = 0;
                int bikeCurCap = 0;



                ComponentLookup<DeliveryTruckData> deliveryLookup = SystemAPI.GetComponentLookup<DeliveryTruckData>(isReadOnly: true);
                ComponentLookup<CarTractorData> tractorLookup = SystemAPI.GetComponentLookup<CarTractorData>(isReadOnly: true);
                ComponentLookup<CarTrailerData> trailerLookup = SystemAPI.GetComponentLookup<CarTrailerData>(isReadOnly: true);

                EntityQuery cargoStationWatchQuery = SystemAPI.QueryBuilder()
                    .WithAll<Game.Buildings.CargoTransportStation, PrefabRef>()
                    .Build();

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
                        case VehicleHelpers.DeliveryBucket.Semi:
                            semi++;
                            semiVanillaCap = vanillaCap;
                            if (dt.m_CargoCapacity > semiCurCap) semiCurCap = dt.m_CargoCapacity;
                            break;

                        case VehicleHelpers.DeliveryBucket.Van:
                            van++;
                            vanVanillaCap = vanillaCap;
                            if (dt.m_CargoCapacity > vanCurCap) vanCurCap = dt.m_CargoCapacity;
                            break;

                        case VehicleHelpers.DeliveryBucket.RawMaterials:
                            raw++;
                            rawVanillaCap = vanillaCap;
                            if (dt.m_CargoCapacity > rawCurCap) rawCurCap = dt.m_CargoCapacity;
                            break;

                        case VehicleHelpers.DeliveryBucket.Motorbike:
                            bike++;
                            bikeVanillaCap = vanillaCap;
                            if (dt.m_CargoCapacity > bikeCurCap) bikeCurCap = dt.m_CargoCapacity;
                            break;

                        default:
                            other++;
                            break;
                    }


                    Append(
                        $"- {name} ({e.Index}:{e.Version}) Bucket={bucket} " +
                        $"VanillaCap={FmtCapacityForReport(vanillaCap)} CurCap={FmtCapacityForReport(dt.m_CargoCapacity)} " +
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

                AppendSectionHeader(sb, ref lines, ref truncated, "Current delivery slider results");
                Append($"Semi prefab cap: {FmtCapacityForReport(semiVanillaCap)} -> {FmtCapacityForReport(semiCurCap)}");
                Append($"Van prefab cap: {FmtCapacityForReport(vanVanillaCap)} -> {FmtCapacityForReport(vanCurCap)}");
                Append($"Raw prefab cap: {FmtCapacityForReport(rawVanillaCap)} -> {FmtCapacityForReport(rawCurCap)}");
                Append($"Motorbike prefab cap: {FmtCapacityForReport(bikeVanillaCap)} -> {FmtCapacityForReport(bikeCurCap)}");
                Append("");


                // Maintenance vehicles
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

                // Maintenance depots
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

                // Cargo stations
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

                // Industrial extractor companies
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

                // Cargo station watch comes first, then the live delivery snapshot last.
                CargoStationResourceWatch.Append(
                    this,
                    m_PrefabSystem,
                    cargoStationWatchQuery,
                    sb,
                    ref lines,
                    ref truncated);

                AppendLiveDeliveryCargoSnapshot(
                    sb,
                    ref lines,
                    ref truncated,
                    deliveryLookup,
                    tractorLookup,
                    trailerLookup);


#if DEBUG
                AppendSectionHeader(sb, ref lines, ref truncated, "Debug appendix");
                Append("Keyword matches are debug-only prefab discovery hints.");
                Append("Keep searches narrow ('truck' returns too much).");
                Append("");

                Append("Keyword Matches (deduped, capped)");

                HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // ...
                Append($"Keyword match summary: UniqueMatches={keywordMatches} Cap={kMaxKeywordMatches}");
                Append("");
#endif




                string reportPath = GetReportPath();
                string dir = System.IO.Path.GetDirectoryName(reportPath) ?? string.Empty;
                if (dir.Length > 0)
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                System.IO.File.WriteAllText(reportPath, sb.ToString(), Encoding.UTF8);

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
    }
}
