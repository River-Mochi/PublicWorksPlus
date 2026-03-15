// File: Settings/Setting.Industry.cs
// Purpose: Industry tab settings (delivery vehicle cargo capacity + cargo/ extractor fleet sizes).
// Notes:
// - Sliders trigger IndustrySystem enable so changes apply immediately in-game.

namespace DispatchBoss
{
    using Game;           // IsGame
    using Game.SceneFlow; // GameManager
    using Game.Settings;  // Settings UI attributes
    using Unity.Entities;

    public sealed partial class Setting
    {
        // Delivery vehicles (scalar)
        private float m_SemiTruckCargoScalar = 1f;
        private float m_DeliveryVanCargoScalar = 1f;
        private float m_OilTruckCargoScalar = 1f;
        private float m_MotorbikeDeliveryCargoScalar = 1f;

        private float m_ExtractorMaxTrucksScalar = 1f;
        private float m_CargoStationMaxTrucksScalar = 1f;

        [SettingsUISlider(min = ServiceMinScalar, max = ServiceMaxScalar, step = ServiceStepScalar)]
        [SettingsUISection(IndustryTab, DeliveryGroup)]
        public float SemiTruckCargoScalar
        {
            get => m_SemiTruckCargoScalar;
            set
            {
                float v = ScalarMath.ClampScalar(value, ServiceMinScalar, ServiceMaxScalar);
                if (m_SemiTruckCargoScalar == v)
                    return;

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
                if (m_DeliveryVanCargoScalar == v)
                    return;

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
                if (m_OilTruckCargoScalar == v)
                    return;

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
                if (m_MotorbikeDeliveryCargoScalar == v)
                    return;

                m_MotorbikeDeliveryCargoScalar = v;
                OnIndustryChanged();
            }
        }

        // Extractor and Cargo Station Buildings (scalar)

        [SettingsUISlider(min = CargoStationMinScalar, max = CargoStationMaxScalar, step = CargoStationStepScalar)]
        [SettingsUISection(IndustryTab, CargoStationsGroup)]
        public float ExtractorMaxTrucksScalar
        {
            get => m_ExtractorMaxTrucksScalar;
            set
            {
                float v = ScalarMath.ClampScalar(value, CargoStationMinScalar, CargoStationMaxScalar);
                if (m_ExtractorMaxTrucksScalar == v)
                    return;

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
                if (m_CargoStationMaxTrucksScalar == v)
                    return;

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
                if (!value)
                    return;

                SemiTruckCargoScalar = 1f;
                DeliveryVanCargoScalar = 1f;
                OilTruckCargoScalar = 1f;
                MotorbikeDeliveryCargoScalar = 1f;

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
                if (!value)
                    return;

                CargoStationMaxTrucksScalar = 1f;
                ExtractorMaxTrucksScalar = 1f;

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

            // Apply() already enables systems when options are applied.
            // This path enables IndustrySystem immediately while the city is running.
            TryEnableOnce<IndustrySystem>(world, "IndustrySystem");
        }
    }
}
