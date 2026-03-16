// File: Systems/MaintenanceSystem.cs
// Purpose: Apply park/road maintenance vehicle and depot multipliers based on current settings.
// Notes:
// - Run-once system: enabled on city load or when settings Apply() enables it.
// - Uses PrefabSystem + PrefabBase to read vanilla/base values so results do NOT stack.
// - Includes name-based fallback for depots with MaintenanceType.None (extra garages / sub-prefabs).

namespace DispatchBoss
{
    using Colossal.Serialization.Entities;
    using Game;
    using Game.Prefabs;
    using Game.SceneFlow;
    using Game.Simulation; // MaintenanceType
    using System;
    using System.Collections.Generic;
    using Unity.Entities;

    public sealed partial class MaintenanceSystem : GameSystemBase
    {
        private PrefabSystem m_PrefabSystem = null!;

        // Prefab base caches (per city/session) — prevents stacking.
        private Dictionary<Entity, (int Cap, int Rate)> m_MaintenanceVehicleBase = null!;
        private Dictionary<Entity, int> m_MaintenanceDepotBaseVehicleCapacity = null!;

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();

            m_MaintenanceVehicleBase = new Dictionary<Entity, (int Cap, int Rate)>();
            m_MaintenanceDepotBaseVehicleCapacity = new Dictionary<Entity, int>();

            EntityQuery q = SystemAPI.QueryBuilder()
                .WithAll<PrefabData>()
                .WithAny<MaintenanceVehicleData, MaintenanceDepotData>()
                .Build();

            RequireForUpdate(q);

            Enabled = false;
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

            m_MaintenanceVehicleBase.Clear();
            m_MaintenanceDepotBaseVehicleCapacity.Clear();

            Mod.s_Log.Info($"{Mod.ModTag} City Loading Complete -> applying Maintenance settings");
            Enabled = true;
        }

        protected override void OnUpdate()
        {
            GameManager gm = GameManager.instance;
            if (gm == null || !gm.gameMode.IsGame())
            {
                Enabled = false;
                return;
            }

            if (Mod.Settings == null)
            {
                Enabled = false;
                return;
            }

            Setting settings = Mod.Settings;
            bool verbose = settings.EnableDebugLogging;

            // Scalars computed once.
            float roadDepotScalar = ScalarMath.PercentToScalarClamped(
                settings.RoadMaintenanceDepotScalar,
                Setting.MaintenanceMinPercent,
                Setting.MaintenanceMaxPercent);

            float parkDepotScalar = ScalarMath.PercentToScalarClamped(
                settings.ParkMaintenanceDepotScalar,
                Setting.MaintenanceMinPercent,
                Setting.MaintenanceMaxPercent);

            float roadCapScalar = ScalarMath.PercentToScalarClamped(
                settings.RoadMaintenanceVehicleCapacityScalar,
                Setting.MaintenanceMinPercent,
                Setting.MaintenanceMaxPercent);

            float roadRateScalar = ScalarMath.PercentToScalarClamped(
                settings.RoadMaintenanceVehicleRateScalar,
                Setting.MaintenanceMinPercent,
                Setting.MaintenanceMaxPercent);

            float parkCapScalar = ScalarMath.PercentToScalarClamped(
                settings.ParkMaintenanceVehicleCapacityScalar,
                Setting.MaintenanceMinPercent,
                Setting.MaintenanceMaxPercent);

            float parkRateScalar = ScalarMath.PercentToScalarClamped(
                settings.ParkMaintenanceVehicleRateScalar,
                Setting.MaintenanceMinPercent,
                Setting.MaintenanceMaxPercent);

            // ------------------------------------------
            // Maintenance depots: max vehicles (prefabs)
            // ------------------------------------------
            foreach ((RefRW<MaintenanceDepotData> depotRef, Entity prefabEntity) in SystemAPI
                         .Query<RefRW<MaintenanceDepotData>>()
                         .WithAll<PrefabData>()
                         .WithEntityAccess())
            {
                ref MaintenanceDepotData data = ref depotRef.ValueRW;

                string prefabName = PrefabNameUtil.GetNameSafe(m_PrefabSystem, prefabEntity);

                bool isPark;
                if (!TryClassifyDepot(data.m_MaintenanceType, prefabName, out isPark))
                {
                    continue;
                }

                int baseVehicles = GetOrCacheMaintenanceDepotBase(prefabEntity, data.m_VehicleCapacity);

                if (baseVehicles <= 0 && data.m_VehicleCapacity <= 0)
                {
                    continue;
                }

                float scalar = isPark ? parkDepotScalar : roadDepotScalar;
                int newVehicles = ScalarMath.ScaleIntRoundedAllowZeroMin1(baseVehicles, scalar);

                if (newVehicles != data.m_VehicleCapacity)
                {
                    if (verbose)
                    {
                        string groupLabel = isPark ? "Park" : "Road";
                        Mod.s_Log.Info(
                            $"{Mod.ModTag} MaintenanceDepot({groupLabel}) vehicles: '{prefabName}' " +
                            $"Type={data.m_MaintenanceType} Base={baseVehicles} x{scalar:0.##} -> {newVehicles}");
                    }

                    data.m_VehicleCapacity = newVehicles;
                }
            }

            // -----------------------------------------------
            // Maintenance vehicles: capacity + rate (prefabs)
            // -----------------------------------------------
            foreach ((RefRW<MaintenanceVehicleData> mvRef, Entity prefabEntity) in SystemAPI
                         .Query<RefRW<MaintenanceVehicleData>>()
                         .WithAll<PrefabData>()
                         .WithEntityAccess())
            {
                ref MaintenanceVehicleData data = ref mvRef.ValueRW;

                string prefabName = PrefabNameUtil.GetNameSafe(m_PrefabSystem, prefabEntity);

                bool isPark;
                if (!TryClassifyVehicle(data.m_MaintenanceType, prefabName, out isPark))
                {
                    continue;
                }

                float capScalar = isPark ? parkCapScalar : roadCapScalar;
                float rateScalar = isPark ? parkRateScalar : roadRateScalar;

                (int Cap, int Rate) baseVals = GetOrCacheMaintenanceVehicleBase(
                    prefabEntity,
                    data.m_MaintenanceCapacity,
                    data.m_MaintenanceRate);

                int newCap = ScalarMath.ScaleIntRoundedMin1(baseVals.Cap, capScalar);
                int newRate = ScalarMath.ScaleIntRoundedMin1(baseVals.Rate, rateScalar);

                if (newCap != data.m_MaintenanceCapacity)
                {
                    if (verbose)
                    {
                        string groupLabel = isPark ? "Park" : "Road";
                        Mod.s_Log.Info(
                            $"{Mod.ModTag} Maintenance({groupLabel}) cap: '{prefabName}' " +
                            $"Type={data.m_MaintenanceType} Base={baseVals.Cap} x{capScalar:0.##} -> {newCap}");
                    }

                    data.m_MaintenanceCapacity = newCap;
                }

                if (newRate != data.m_MaintenanceRate)
                {
                    if (verbose)
                    {
                        string groupLabel = isPark ? "Park" : "Road";
                        Mod.s_Log.Info(
                            $"{Mod.ModTag} Maintenance({groupLabel}) rate: '{prefabName}' " +
                            $"Type={data.m_MaintenanceType} Base={baseVals.Rate} x{rateScalar:0.##} -> {newRate}");
                    }

                    data.m_MaintenanceRate = newRate;
                }
            }

            Enabled = false;
        }

        private static bool TryClassifyDepot(MaintenanceType mt, string prefabName, out bool isPark)
        {
            // Name fallback for Type=None (sub-prefabs / upgrades).
            bool nameParkDepot = prefabName.IndexOf("ParkMaintenanceDepot", StringComparison.OrdinalIgnoreCase) >= 0;
            bool nameRoadDepot = prefabName.IndexOf("RoadMaintenanceDepot", StringComparison.OrdinalIgnoreCase) >= 0;

            if (mt == MaintenanceType.None)
            {
                if (nameParkDepot) { isPark = true; return true; }
                if (nameRoadDepot) { isPark = false; return true; }

                isPark = false;
                return false;
            }

            bool hasParkFlag = (mt & MaintenanceType.Park) != MaintenanceType.None;
            bool hasRoadFlags = (mt & (MaintenanceType.Road | MaintenanceType.Snow | MaintenanceType.Vehicle)) != MaintenanceType.None;

            if (hasParkFlag || hasRoadFlags)
            {
                isPark = hasParkFlag && !hasRoadFlags;
                return true;
            }

            // Rare: non-standard flags. Try name fallback.
            if (nameParkDepot) { isPark = true; return true; }
            if (nameRoadDepot) { isPark = false; return true; }

            isPark = false;
            return false;
        }

        private static bool TryClassifyVehicle(MaintenanceType mt, string prefabName, out bool isPark)
        {
            if (mt == MaintenanceType.None)
            {
                isPark = false;
                return false;
            }

            bool hasParkFlag = (mt & MaintenanceType.Park) != MaintenanceType.None;
            bool hasRoadFlags = (mt & (MaintenanceType.Road | MaintenanceType.Snow | MaintenanceType.Vehicle)) != MaintenanceType.None;

            if (hasParkFlag || hasRoadFlags)
            {
                isPark = hasParkFlag && !hasRoadFlags;
                return true;
            }

            // Rare: non-standard flags. Try name fallback.
            if (prefabName.IndexOf("ParkMaintenance", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                isPark = true;
                return true;
            }

            if (prefabName.IndexOf("RoadMaintenance", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                isPark = false;
                return true;
            }

            isPark = false;
            return false;
        }

        // -------------------------
        // Vanilla/base caching
        // -------------------------

        private int GetOrCacheMaintenanceDepotBase(Entity prefabEntity, int currentValue)
        {
            if (m_MaintenanceDepotBaseVehicleCapacity.TryGetValue(prefabEntity, out int baseVehicles))
            {
                return baseVehicles;
            }

            int vanilla;
            if (TryGetMaintenanceDepotVanilla(prefabEntity, out vanilla) && vanilla >= 0)
                baseVehicles = vanilla;
            else
                baseVehicles = currentValue;

            m_MaintenanceDepotBaseVehicleCapacity[prefabEntity] = baseVehicles;
            return baseVehicles;
        }

        private bool TryGetMaintenanceDepotVanilla(Entity prefabEntity, out int baseVehicles)
        {
            baseVehicles = 0;

            if (!PrefabComponentUtil.TryGetComponent(m_PrefabSystem, prefabEntity, out Game.Prefabs.MaintenanceDepot depot))
                return false;

            baseVehicles = depot.m_VehicleCapacity;
            return true;
        }

        private (int Cap, int Rate) GetOrCacheMaintenanceVehicleBase(Entity prefabEntity, int currentCap, int currentRate)
        {
            if (m_MaintenanceVehicleBase.TryGetValue(prefabEntity, out (int Cap, int Rate) baseVals))
            {
                return baseVals;
            }

            int baseCap;
            int baseRate;

            if (!TryGetMaintenanceVehicleVanilla(prefabEntity, out baseCap, out baseRate))
            {
                baseCap = currentCap;
                baseRate = currentRate;
            }

            if (baseCap < 1) baseCap = 1;
            if (baseRate < 1) baseRate = 1;

            baseVals = (baseCap, baseRate);
            m_MaintenanceVehicleBase[prefabEntity] = baseVals;
            return baseVals;
        }

        private bool TryGetMaintenanceVehicleVanilla(Entity prefabEntity, out int baseCap, out int baseRate)
        {
            baseCap = 0;
            baseRate = 0;

            if (!PrefabComponentUtil.TryGetComponent(m_PrefabSystem, prefabEntity, out Game.Prefabs.MaintenanceVehicle mv))
                return false;

            baseCap = mv.m_MaintenanceCapacity;
            baseRate = mv.m_MaintenanceRate;
            return true;
        }
    }
}
