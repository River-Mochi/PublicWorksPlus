// File: Settings/Setting.cs
// Purpose: Options UI + saved settings for Public Works Plus (Public Transit + Industry + Parks/Roads + About).

namespace PublicWorksPlus
{
    using Colossal.IO.AssetDatabase; // FileLocation
    using Game;                     // IsGame
    using Game.Modding;             // IMod, ModSetting
    using Game.SceneFlow;           // GameManager
    using Game.Settings;            // Settings UI attributes
    using System;                   // Exception
    using Unity.Entities;           // World
    using UnityEngine;              // Application.OpenURL

    [FileLocation("ModsSettings/PublicWorksPlus/PublicWorksPlus")]
    [SettingsUITabOrder(PublicTransitTab, IndustryTab, ParksRoadsTab, AboutTab)]
    [SettingsUIGroupOrder(
        LineVehiclesGroup, DepotGroup, PassengerGroup,
        DeliveryGroup, CargoStationsGroup,
        RoadMaintenanceGroup, ParkMaintenanceGroup,
        AboutInfoGroup, AboutLinksGroup, DebugGroup
    )]
    [SettingsUIShowGroupName(
        LineVehiclesGroup, DepotGroup, PassengerGroup,
        DeliveryGroup, CargoStationsGroup,
        RoadMaintenanceGroup, ParkMaintenanceGroup,
        AboutLinksGroup, DebugGroup
    )]
    public sealed partial class Setting : ModSetting
    {
        // Tab ids (must match Locale ids).
        public const string PublicTransitTab = "Public-Transit";
        public const string IndustryTab = "Industry";
        public const string ParksRoadsTab = "Parks-Roads";
        public const string AboutTab = "About";

        // Group ids (must match Locale ids).
        public const string LineVehiclesGroup = "LineVehicles";
        public const string DepotGroup = "DepotCapacity";
        public const string PassengerGroup = "PassengerCapacity";

        public const string DeliveryGroup = "DeliveryVehicles";
        public const string CargoStationsGroup = "CargoStations";

        public const string RoadMaintenanceGroup = "RoadMaintenance";
        public const string ParkMaintenanceGroup = "ParkMaintenance";

        public const string AboutInfoGroup = "AboutInfo";
        public const string AboutLinksGroup = "AboutLinks";
        public const string DebugGroup = "Debug";

        // -----------------------
        // Slider ranges
        // -----------------------

        // Public-Transit sliders (percent).
        public const float DepotMinPercent = 100f;
        public const float PassengerMinPercent = 10f;
        public const float MaxPercent = 1000f;
        public const float StepPercent = 10f;

        private const float kVanillaPercent = 100f;
        private const float kVanillaScalar = 1f;

        // Industry delivery sliders now store/display percent directly, same as Transit.
        // 100% = vanilla, 500% = 5x.
        public const float DeliveryMinPercent = 100f;
        public const float DeliveryMaxPercent = 500f;
        public const float DeliveryStepPercent = 25f;

        // Cargo station / extractors still use scalar 1x..5x.
        public const float CargoStationMinScalar = 1f;
        public const float CargoStationMaxScalar = 5f;
        public const float CargoStationStepScalar = 1f;

        // Parks+Roads: display as percent (100%..500% = 1x..5x).
        public const float MaintenanceMinPercent = 100f;
        public const float MaintenanceMaxPercent = 500f;
        public const float MaintenanceStepPercent = 10f;

        // Road wear speed: percent (10%..500% = 0.1x..5x).
        public const float RoadWearMinPercent = 10f;
        public const float RoadWearMaxPercent = 500f;
        public const float RoadWearStepPercent = 10f;

        private const string UrlParadox =
            "https://mods.paradoxplaza.com/authors/River-mochi/cities_skylines_2?games=cities_skylines_2&orderBy=desc&sortBy=best&time=alltime";

        private const string UrlDiscord =
            "https://discord.gg/HTav7ARPs2";

        public Setting(IMod mod)
            : base(mod)
        {
            // New install starts with defaults. LoadSettings overwrites when .coc exists.
            SetDefaults();
        }

        /// <summary>
        /// Repair missing/out-of-range/legacy values after LoadSettings.
        /// No auto-save performed.
        /// </summary>
        public void SanitizeAfterLoad()
        {
            RepairAndClamp();
        }

        public override void SetDefaults()
        {
            SetDefaults_Transit();
            SetDefaults_Industry();
            SetDefaults_ParksRoads();

            // Debug defaults.
            EnableDebugLogging = false;
        }

        public override void Apply()
        {
            // Repair in-memory values first so ECS always sees sane inputs.
            RepairAndClamp();

            base.Apply();

            GameManager gm = GameManager.instance;
            if (gm == null || !gm.gameMode.IsGame())
            {
                return;
            }

            World world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                return;
            }

            // Settings changes re-run systems once.
            TryEnableOnce<TransitSystem>(world, "TransitSystem");
            TryEnableOnce<MaintenanceSystem>(world, "MaintenanceSystem");
            TryEnableOnce<IndustrySystem>(world, "IndustrySystem");
            TryEnableOnce<LaneWearSystem>(world, "LaneWearSystem");
            TryEnableOnce<VehicleCountPolicyTunerSystem>(world, "TransitLinePolicyTunerSystem");
        }

        private static void TryEnableOnce<T>(World world, string label) where T : GameSystemBase
        {
            try
            {
                T sys = world.GetExistingSystemManaged<T>();
                if (sys != null)
                {
                    sys.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Mod.s_Log.Warn($"{Mod.ModTag} Apply: failed enabling {label}: {ex.GetType().Name}: {ex.Message}");
            }
        }

        // ----------------
        // About tab
        // ----------------

        [SettingsUISection(AboutTab, AboutInfoGroup)]
        public string ModNameDisplay => $"{Mod.ModName} {Mod.ModTag}";

        [SettingsUISection(AboutTab, AboutInfoGroup)]
        public string ModVersionDisplay => Mod.ModVersion;

        [SettingsUIButtonGroup(AboutLinksGroup)]
        [SettingsUIButton]
        [SettingsUISection(AboutTab, AboutLinksGroup)]
        public bool OpenParadoxMods
        {
            set
            {
                if (!value) return;

                try
                {
                    Application.OpenURL(UrlParadox);
                }
                catch (Exception ex)
                {
                    Mod.s_Log.Info($"{Mod.ModTag} OpenParadoxMods failed: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        [SettingsUIButtonGroup(AboutLinksGroup)]
        [SettingsUIButton]
        [SettingsUISection(AboutTab, AboutLinksGroup)]
        public bool OpenDiscord
        {
            set
            {
                if (!value) return;

                try
                {
                    Application.OpenURL(UrlDiscord);
                }
                catch (Exception ex)
                {
                    Mod.s_Log.Info($"{Mod.ModTag} OpenDiscord failed: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        // DEBUG/LOGGING

        [SettingsUIButtonGroup(DebugGroup)]
        [SettingsUIButton]
        [SettingsUISection(AboutTab, DebugGroup)]
        public bool RunPrefabScanButton
        {
            set
            {
                if (!value) return;

                GameManager gm = GameManager.instance;
                if (gm == null || !gm.gameMode.IsGame())
                {
                    PrefabScanState.MarkFailed(PrefabScanState.FailCode.NoCityLoaded, null);
                    return;
                }

                if (!PrefabScanState.RequestScan())
                {
                    Mod.s_Log.Info($"{Mod.ModTag} Prefab scan already queued/running.");
                    return;
                }

                try
                {
                    World world = World.DefaultGameObjectInjectionWorld;
                    if (world != null)
                    {
                        PrefabScanSystem scan = world.GetOrCreateSystemManaged<PrefabScanSystem>();
                        scan.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    PrefabScanState.MarkFailed(PrefabScanState.FailCode.Exception, $"{ex.GetType().Name}: {ex.Message}");
                    Mod.s_Log.Warn($"{Mod.ModTag} RunPrefabScanButton failed: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        [SettingsUIButtonGroup(DebugGroup)]
        [SettingsUISection(AboutTab, DebugGroup)]
        public string PrefabScanStatus => PrefabScanStatusText.Format(PrefabScanState.GetSnapshot());

        [SettingsUIButtonGroup(DebugGroup)]
        [SettingsUIButton]
        [SettingsUISection(AboutTab, DebugGroup)]
        public bool OpenReportButton
        {
            set => ShellOpen.OpenFolderSafe(ShellOpen.GetModsDataFolder(), "OpenReport");
        }

        [SettingsUISection(AboutTab, DebugGroup)]
        public bool EnableDebugLogging { get; set; }

        [SettingsUIButton]
        [SettingsUISection(AboutTab, DebugGroup)]
        public bool OpenLogButton
        {
            set => ShellOpen.OpenFolderSafe(ShellOpen.GetLogsFolder(), "OpenLog");
        }

        // ------------------------
        // Robust repair/clamp
        // ------------------------

        private void RepairAndClamp()
        {
            RepairAndClamp_Transit();
            RepairAndClamp_Industry();
            RepairAndClamp_ParksRoads();

            // Debug toggle is a bool; no repair needed.
        }

        private static float ClampPercentOrVanilla(float value, float min, float max, float vanilla)
        {
            if (!IsFinite(value) || value == 0f)
            {
                return vanilla;
            }

            if (value < min || value > max)
            {
                return vanilla;
            }

            return value;
        }

        private static float ClampScalarOrDefault(float value, float min, float max, float def)
        {
            // Missing/corrupt values still fall back to default.
            if (!IsFinite(value) || value == 0f)
            {
                return def;
            }

            // Legacy saved values above the new max clamp down to max instead of resetting to vanilla.
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Migration helper for Industry delivery sliders.
        /// Supports:
        /// - current/future percent values: 100..500
        /// - old scalar values: 1..5 -> 100..500
        /// - old larger scalar values: 6..10 -> 500
        /// </summary>
        private static float NormalizeDeliveryPercentOrVanilla(float value)
        {
            if (!IsFinite(value) || value == 0f)
            {
                return kVanillaPercent;
            }

            // New/current correct format: percent stored directly.
            if (value >= DeliveryMinPercent)
            {
                if (value > DeliveryMaxPercent)
                {
                    return DeliveryMaxPercent;
                }

                return value;
            }

            // Legacy factor format: 1..5 stored as scalar.
            if (value >= 1f && value <= 5f)
            {
                return value * 100f;
            }

            // Legacy 6x..10x values become 500%.
            if (value > 5f && value <= 10f)
            {
                return DeliveryMaxPercent;
            }

            // Unknown positive leftovers below 100 are safest at max rather than vanilla.
            if (value > 0f && value < DeliveryMinPercent)
            {
                return DeliveryMaxPercent;
            }

            return kVanillaPercent;
        }

        private static bool IsFinite(float v)
        {
            return !(float.IsNaN(v) || float.IsInfinity(v));
        }

        // Partial hooks keep files organized without duplicating boilerplate.
        partial void SetDefaults_Transit();
        partial void SetDefaults_Industry();
        partial void SetDefaults_ParksRoads();

        partial void RepairAndClamp_Transit();
        partial void RepairAndClamp_Industry();
        partial void RepairAndClamp_ParksRoads();
    }
}
