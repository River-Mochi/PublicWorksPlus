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

                // Tabs (match Setting.cs tab ids)
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "ÖPNV" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "Info" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Transitlinien (Schiebereglerbereich im Spiel)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Min./Max. der Transitlinien erweitern" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Erhöht den **Bereich** des Transitlinien-Schiebereglers im Spiel für jede Route.\n" +
                    "**Bis auf (1)** bei allen getesteten Routen.\n" +
                    "Das **Maximallimit variiert**; aber alle liegen 3× oder mehr über Vanilla.\n" +
                    "Technischer Hinweis: Das Spiel nutzt die Routenzeit (Fahrzeit + Haltestellenanzahl); dadurch entsteht ein variables Maximum (dieser Mod folgt der Spiellogik und setzt daher kein statisches Maximum wie 200).\n" +
                    "Funktioniert für alle Verkehrsmittel: Bus, Fähre, Straßenbahn, Zug, U-Bahn, Schiff, Flugzeug.\n\n" +
                    "**---------------**\n" +
                    "Tipp: Wenn das obere Ende des Schiebereglers noch etwas höher sein soll, der Route einige Haltestellen hinzufügen.\n" +
                    "Das Spiel erhöht das Maximum automatisch anhand zusätzlicher Haltestellen + Faktoren; zusätzliche Haltestellen sind eine einfache Anpassung für Spielende.\n" +
                    "<Konflikte vermeiden>: Mods entfernen, die dieselbe Transitlinien-Richtlinie bearbeiten.\n" +
                    "Deaktivieren, wenn die Funktion nicht benötigt wird oder wenn sie ausgeschaltet sein muss, um einen anderen Mod für dasselbe zu verwenden."
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Depotkapazität (max. Fahrzeuge pro Depot)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Busdepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Ändert, wie viele Busse jedes **Busdepot** warten/spawnen kann.\n" +
                    "**100%** = Vanilla (Spielstandard).\n" +
                    "**1000%** = 10× mehr.\n" +
                    "Gilt für das Basisgebäude." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Fährdepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**Fährdepot** max. Fahrzeuge pro Gebäude.\n" +
                    "**100%** = Vanilla (Spielstandard).\n" +
                    "Gilt für das Basisgebäude."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "U-Bahn-Depot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Ändert, wie viele U-Bahn-Fahrzeuge jedes **U-Bahn-Depot** warten kann.\n" +
                    "Gilt für das Basisgebäude."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Taxidepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Wie viele Taxis jedes **Taxidepot** warten kann.\n" +
                    "Bei maximaler Einstellung könnte das eine übertriebene, fast komische Menge an Taxis verursachen."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Straßenbahndepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Ändert, wie viele Straßenbahnen jedes **Straßenbahndepot** warten kann.\n" +
                    "Gilt für das Basisgebäude." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Zugdepot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Ändert, wie viele Züge jedes **Zugdepot** warten kann.\n" +
                    "Gilt für das Basisgebäude." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Depot-Standardwerte zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Alle Depot-Schieberegler wieder auf **100%** setzen (Spielstandard / Vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Passagierkapazität (max. Personen pro Fahrzeug)" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Bus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Ändert die **Bus-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Straßenbahn" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Ändert die **Straßenbahn-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Zug" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Ändert die **Zug-Passagier**kapazität.\n" +
                    "Gilt für Lokomotiven und Abschnitte.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "U-Bahn" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Ändert die **U-Bahn-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Schiff" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Ändert die Kapazität von **Passagierschiffen** (keine Frachtschiffe).\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Fähre" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Ändert die **Fähr-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Flugzeug" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Ändert die **Flugzeug-Passagier**kapazität.\n" +
                    "**10%** = 10% der Vanilla-Sitzplätze.\n" +
                    "**100%** = Vanilla-Sitzplätze (Spielstandard).\n" +
                    "**1000%** = 10× mehr Sitzplätze." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Verdoppeln" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Setzt jeden Passagier-Schieberegler auf **200%**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Alle Passagiere zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Alle Passagier-Schieberegler wieder auf **100%** setzen\n" +
                    "(Spielstandard / Vanilla)." },

               
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
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Öffnet die Paradox-Mods-Website für die Mods des Autors." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Öffnet den Community-Discord im Browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Ausführliche Debug-Logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Sendet zusätzliche Details zur Fehlersuche an <AdjustTransit.log>.\n" +
                    "Für normales Spielen **deaktivieren**.\n" +
                    "<Dies erhöht nur die Protokollierung und ändert keine Gameplay-Werte.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Log-Ordner öffnen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Öffnet den Log-Ordner.\n" +
                    "Danach: <AdjustTransit.log> mit einem Texteditor öffnen (Notepad++ empfohlen)." },
                
            };
        }

        public void Unload( )
        {
        }
    }
}
