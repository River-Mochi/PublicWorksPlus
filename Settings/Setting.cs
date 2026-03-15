// File: Settings/Setting.cs
// Purpose: Options UI + saved settings for Dispatch Boss.
// Notes:
// - Partial class split across 4 source files.
// - Defaults are applied in ctor to avoid “all zero” default instances.
// - Apply() re-enables run-once systems so changes take effect.

namespace DispatchBoss
{
    using Colossal.IO.AssetDatabase; // FileLocation
    using Game;                     // IsGame
    using Game.Modding;             // IMod
    using Game.SceneFlow;           // GameManager
    using Game.Settings;            // Settings UI attributes
    using Game.UI;                  // Unit
    using System;                   // Exception
    using Unity.Entities;
    using UnityEngine;              // Application URL

    [FileLocation("ModsSettings/DispatchBoss/DispatchBoss")]
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

        // Industry sliders (scalar 1x..10x).
        public const float ServiceMinScalar = 1f;
        public const float ServiceMaxScalar = 10f;
        public const float ServiceStepScalar = 1f;

        // Cargo station / extractors (scalar 1x..5x).
        public const float CargoStationMinScalar = 1f;
        public const float CargoStationMaxScalar = 5f;
        public const float CargoStationStepScalar = 1f;

        // Parks+Roads: display as percent (100%..1000% = 1x..10x).
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
            // Defaults must exist for:
            // - First-run (no settings file)
            // - Default template instance passed to LoadSettings(...)
            SetDefaults();
        }

        public override void SetDefaults()
        {
            // Public-Transit defaults (percent).
            m_EnableLineVehicleCountTuner = false;   // Setter avoided (prevents early Apply).
            ResetDepotToVanilla();
            ResetPassengerToVanilla();

            // Industry defaults (scalar).
            m_SemiTruckCargoScalar = 1f;
            m_DeliveryVanCargoScalar = 1f;
            m_OilTruckCargoScalar = 1f;
            m_MotorbikeDeliveryCargoScalar = 1f;

            m_CargoStationMaxTrucksScalar = 1f;
            m_ExtractorMaxTrucksScalar = 1f;

            // Parks-Roads defaults (percent).
            RoadWearScalar = 100f;

            RoadMaintenanceVehicleCapacityScalar = 100f;
            RoadMaintenanceVehicleRateScalar = 100f;
            RoadMaintenanceDepotScalar = 100f;

            ParkMaintenanceVehicleCapacityScalar = 100f;
            ParkMaintenanceVehicleRateScalar = 100f;
            ParkMaintenanceDepotScalar = 100f;

            // Debug.
            EnableDebugLogging = false;
        }

        public override void Apply()
        {
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

            // Settings changes re-enable run-once systems.
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

        // ----------------
        // Debug
        // ----------------

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
    }
}
