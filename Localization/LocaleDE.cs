// File: Localization/LocaleDE.cs
// German (de-DE) strings for Options UI.

namespace DispatchBoss
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
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "Industrie" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "Parks-Straßen" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "Info" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Transitlinien (Schiebereglerbereich im Spiel)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Min./Max. der Transitlinien erweitern" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Erhöht den **Bereich** des Transitlinien-Schiebereglers im Spiel für jede Route.\n" +
                    "**Bis auf (1)** bei allen getesteten Routen.\n" +
                    "Das **Maximallimit variiert**; aber alle liegen 3x oder mehr über Vanilla, z. B. 30-60\n" +
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

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "Lieferfahrzeuge (Frachtkapazität)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "Sattelzüge" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "Kapazität der **Sattelzüge**.\n" +
                    "Enthält:\n" +
                    "* Spezialindustrie-Sattelzüge (Farmen, Fischerei, Forstwirtschaft usw.).\n" +
                    "* Sattelzüge, die Post zu/von Frachtstationen transportieren (nicht dasselbe wie lokale Postzustellung).\n" +
                    "**1× = 25t** (Vanilla)\n" +
                    "**10×** = 10× mehr." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "Lieferwagen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**Lieferwagen**\n" +
                    "**1× = 4t** (Vanilla)\n" +
                    "**10×** = 10× mehr." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilTruckCargoScalar)), "Rohstoff-LKW" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilTruckCargoScalar)),
                    "**Rohstoff-LKW** (Öl, Kohle, Erz, Stein)\n" +
                    "**1× = 20t** (Vanilla)\n" +
                    "**10×** = 10× mehr." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "Liefermotorrad" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**Motorradlieferung** bringt typischerweise Medikamente zu einem Krankenhaus/einer Klinik.\n" +
                    "**1× = 0.1t** (Vanilla)\n" +
                    "**10×** = 10× mehr." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "Liefer-Standardwerte zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "Liefer-Multiplikatoren wieder auf **1×** setzen (Spielstandard / Vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "Frachtflotte (Hafen, Zug, Flughafen)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "Max. Frachtstationsflotte" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "Multiplikator für die maximale Anzahl aktiver Transporter von **Frachttransportstationen**.\n" +
                    "**1×** = Vanilla, **5×** = 5× mehr." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "Fördererflotte" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "Multiplikator für die **maximalen LKWs industrieller Förderer**\n" +
                    "(Farmen, Fischerei, Forstwirtschaft, Erz, Öl, Kohle, Stein).\n" +
                    "**1×** = Vanilla, **5×** = 5× mehr." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "Fracht + Förderer zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "Frachtstations- + Förderer-Multiplikatoren wieder auf **1×** setzen (Spielstandard / Vanilla)." },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "Parkwartung" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "Arbeitsschichtkapazität" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "Multiplikator für die **Arbeitsschichtkapazität** (Fahrzeugkapazität).\n" +
                    "Gesamtarbeit, die ein LKW leisten kann, bevor er zum Gebäude zurückkehrt.\n" +
                    "Einfach gesagt: mehr Vorräte = länger unterwegs." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "Fahrzeugrate" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "Multiplikator für die **Fahrzeugarbeitsrate**.\n" +
                    "Rate = wie viel Arbeit es pro Simulationstick im Stand erledigt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "Depotflottengröße" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "Multiplikator für die **maximalen Fahrzeuge** des Depotgebäudes.\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "Parkwartung zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "Alle Werte wieder auf **100%** setzen (Spielstandard / Vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "Straßenwartung" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "Depotflottengröße" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "Multiplikator für die **maximalen Depotfahrzeuge** pro Gebäude.\n" +
                    "Höher = mehr LKWs.\n" +
                    "<Balance-Hinweis: zu wenige oder zu viele können dem Verkehr schaden.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "Arbeitsschichtkapazität" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "Multiplikator für die **Arbeitsschichtkapazität**.\n" +
                    "Gesamtarbeit, die ein LKW leisten kann, bevor er zum Depot zurückkehrt.\n" +
                    "**Höher = weniger Rückfahrten.**" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "Reparaturrate" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "Rate = wie viel Arbeit es pro Simulationstick im Stand erledigt.\n" +
                    "LKWs machen selbst bei höchster Rate noch einen kurzen Stopp+Losfahr-Moment (sie erledigen mehr Arbeit pro Stopp).\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "Straßenverschleiß" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<NEUE Alpha-Funktion>\n" +
                    "Steuert, wie schnell Straßen durch **Zeit- und Verkehrs**faktoren verschleißen.\n" +
                    "**10%** = 10× langsamerer Verschleiß (weniger Reparaturen nötig)\n" +
                    "**100%** = Vanilla\n" +
                    "**500%** = 5× schnellerer Schaden (mehr Reparaturen/LKWs nötig)\n" +
                    "Wenn Faktor m_Wear <= 2.5, keine Verlangsamung.\n" +
                    "Wenn m_Wear >= 17.5, maximale Strafe, Fahrzeuge sind auf Straßen 50% langsamer.\n" +
                    "Siehe Straßen-Infoview: stark beschädigte Straßen werden rot angezeigt und verlangsamen Fahrzeuge."

                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "Straßenwartung zurücksetzen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "Alle Werte wieder auf **100%** setzen (Spielstandard / Vanilla)." },

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

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "Scan-Bericht (Prefabs)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "Erstellt einen <einmaligen> Bericht zum Debuggen.\n" +
                    "Für normales Spielen nicht erforderlich.\n" +
                    "Dateispeicherort: <ModsData/DispatchBoss/ScanReport-Prefabs.txt>\n" +
                    "Tipp: <einmal> klicken; wenn der Status Fertig anzeigt, dann <Berichtsordner öffnen> verwenden." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "Prefab-Scanstatus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "Zeigt den Scanstatus: Idle / Queued / Running / Done / No Data.\n" +
                    "Queued/Running zeigt die verstrichene Zeit; Done zeigt Dauer + Endzeit." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Ausführliche Debug-Logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Sendet zusätzliche Details zur Fehlersuche an <DispatchBoss.log>.\n" +
                    "Für normales Spielen **deaktivieren**.\n" +
                    "<Dies erhöht nur die Protokollierung und ändert keine Gameplay-Werte.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Log-Ordner öffnen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Öffnet den Log-Ordner.\n" +
                    "Danach: <DispatchBoss.log> mit einem Texteditor öffnen (Notepad++ empfohlen)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "Berichtsordner öffnen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "Öffnet den Berichtsordner.\n" +
                    "Danach: <ScanReport-Prefabs.txt> mit einem Texteditor öffnen (z. B. Notepad++)." },

                // ---- Scan Report Status Text (format string templates) ----
                { "DB_SCAN_IDLE", "Leerlauf" },
                { "DB_SCAN_QUEUED_FMT", "In Warteschlange ({0})" },
                { "DB_SCAN_RUNNING_FMT", "Läuft ({0})" },
                { "DB_SCAN_DONE_FMT", "Fertig ({0} | {1})" },
                { "DB_SCAN_FAILED", "Fehlgeschlagen" },
                { "DB_SCAN_FAIL_NO_CITY", "Zuerst Stadt laden" },
                { "DB_SCAN_UNKNOWN_TIME", "unbekannte Zeit" },

            };
        }

        public void Unload( )
        {
        }
    }
}
