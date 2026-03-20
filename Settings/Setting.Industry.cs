// File: Settings/Setting.Industry.cs
// Purpose: Industry settings (delivery vehicles, cargo stations, extractors).

namespace PublicWorksPlus
{
    using Game;              // IsGame
    using Game.SceneFlow;    // GameManager
    using Game.Settings;     // Settings UI attributes
    using Unity.Entities;    // World

    public sealed partial class Setting
    {
        // Vanilla/default scalar is 1.0f: scaling by 1.0 means "no change".
        private const float kVanillaScalar = 1f;

        private float m_SemiTruckCargoScalar = kVanillaScalar;
        private float m_DeliveryVanCargoScalar = kVanillaScalar;
        private float m_OilTruckCargoScalar = kVanillaScalar;
        private float m_MotorbikeDeliveryCargoScalar = kVanillaScalar;

        private float m_ExtractorMaxTrucksScalar = kVanillaScalar;
        private float m_CargoStationMaxTrucksScalar = kVanillaScalar;

        // Delivery vehicles (scalar 1..10)

        [SettingsUISlider(min = ServiceMinScalar, max = ServiceMaxScalar, step = ServiceStepScalar)]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public float SemiTruckCargoScalar
        {
            get => m_SemiTruckCargoScalar;
            set
            {
                float v = ScalarMath.ClampScalar(value, ServiceMinScalar, ServiceMaxScalar);
                if (m_SemiTruckCargoScalar == v) return;

                m_SemiTruckCargoScalar = v;
                OnIndustryChanged();
            }
        }

        [SettingsUISlider(min = ServiceMinScalar, max = ServiceMaxScalar, step = ServiceStepScalar)]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public float DeliveryVanCargoScalar
        {
            get => m_DeliveryVanCargoScalar;
            set
            {
                float v = ScalarMath.ClampScalar(value, ServiceMinScalar, ServiceMaxScalar);
                if (m_DeliveryVanCargoScalar == v) return;

                m_DeliveryVanCargoScalar = v;
                OnIndustryChanged();
            }
        }

        [SettingsUISlider(min = ServiceMinScalar, max = ServiceMaxScalar, step = ServiceStepScalar)]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public float OilTruckCargoScalar
        {
            get => m_OilTruckCargoScalar;
            set
            {
                float v = ScalarMath.ClampScalar(value, ServiceMinScalar, ServiceMaxScalar);
                if (m_OilTruckCargoScalar == v) return;

                m_OilTruckCargoScalar = v;
                OnIndustryChanged();
            }
        }

        [SettingsUISlider(min = ServiceMinScalar, max = ServiceMaxScalar, step = ServiceStepScalar)]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public float MotorbikeDeliveryCargoScalar
        {
            get => m_MotorbikeDeliveryCargoScalar;
            set
            {
                float v = ScalarMath.ClampScalar(value, ServiceMinScalar, ServiceMaxScalar);
                if (m_MotorbikeDeliveryCargoScalar == v) return;

                m_MotorbikeDeliveryCargoScalar = v;
                OnIndustryChanged();
            }
        }

        // Extractor + Cargo Stations (scalar 1..5)

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

                m_SemiTruckCargoScalar = kVanillaScalar;
                m_DeliveryVanCargoScalar = kVanillaScalar;
                m_OilTruckCargoScalar = kVanillaScalar;
                m_MotorbikeDeliveryCargoScalar = kVanillaScalar;

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
            m_SemiTruckCargoScalar = kVanillaScalar;
            m_DeliveryVanCargoScalar = kVanillaScalar;
            m_OilTruckCargoScalar = kVanillaScalar;
            m_MotorbikeDeliveryCargoScalar = kVanillaScalar;

            m_CargoStationMaxTrucksScalar = kVanillaScalar;
            m_ExtractorMaxTrucksScalar = kVanillaScalar;
        }

        partial void RepairAndClamp_Industry()
        {
            m_SemiTruckCargoScalar = ClampScalarOrDefault(m_SemiTruckCargoScalar, ServiceMinScalar, ServiceMaxScalar, kVanillaScalar);
            m_DeliveryVanCargoScalar = ClampScalarOrDefault(m_DeliveryVanCargoScalar, ServiceMinScalar, ServiceMaxScalar, kVanillaScalar);
            m_OilTruckCargoScalar = ClampScalarOrDefault(m_OilTruckCargoScalar, ServiceMinScalar, ServiceMaxScalar, kVanillaScalar);
            m_MotorbikeDeliveryCargoScalar = ClampScalarOrDefault(m_MotorbikeDeliveryCargoScalar, ServiceMinScalar, ServiceMaxScalar, kVanillaScalar);

            m_CargoStationMaxTrucksScalar = ClampScalarOrDefault(m_CargoStationMaxTrucksScalar, CargoStationMinScalar, CargoStationMaxScalar, kVanillaScalar);
            m_ExtractorMaxTrucksScalar = ClampScalarOrDefault(m_ExtractorMaxTrucksScalar, CargoStationMinScalar, CargoStationMaxScalar, kVanillaScalar);
        }
    }
}
