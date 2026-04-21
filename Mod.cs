// File: Mod.cs
// Entrypoint: registers settings, locales, and the ECS systems.

namespace PublicWorksPlus
{
    using Colossal;                       // IDictionarySource
    using Colossal.IO.AssetDatabase;      // AssetDatabase.LoadSettings
    using Colossal.Localization;          // LocalizationManager
    using Colossal.Logging;               // ILog, defines shared s_Log
    using Game;                           // UpdateSystem, GameManager, SystemUpdatePhase
    using Game.Modding;                   // IMod
    using Game.Prefabs;                    // VehicleCapacitySystem, DeliveryTruckSelectData
    using Game.SceneFlow;                 // GameManager
    using Game.Simulation;                // game ECS systems for ordering hooks
    using System;                         // Exception
    using System.Reflection;              // Assembly

    /// <summary>Mod entry point: registers settings, locales, and ECS systems.</summary>
    public sealed class Mod : IMod
    {
        public const string ModName = "Public Works Plus";
        public const string ShortName = "Public Works +";
        public const string ModId = "PublicWorksPlus";
        public const string ModTag = "[PWP]";

        public static readonly string ModVersion =
            Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "1.0.0";

        private static bool s_BannerLogged;

        public static readonly ILog s_Log =
            LogManager.GetLogger(ModId).SetShowsErrorsInUI(false);

        public static Setting? Settings;

        public void OnLoad(UpdateSystem updateSystem)
        {
            if (!s_BannerLogged)
            {
                s_BannerLogged = true;
                LogUtils.Info(s_Log, () => $"{ModName} v{ModVersion} OnLoad");
            }

            // Settings first so locale labels can resolve.
            Setting setting = new(this);
            Settings = setting;

            // Register ALL languages (keep these lines!)
            AddLocaleSource("en-US", new LocaleEN(setting));
            AddLocaleSource("fr-FR", new LocaleFR(setting));
            AddLocaleSource("es-ES", new LocaleES(setting));
            AddLocaleSource("de-DE", new LocaleDE(setting));
            AddLocaleSource("it-IT", new LocaleIT(setting));
            AddLocaleSource("ja-JP", new LocaleJA(setting));
            AddLocaleSource("ko-KR", new LocaleKO(setting));
            AddLocaleSource("pl-PL", new LocalePL(setting));
            AddLocaleSource("pt-BR", new LocalePT_BR(setting));
            AddLocaleSource("zh-HANS", new LocaleZH_CN(setting));    // Simplified Chinese
            AddLocaleSource("zh-HANT", new LocaleZH_HANT(setting));  // Traditional Chinese

            // Load settings (.coc) into the instance.
            // The default instance passed here provides defaults for missing fields.
            AssetDatabase.global.LoadSettings(ModId, setting, new Setting(this));

            // Repair missing/out-of-range/invalid values in-memory (no auto-save).
            setting.SanitizeAfterLoad();

            setting.RegisterInOptionsUI();

            // Systems
            updateSystem.UpdateAfter<TransitSystem>(SystemUpdatePhase.PrefabUpdate);
            updateSystem.UpdateAfter<MaintenanceSystem>(SystemUpdatePhase.PrefabUpdate);
            updateSystem.UpdateAfter<LaneWearSystem>(SystemUpdatePhase.PrefabUpdate);

            // Storage transfer car-request promotion:
            // run after station/storage transfer requests are created,
            // before the car-request system turns them into TripNeeded.
            updateSystem.UpdateAt<StationTransferCapacitySystem>(SystemUpdatePhase.GameSimulation);
            updateSystem.UpdateAfter<StationTransferCapacitySystem, StorageTransferSystem>(SystemUpdatePhase.GameSimulation);
            updateSystem.UpdateBefore<StationTransferCapacitySystem, CarStorageTransferRequestSystem>(SystemUpdatePhase.GameSimulation);

            // Company shopping promotion:
            // run after BuyingCompanySystem creates ResourceBuyer,
            // before ResourceBuyerSystem turns it into TripNeeded.
            updateSystem.UpdateAt<CompanyShoppingCapacitySystem>(SystemUpdatePhase.GameSimulation);
            updateSystem.UpdateAfter<CompanyShoppingCapacitySystem, BuyingCompanySystem>(SystemUpdatePhase.GameSimulation);
            updateSystem.UpdateBefore<CompanyShoppingCapacitySystem, ResourceBuyerSystem>(SystemUpdatePhase.GameSimulation);


            // Industry (prefab editing window)
            // Critical: run IndustrySystem BEFORE VehicleCapacitySystem so the game's
            // DeliveryTruckSelectData table is rebuilt from the updated truck capacities.
            updateSystem.UpdateAfter<IndustrySystem>(SystemUpdatePhase.PrefabUpdate);
            updateSystem.UpdateBefore<IndustrySystem, VehicleCapacitySystem>(SystemUpdatePhase.PrefabUpdate);
            updateSystem.UpdateBefore<IndustrySystem>(SystemUpdatePhase.PrefabReferences);


            // Allow transit lines range to be 1-and higher than vanilla
            updateSystem.UpdateAfter<VehicleCountPolicyTunerSystem>(SystemUpdatePhase.PrefabUpdate);

            // Prefab scan: must work even while Options UI is open
            updateSystem.UpdateAt<PrefabScanSystem>(SystemUpdatePhase.PrefabUpdate);

            // Live delivery cargo proof logger.
            // Safe for Release because runtime work is gated by EnableDebugLogging inside the system.
            updateSystem.UpdateAt<DeliveryCargoProbeSystem>(SystemUpdatePhase.GameSimulation);

#if DEBUG
            // Debug probe: logs LaneCondition.m_Wear deltas (runtime)
            updateSystem.UpdateAt<LaneWearProbeSystem>(SystemUpdatePhase.GameSimulation);
#endif

            LogUtils.Info(s_Log, () => $"{ModId}.{nameof(OnLoad)} Completed.");
        }

        public void OnDispose()
        {
            LogUtils.Info(s_Log, () => "OnDispose");

            if (Settings != null)
            {
                Settings.UnregisterInOptionsUI();
                Settings = null;
            }
        }

        //---------------
        // HELPERS
        //---------------

        private static void AddLocaleSource(string localeId, IDictionarySource source)
        {
            if (string.IsNullOrEmpty(localeId))
            {
                return;
            }

            LocalizationManager? lm = GameManager.instance?.localizationManager;
            if (lm == null)
            {
                LogUtils.Warn(s_Log, () => $"AddLocaleSource: No LocalizationManager; cannot add source for '{localeId}'.");
                return;
            }

            try
            {
                lm.AddSource(localeId, source);
            }
            catch (Exception ex)
            {
                LogUtils.Warn(s_Log, () => $"AddLocaleSource: AddSource for '{localeId}' failed: {ex.GetType().Name}: {ex.Message}");
            }
        }

        internal static string L(string id, string fallback)
        {
            try
            {
                LocalizationManager? lm = GameManager.instance?.localizationManager;
                if (lm != null &&
                    lm.activeDictionary != null &&
                    lm.activeDictionary.TryGetValue(id, out string result))
                {
                    return result;
                }
            }
            catch
            {
            }

            return fallback;
        }
    }
}
