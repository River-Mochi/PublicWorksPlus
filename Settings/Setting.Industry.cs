// File: Settings/Setting.Industry.cs
// Purpose: Industry settings (delivery vehicles, cargo stations, extractors).
// Notes:
// - Delivery vehicle sliders are stored as percent values, same style as Transit.
// - 100% = vanilla, 500% = 5x.
// - Cargo station / extractor fleet sliders remain scalar 1x..5x.

namespace PublicWorksPlus
{
    using Game;              // IsGame
    using Game.SceneFlow;    // GameManager
    using Game.Settings;     // Settings UI attributes
    using Game.UI;           // Unit
    using Unity.Entities;    // World

    public sealed partial class Setting
    {
        // Delivery vehicles are now stored as percent values.
        private float m_SemiTruckCargoScalar = kVanillaPercent;
        private float m_DeliveryVanCargoScalar = kVanillaPercent;
        private float m_CoalTruckScalar = kVanillaPercent;
        private float m_MotorbikeDeliveryCargoScalar = kVanillaPercent;

        // These still use simple scalar values (1x..5x).
        private float m_ExtractorMaxTrucksScalar = kVanillaScalar;
        private float m_CargoStationMaxTrucksScalar = kVanillaScalar;

        // Delivery vehicles (stored/displayed as percent, like Transit).
        [SettingsUISlider(min = DeliveryMinPercent, max = DeliveryMaxPercent, step = DeliveryStepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public float SemiTruckCargoScalar
        {
            get => m_SemiTruckCargoScalar;
            set
            {
                float v = NormalizeDeliveryPercentOrVanilla(value);
                if (m_SemiTruckCargoScalar == v) return;

                m_SemiTruckCargoScalar = v;
                OnIndustryChanged();
            }
        }

        [SettingsUISlider(min = DeliveryMinPercent, max = DeliveryMaxPercent, step = DeliveryStepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public float DeliveryVanCargoScalar
        {
            get => m_DeliveryVanCargoScalar;
            set
            {
                float v = NormalizeDeliveryPercentOrVanilla(value);
                if (m_DeliveryVanCargoScalar == v) return;

                m_DeliveryVanCargoScalar = v;
                OnIndustryChanged();
            }
        }

        [SettingsUISlider(min = DeliveryMinPercent, max = DeliveryMaxPercent, step = DeliveryStepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public float CoalTruckScalar
        {
            get => m_CoalTruckScalar;
            set
            {
                float v = NormalizeDeliveryPercentOrVanilla(value);
                if (m_CoalTruckScalar == v) return;

                m_CoalTruckScalar = v;
                OnIndustryChanged();
            }
        }

        [SettingsUISlider(min = DeliveryMinPercent, max = DeliveryMaxPercent, step = DeliveryStepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public float MotorbikeDeliveryCargoScalar
        {
            get => m_MotorbikeDeliveryCargoScalar;
            set
            {
                float v = NormalizeDeliveryPercentOrVanilla(value);
                if (m_MotorbikeDeliveryCargoScalar == v) return;

                m_MotorbikeDeliveryCargoScalar = v;
                OnIndustryChanged();
            }
        }

        // Extractor + Cargo Stations remain scalar 1x..5x.
        [SettingsUISlider(min = CargoStationMinScalar, max = CargoStationMaxScalar, step = CargoStationStepScalar)]
        [SettingsUISection(IndustryTab, CargoStationsGroup)]
        public float ExtractorMaxTrucksScalar
        {
            get => m_ExtractorMaxTrucksScalar;
            set
            {
                float v = ScalarMath.ClampScalar(value, CargoStationMinScalar, CargoStationMaxScalar);
                if (m_ExtractorMaxTrucksScalar == v) return;

                m_ExtractorMaxTrucksScalar = v;
                OnIndustryChanged();
            }
        }

        [SettingsUISlider(min = CargoStationMinScalar, max = CargoStationMaxScalar, step = CargoStationStepScalar)]
        [SettingsUISection(IndustryTab, CargoStationsGroup)]
        public float CargoStationMaxTrucksScalar
        {
            get => m_CargoStationMaxTrucksScalar;
            set
            {
                float v = ScalarMath.ClampScalar(value, CargoStationMinScalar, CargoStationMaxScalar);
                if (m_CargoStationMaxTrucksScalar == v) return;

                m_CargoStationMaxTrucksScalar = v;
                OnIndustryChanged();
            }
        }

        [SettingsUIButtonGroup(DeliveryGroup)]
        [SettingsUIButton]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public bool ResetDeliveryToVanillaButton
        {
            set
            {
                if (!value) return;

                m_SemiTruckCargoScalar = kVanillaPercent;
                m_DeliveryVanCargoScalar = kVanillaPercent;
                m_CoalTruckScalar = kVanillaPercent;
                m_MotorbikeDeliveryCargoScalar = kVanillaPercent;

                ApplyAndSave();
            }
        }

        [SettingsUIButtonGroup(CargoStationsGroup)]
        [SettingsUIButton]
        [SettingsUISection(IndustryTab, CargoStationsGroup)]
        public bool ResetCargoStationsToVanillaButton
        {
            set
            {
                if (!value) return;

                m_CargoStationMaxTrucksScalar = kVanillaScalar;
                m_ExtractorMaxTrucksScalar = kVanillaScalar;

                ApplyAndSave();
            }
        }

        private void OnIndustryChanged()
        {
            GameManager gm = GameManager.instance;
            if (gm == null || !gm.gameMode.IsGame())
                return;

            World world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
                return;

            TryEnableOnce<IndustrySystem>(world, "IndustrySystem");
        }

        partial void SetDefaults_Industry()
        {
            m_SemiTruckCargoScalar = kVanillaPercent;
            m_DeliveryVanCargoScalar = kVanillaPercent;
            m_CoalTruckScalar = kVanillaPercent;
            m_MotorbikeDeliveryCargoScalar = kVanillaPercent;

            m_CargoStationMaxTrucksScalar = kVanillaScalar;
            m_ExtractorMaxTrucksScalar = kVanillaScalar;
        }

        partial void RepairAndClamp_Industry()
        {
            // Delivery sliders support migration from older scalar saves.
            m_SemiTruckCargoScalar = NormalizeDeliveryPercentOrVanilla(m_SemiTruckCargoScalar);
            m_DeliveryVanCargoScalar = NormalizeDeliveryPercentOrVanilla(m_DeliveryVanCargoScalar);
            m_CoalTruckScalar = NormalizeDeliveryPercentOrVanilla(m_CoalTruckScalar);
            m_MotorbikeDeliveryCargoScalar = NormalizeDeliveryPercentOrVanilla(m_MotorbikeDeliveryCargoScalar);

            // Fleet sliders stay scalar.
            m_CargoStationMaxTrucksScalar = ClampScalarOrDefault(m_CargoStationMaxTrucksScalar, CargoStationMinScalar, CargoStationMaxScalar, kVanillaScalar);
            m_ExtractorMaxTrucksScalar = ClampScalarOrDefault(m_ExtractorMaxTrucksScalar, CargoStationMinScalar, CargoStationMaxScalar, kVanillaScalar);
        }
    }
}
