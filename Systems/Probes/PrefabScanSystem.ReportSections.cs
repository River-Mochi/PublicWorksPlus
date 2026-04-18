// File: Systems/Probes/PrefabScanSystem.ReportSections.cs
// Purpose: Helper/report methods for PrefabScanSystem.
// Notes:
// - Keeps the main scan flow file shorter.
// - Holds shared formatting, filtering, and live-snapshot sections.
// - Delivery research section is intentionally styled more like MagicGarbage:
//   compact summary first, then detailed bucket lines.

namespace PublicWorksPlus
{
    using Colossal.PSI.Environment;
    using Game.Economy;
    using Game.Net;
    using Game.Prefabs;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Unity.Collections;
    using Unity.Entities;

    public sealed partial class PrefabScanSystem
    {
        private struct LiveDispatchCategoryStats
        {
            public int Carrying;
            public int OverVanilla;
        }

        private void AppendLiveLaneUsage(StringBuilder sb, ref int lines, ref bool truncated)
        {
            if (truncated)
                return;

            AppendSectionHeader(sb, ref lines, ref truncated, "Live lane usage");
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

            AppendCapped(sb, ref lines, ref truncated, "");
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

            AppendSectionHeader(sb, ref lines, ref truncated, "Live delivery cargo snapshot (current city)");
            AppendCapped(sb, ref lines, ref truncated, "Reads live DeliveryTruck.m_Amount and compares it to the vanilla prefab cargo cap.");
            AppendCapped(sb, ref lines, ref truncated, "This is a one-time snapshot of currently spawned vehicles, not a long-running average.");
            AppendCapped(sb, ref lines, ref truncated, "");

            EntityQuery q = SystemAPI.QueryBuilder()
                .WithAll<Game.Vehicles.DeliveryTruck, PrefabRef>()
                .Build();

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

            LiveDispatchCategoryStats companyShopping = default;
            LiveDispatchCategoryStats storageTransfer = default;
            LiveDispatchCategoryStats facilityOwnedDispatch = default;

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
                    Game.Vehicles.DeliveryTruckFlags flags = truck.m_State;

                    PrefabRef prefabRef = EntityManager.GetComponentData<PrefabRef>(liveEntity);
                    Entity prefab = prefabRef.m_Prefab;

                    int vanillaCap = GetVanillaDeliveryVanillaCap(prefab);
                    if (vanillaCap <= 0)
                        continue;

                    relevant++;

                    Game.Economy.Resource transported = Game.Economy.Resource.NoResource;
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

                        bool isOverVanilla = amount > vanillaCap;
                        if (isOverVanilla)
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

                        // Category hints from live truck flags.
                        // These are useful but not perfect:
                        // - CompanyShopping = Buying and not StorageTransfer / UpkeepDelivery
                        // - StorageTransfer = StorageTransfer
                        // - FacilityOwnedDispatch = UpkeepDelivery
                        // - OC-Transfer cannot be isolated cleanly from one snapshot without extra source/target tracing.
                        if ((flags & Game.Vehicles.DeliveryTruckFlags.StorageTransfer) != 0)
                        {
                            storageTransfer.Carrying++;
                            if (isOverVanilla) storageTransfer.OverVanilla++;
                        }
                        else if ((flags & Game.Vehicles.DeliveryTruckFlags.UpkeepDelivery) != 0)
                        {
                            facilityOwnedDispatch.Carrying++;
                            if (isOverVanilla) facilityOwnedDispatch.OverVanilla++;
                        }
                        else if ((flags & Game.Vehicles.DeliveryTruckFlags.Buying) != 0)
                        {
                            companyShopping.Carrying++;
                            if (isOverVanilla) companyShopping.OverVanilla++;
                        }
                    }

                    bucketStats[bucketIndex] = stats;
                }
            }

            // Compact summary block first.
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

            AppendCapped(sb, ref lines, ref truncated, "");

            AppendCapped(sb, ref lines, ref truncated, "Truck buckets above vanilla:");
            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Semi", bucketStats[(int)VehicleHelpers.DeliveryBucket.Semi]);
            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Van", bucketStats[(int)VehicleHelpers.DeliveryBucket.Van]);
            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Raw", bucketStats[(int)VehicleHelpers.DeliveryBucket.RawMaterials]);
            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Motorbike", bucketStats[(int)VehicleHelpers.DeliveryBucket.Motorbike]);
            AppendLiveDeliveryBucket(sb, ref lines, ref truncated, "Other", bucketStats[(int)VehicleHelpers.DeliveryBucket.Other]);

            AppendCapped(sb, ref lines, ref truncated, "");
            AppendCapped(sb, ref lines, ref truncated, "Dispatch category hints (one-shot live snapshot):");
            AppendLiveDispatchCategory(sb, ref lines, ref truncated, "1. CompanyShopping", companyShopping);
            AppendLiveDispatchCategory(sb, ref lines, ref truncated, "2. StorageTransfer", storageTransfer);
            AppendCapped(sb, ref lines, ref truncated, "3. OC-Transfer: not isolated in one-shot live snapshot");
            AppendLiveDispatchCategory(sb, ref lines, ref truncated, "4. FacilityOwnedDispatch", facilityOwnedDispatch);
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
                $"- {bucketName}: {stats.OverVanilla}/{stats.Carrying} above vanilla ({pct:0.#}%) | max={FmtTons(stats.MaxAmount)} | prefab='{stats.MaxPrefabName}'");
        }

        private static void AppendLiveDispatchCategory(
            StringBuilder sb,
            ref int lines,
            ref bool truncated,
            string label,
            LiveDispatchCategoryStats stats)
        {
            if (stats.Carrying <= 0)
            {
                AppendCapped(sb, ref lines, ref truncated, $"{label}: none seen");
                return;
            }

            float pct = 100f * stats.OverVanilla / stats.Carrying;
            AppendCapped(sb, ref lines, ref truncated, $"{label}: {stats.OverVanilla}/{stats.Carrying} above vanilla ({pct:0.#}%)");
        }

        private static void AppendSectionHeader(StringBuilder sb, ref int lines, ref bool truncated, string title)
        {
            AppendCapped(sb, ref lines, ref truncated, "================================");
            AppendCapped(sb, ref lines, ref truncated, title);
            AppendCapped(sb, ref lines, ref truncated, "================================");
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

        internal static string FmtCapacityForReport(int amount)
        {
            float tons = amount / 1000f;
            return $"{amount} (~{tons:0.###}t)";
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
