// File: Localization/LocaleDE.cs
// German (de-DE) strings for Options UI.

namespace AdjustTransit
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocaleDE : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleDE(Setting setting)
        {
            m_Setting = setting;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            string title = Mod.ShortName;

            if (!string.IsNullOrEmpty(Mod.ModVersion))
            {
                title = title + " (" + Mod.ModVersion + ")";
            }

            return new Dictionary<string, string>
            {
                // --------------------------
                // Mod title / tabs / groups
                // --------------------------

                { m_Setting.GetSettingsLocaleID(), title },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "ÖPNV" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "Info" },

                // --------------------
                // Public Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Transitlinien (Schiebereglerbereich im Spiel)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Erweiterte Transitlinien" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Erweitert den **Bereich** des Transitlinien-Schiebereglers im Spiel pro Route.\n" +
                    "**Bis auf (1)** bei allen getesteten Routen.\n" +
                    "Das **Maximallimit variiert**; alle getesteten Routen erreichten mindestens 3× mehr als Vanilla.\n" +
                    "Technischer Hinweis: Das Spiel verwendet die Routenzeit (Fahrzeit + Anzahl der Haltestellen), daher ist das Maximum variabel und nicht statisch.\n" +
                    "Funktioniert für Bus, Fähre, Straßenbahn, Zug, U-Bahn, Schiff und Flugzeug.\n\n" +
                    "<Konfliktwarnung>: Wenn Public Works Plus oder ein anderer Mod dieselbe Transitlinien-Richtlinie bearbeitet, diese Funktion nur in einem Mod aktiv lassen."
                },

                // Depot capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Depotkapazität (max. Fahrzeuge pro Depot)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Busdepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Ändert, wie viele Busse jedes **Busdepot** warten oder spawnen kann.\n" +
                    "**100%** = Vanilla (Spielstandard).\n" +
                    "**1000%** = 10× mehr.\n" +
                    "Gilt für das Basisgebäude."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Fährdepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "Ändert, wie viele Fähren jedes **Fährdepot** warten oder spawnen kann.\n" +
                    "**100%** = Vanilla (Spielstandard).\n" +
                    "**1000%** = 10× mehr.\n" +
                    "Gilt für das Basisgebäude."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "U-Bahn-Depot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Ändert, wie viele U-Bahn-Fahrzeuge jedes **U-Bahn-Depot** warten kann.\n" +
                    "**100%** = Vanilla (Spielstandard).\n" +
                    "**1000%** = 10× mehr.\n" +
                    "Gilt für das Basisgebäude."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Taxidepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Ändert, wie viele Taxis jedes **Taxidepot** warten kann.\n" +
                    "**100%** = Vanilla (Spielstandard).\n" +
                    "**1000%** = 10× mehr.\n" +
                    "Hohe Werte können übermäßigen Taxiverkehr verursachen."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Straßenbahndepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Ändert, wie viele Straßenbahnen jedes **Straßenbahndepot** warten kann.\n" +
                    "**100%** = Vanilla (Spielstandard).\n" +
                    "**1000%** = 10× mehr.\n" +
                    "Gilt für das Basisgebäude."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Zugdepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Ändert, wie viele Züge jedes **Zugdepot** warten kann.\n" +
                    "**100%** = Vanilla (Spielstandard).\n" +
                    "**1000%** = 10× mehr.\n" +
                    "Gilt für das Basisgebäude."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Depot-Standards zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Setzt alle Depot-Schieberegler auf **100%** zurück (Spielstandard / Vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Passagierkapazität (max. Personen pro Fahrzeug)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Bus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Ändert die **Bus-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Straßenbahn" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Ändert die **Straßenbahn-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Zug" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Ändert die **Zug-Passagier**kapazität.\n" +
                    "Gilt für Lokomotiven und Abschnitte.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "U-Bahn" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Ändert die **U-Bahn-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Schiff" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Ändert die Kapazität von **Passagierschiffen** (keine Frachtschiffe).\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Fähre" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Ändert die **Fähr-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Flugzeug" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Ändert die **Flugzeug-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Alle verdoppeln" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Setzt jeden Passagier-Schieberegler auf **200%**."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Alle Passagiere zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Setzt alle Passagier-Schieberegler auf **100%** zurück (Spielstandard / Vanilla)."
                },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Support-Links" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Debug / Protokollierung" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Anzeigename dieses Mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Aktuelle Mod-Version." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Öffnet die Paradox-Mods-Seite des Autors." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Öffnet den Community-Discord im Browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Ausführliche Debug-Logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Sendet zusätzliche Details zur Fehlersuche in die Logdatei dieses Mods.\n" +
                    "**Deaktivieren** für normales Spielen.\n" +
                    "<Dies erhöht nur die Protokollierung und ändert keine Gameplay-Werte.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Log-Ordner öffnen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Öffnet den Logs-Ordner dieses Mods."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
