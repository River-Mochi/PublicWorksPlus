// File: Localization/LocalePL.cs
// Polish (pl-PL) strings for Options UI.

namespace AdjustTransit
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocalePL : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocalePL(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "Transport publiczny" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "Informacje" },

                // --------------------
                // Public Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Linie transportowe (zakres suwaka w grze)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Rozszerzone linie transportowe" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Rozszerza **zakres** suwaka linii transportowych w grze dla każdej trasy.\n" +
                    "**Nawet do (1)** na wszystkich testowanych trasach.\n" +
                    "**Maksymalny limit jest zmienny**; wszystkie testowane trasy osiągnęły co najmniej 3× więcej niż vanilla.\n" +
                    "Uwaga techniczna: gra używa czasu trasy (czas jazdy + liczba przystanków), więc maksimum jest zmienne, a nie stałe.\n" +
                    "Działa dla autobusu, promu, tramwaju, pociągu, metra, statku i samolotu.\n\n" +
                    "<Ostrzeżenie o konflikcie>: jeśli Public Works Plus lub inny mod edytuje tę samą politykę linii transportowych, tę funkcję pozostawić włączoną tylko w jednym modzie."
                },

                // Depot capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Pojemność zajezdni (maks. pojazdów na zajezdnię)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Zajezdnia autobusowa" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Zmienia liczbę autobusów, które każda **zajezdnia autobusowa** może utrzymać lub wystawić.\n" +
                    "**100%** = vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej.\n" +
                    "Dotyczy głównego budynku."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Zajezdnia promowa" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "Zmienia liczbę promów, które każda **zajezdnia promowa** może utrzymać lub wystawić.\n" +
                    "**100%** = vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej.\n" +
                    "Dotyczy głównego budynku."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "Zajezdnia metra" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Zmienia liczbę pojazdów metra, które każda **zajezdnia metra** może utrzymać.\n" +
                    "**100%** = vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej.\n" +
                    "Dotyczy głównego budynku."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Zajezdnia taksówek" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Zmienia liczbę taksówek, które każda **zajezdnia taksówek** może utrzymać.\n" +
                    "**100%** = vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej.\n" +
                    "Wysokie wartości mogą powodować nadmierny ruch taksówek."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Zajezdnia tramwajowa" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Zmienia liczbę tramwajów, które każda **zajezdnia tramwajowa** może utrzymać.\n" +
                    "**100%** = vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej.\n" +
                    "Dotyczy głównego budynku."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Zajezdnia kolejowa" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Zmienia liczbę pociągów, które każda **zajezdnia kolejowa** może utrzymać.\n" +
                    "**100%** = vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej.\n" +
                    "Dotyczy głównego budynku."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Resetuj zajezdnie" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Przywraca wszystkie suwaki zajezdni do **100%** (domyślna wartość gry / vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Pojemność pasażerska (maks. osób na pojazd)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Autobus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Zmienia pojemność **pasażerów autobusu**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Tramwaj" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Zmienia pojemność **pasażerów tramwaju**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Pociąg" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Zmienia pojemność **pasażerów pociągu**.\n" +
                    "Dotyczy lokomotyw i wagonów.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "Metro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Zmienia pojemność **pasażerów metra**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Statek" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Zmienia pojemność **statków pasażerskich** (nie towarowych).\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Prom" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Zmienia pojemność **pasażerów promu**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Samolot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Zmienia pojemność **pasażerów samolotu**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Podwój wszystko" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Ustawia wszystkie suwaki pasażerów na **200%**."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Resetuj wszystkich pasażerów" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Przywraca wszystkie suwaki pasażerów do **100%** (domyślna wartość gry / vanilla)." },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Linki wsparcia" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Debug / Logi" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Wyświetlana nazwa tego moda." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Wersja" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Aktualna wersja moda." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Otwiera stronę autora w Paradox Mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Otwiera społecznościowy Discord w przeglądarce." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Szczegółowe logi debug" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Wysyła dodatkowe szczegóły do pliku logów tego moda w celu diagnozy problemów.\n" +
                    "**Wyłączyć** do normalnej gry.\n" +
                    "<To tylko zwiększa logowanie i nie zmienia wartości rozgrywki.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Otwórz folder logów" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Otwiera folder logów tego moda."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
