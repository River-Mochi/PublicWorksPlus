// File: Settings/Setting.Transit.cs
// Purpose: Public Transit settings (depots, passengers, line vehicle policy toggle).

namespace AdjustTransit
{
    using Game;           // IsGame
    using Game.SceneFlow; // GameManager
    using Game.Settings;  // Settings UI attributes
    using Game.UI;        // Unit

    public sealed partial class Setting
    {
        private bool m_EnableLineVehicleCountTuner;

        // Toggle extended transit line slider range.
        [SettingsUISection(PublicTransitTab, LineVehiclesGroup)]
        public bool EnableLineVehicleCountTuner
        {
            get => m_EnableLineVehicleCountTuner;
            set
            {
                if (m_EnableLineVehicleCountTuner == value)
                {
                    return;
                }

                m_EnableLineVehicleCountTuner = value;

                // Apply immediately when a city is loaded.
                GameManager? gm = GameManager.instance;
                if (gm != null && gm.gameMode.IsGame())
                {
                    Apply();
                }
            }
        }

        // ------------------------
        // DEPOT buildings (percent)
        // ------------------------

        [SettingsUISlider(min = DepotMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, DepotGroup)]
        public float BusDepotScalar { get; set; }

        [SettingsUISlider(min = DepotMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, DepotGroup)]
        public float FerryDepotScalar { get; set; }

        [SettingsUISlider(min = DepotMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, DepotGroup)]
        public float SubwayDepotScalar { get; set; }

        [SettingsUISlider(min = DepotMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, DepotGroup)]
        public float TaxiDepotScalar { get; set; }

        [SettingsUISlider(min = DepotMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, DepotGroup)]
        public float TrainDepotScalar { get; set; }

        [SettingsUISlider(min = DepotMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, DepotGroup)]
        public float TramDepotScalar { get; set; }

        [SettingsUIButtonGroup(DepotGroup)]
        [SettingsUIButton]
        [SettingsUISection(PublicTransitTab, DepotGroup)]
        public bool ResetDepotToVanillaButton
        {
            set
            {
                if (!value)
                {
                    return;
                }

                ResetDepotToVanilla();
                ApplyAndSave();
            }
        }

        // ------------------------
        // PASSENGERS (percent)
        // ------------------------

        [SettingsUISlider(min = PassengerMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, PassengerGroup)]
        public float BusPassengerScalar { get; set; }

        [SettingsUISlider(min = PassengerMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, PassengerGroup)]
        public float TramPassengerScalar { get; set; }

        [SettingsUISlider(min = PassengerMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, PassengerGroup)]
        public float TrainPassengerScalar { get; set; }

        [SettingsUISlider(min = PassengerMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, PassengerGroup)]
        public float SubwayPassengerScalar { get; set; }

        [SettingsUISlider(min = PassengerMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, PassengerGroup)]
        public float ShipPassengerScalar { get; set; }

        [SettingsUISlider(min = PassengerMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, PassengerGroup)]
        public float FerryPassengerScalar { get; set; }

        [SettingsUISlider(min = PassengerMinPercent, max = MaxPercent, step = StepPercent, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(PublicTransitTab, PassengerGroup)]
        public float AirplanePassengerScalar { get; set; }

        [SettingsUIButtonGroup(PassengerGroup)]
        [SettingsUIButton]
        [SettingsUISection(PublicTransitTab, PassengerGroup)]
        public bool DoublePassengersButton
        {
            set
            {
                if (!value)
                {
                    return;
                }

                BusPassengerScalar = 200f;
                TramPassengerScalar = 200f;
                TrainPassengerScalar = 200f;
                SubwayPassengerScalar = 200f;
                ShipPassengerScalar = 200f;
                FerryPassengerScalar = 200f;
                AirplanePassengerScalar = 200f;

                ApplyAndSave();
            }
        }

        [SettingsUIButtonGroup(PassengerGroup)]
        [SettingsUIButton]
        [SettingsUISection(PublicTransitTab, PassengerGroup)]
        public bool ResetPassengerToVanillaButton
        {
            set
            {
                if (!value)
                {
                    return;
                }

                ResetPassengerToVanilla();
                ApplyAndSave();
            }
        }

        internal void ResetDepotToVanilla()
        {
            BusDepotScalar = kVanillaPercent;
            FerryDepotScalar = kVanillaPercent;
            SubwayDepotScalar = kVanillaPercent;
            TaxiDepotScalar = kVanillaPercent;
            TrainDepotScalar = kVanillaPercent;
            TramDepotScalar = kVanillaPercent;
        }

        internal void ResetPassengerToVanilla()
        {
            AirplanePassengerScalar = kVanillaPercent;
            BusPassengerScalar = kVanillaPercent;
            FerryPassengerScalar = kVanillaPercent;
            ShipPassengerScalar = kVanillaPercent;
            SubwayPassengerScalar = kVanillaPercent;
            TrainPassengerScalar = kVanillaPercent;
            TramPassengerScalar = kVanillaPercent;
        }

        partial void SetDefaults_Transit()
        {
            m_EnableLineVehicleCountTuner = true;

            ResetDepotToVanilla();
            ResetPassengerToVanilla();
        }

        partial void RepairAndClamp_Transit()
        {
            BusDepotScalar = ClampPercentOrVanilla(BusDepotScalar, DepotMinPercent, MaxPercent, kVanillaPercent);
            FerryDepotScalar = ClampPercentOrVanilla(FerryDepotScalar, DepotMinPercent, MaxPercent, kVanillaPercent);
            SubwayDepotScalar = ClampPercentOrVanilla(SubwayDepotScalar, DepotMinPercent, MaxPercent, kVanillaPercent);
            TaxiDepotScalar = ClampPercentOrVanilla(TaxiDepotScalar, DepotMinPercent, MaxPercent, kVanillaPercent);
            TrainDepotScalar = ClampPercentOrVanilla(TrainDepotScalar, DepotMinPercent, MaxPercent, kVanillaPercent);
            TramDepotScalar = ClampPercentOrVanilla(TramDepotScalar, DepotMinPercent, MaxPercent, kVanillaPercent);

            BusPassengerScalar = ClampPercentOrVanilla(BusPassengerScalar, PassengerMinPercent, MaxPercent, kVanillaPercent);
            TramPassengerScalar = ClampPercentOrVanilla(TramPassengerScalar, PassengerMinPercent, MaxPercent, kVanillaPercent);
            TrainPassengerScalar = ClampPercentOrVanilla(TrainPassengerScalar, PassengerMinPercent, MaxPercent, kVanillaPercent);
            SubwayPassengerScalar = ClampPercentOrVanilla(SubwayPassengerScalar, PassengerMinPercent, MaxPercent, kVanillaPercent);
            ShipPassengerScalar = ClampPercentOrVanilla(ShipPassengerScalar, PassengerMinPercent, MaxPercent, kVanillaPercent);
            FerryPassengerScalar = ClampPercentOrVanilla(FerryPassengerScalar, PassengerMinPercent, MaxPercent, kVanillaPercent);
            AirplanePassengerScalar = ClampPercentOrVanilla(AirplanePassengerScalar, PassengerMinPercent, MaxPercent, kVanillaPercent);
        }
    }
}
