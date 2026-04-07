// File: Settings/Setting.cs
// Purpose: Options UI + saved settings for Adjust Transit Capacity (Public Transit + About).

namespace AdjustTransit
{
    using Colossal.IO.AssetDatabase; // FileLocation
    using Game;                      // IsGame
    using Game.Modding;              // IMod, ModSetting
    using Game.SceneFlow;            // GameManager
    using Game.Settings;             // Settings UI attributes
    using System;                    // Exception
    using Unity.Entities;            // World
    using UnityEngine;               // Application.OpenURL

    [FileLocation("ModsSettings/AdjustTransit/AdjustTransit")]
    [SettingsUITabOrder(PublicTransitTab, AboutTab)]
    [SettingsUIGroupOrder(
        LineVehiclesGroup, DepotGroup, PassengerGroup,
        AboutInfoGroup, AboutLinksGroup, DebugGroup)]
    [SettingsUIShowGroupName(
        LineVehiclesGroup, DepotGroup, PassengerGroup,
        AboutLinksGroup, DebugGroup)]
    public sealed partial class Setting : ModSetting
    {
        // Tab ids (must match Locale ids).
        public const string PublicTransitTab = "Public-Transit";
        public const string AboutTab = "About";

        // Group ids (must match Locale ids).
        public const string LineVehiclesGroup = "LineVehicles";
        public const string DepotGroup = "DepotCapacity";
        public const string PassengerGroup = "PassengerCapacity";
        public const string AboutInfoGroup = "AboutInfo";
        public const string AboutLinksGroup = "AboutLinks";
        public const string DebugGroup = "Debug";

        // Public Transit sliders (percent).
        public const float DepotMinPercent = 100f;
        public const float PassengerMinPercent = 10f;
        public const float MaxPercent = 1000f;
        public const float StepPercent = 10f;

        private const float kVanillaPercent = 100f;

        private const string UrlParadox =
            "https://mods.paradoxplaza.com/authors/River-mochi/cities_skylines_2?games=cities_skylines_2&orderBy=desc&sortBy=best&time=alltime";

        private const string UrlDiscord =
            "https://discord.gg/HTav7ARPs2";

        public Setting(IMod mod)
            : base(mod)
        {
            SetDefaults();
        }

        /// <summary>
        /// Repair missing or invalid values after LoadSettings.
        /// No auto-save performed.
        /// </summary>
        public void SanitizeAfterLoad()
        {
            RepairAndClamp();
        }

        public override void SetDefaults()
        {
            SetDefaults_Transit();
            EnableDebugLogging = false;
        }

        public override void Apply()
        {
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

            TryEnableOnce<TransitSystem>(world, "TransitSystem");
            TryEnableOnce<VehicleCountPolicyTunerSystem>(world, "VehicleCountPolicyTunerSystem");
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
                if (!value)
                {
                    return;
                }

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
                if (!value)
                {
                    return;
                }

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
        // Debug / Logging
        // ----------------

        [SettingsUISection(AboutTab, DebugGroup)]
        public bool EnableDebugLogging { get; set; }

        [SettingsUIButtonGroup(DebugGroup)]
        [SettingsUIButton]
        [SettingsUISection(AboutTab, DebugGroup)]
        public bool OpenLogButton
        {
            set => ShellOpen.OpenFolderSafe(ShellOpen.GetLogsFolder(), "OpenLog");
        }

        // ------------------------
        // Robust repair / clamp
        // ------------------------

        private void RepairAndClamp()
        {
            RepairAndClamp_Transit();
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

        private static bool IsFinite(float value)
        {
            return !(float.IsNaN(value) || float.IsInfinity(value));
        }

        partial void SetDefaults_Transit();
        partial void RepairAndClamp_Transit();
    }
}
