// File: Settings/Setting.Transit.cs
// Purpose: Public-Transit tab settings (line policy toggle, depot capacity, passenger capacity).
// Notes:
// - Ferry depot slider is included (FerryDepotScalar).
// - Reset helpers live here to keep transit settings cohesive.

namespace DispatchBoss
{
    using Game;
    using Game.SceneFlow; // GameManager
    using Game.Settings;  // Settings UI attributes
    using Game.UI;        // Unit
    using Unity.Entities;

    public sealed partial class Setting
    {
        private bool m_EnableLineVehicleCountTuner;

        // Toggle vanilla transit line range tuner (global policy).
        [SettingsUISection(PublicTransitTab, LineVehiclesGroup)]
        public bool EnableLineVehicleCountTuner
        {
            get => m_EnableLineVehicleCountTuner;
            set
            {
                if (m_EnableLineVehicleCountTuner == value)
                    return;

                m_EnableLineVehicleCountTuner = value;

                // Apply immediately if a city is loaded (no auto-persist on toggle).
                GameManager gm = GameManager.instance;
                if (gm != null && gm.gameMode.IsGame())
                {
                    Apply();
                }
            }
        }

        // ------------------------
        // DEPOT Buildings (percent)
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
                    return;

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
                    return;

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
                    return;

                ResetPassengerToVanilla();
                ApplyAndSave();
            }
        }

        // ------------------------
        // Helpers
        // ------------------------

        public void ResetDepotToVanilla()
        {
            BusDepotScalar = 100f;
            FerryDepotScalar = 100f;
            SubwayDepotScalar = 100f;
            TaxiDepotScalar = 100f;
            TrainDepotScalar = 100f;
            TramDepotScalar = 100f;
        }

        public void ResetPassengerToVanilla()
        {
            AirplanePassengerScalar = 100f;
            BusPassengerScalar = 100f;
            FerryPassengerScalar = 100f;
            ShipPassengerScalar = 100f;
            SubwayPassengerScalar = 100f;
            TrainPassengerScalar = 100f;
            TramPassengerScalar = 100f;
        }
    }
}
