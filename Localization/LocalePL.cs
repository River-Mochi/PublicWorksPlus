// File: Localization/LocalePL.cs
// Polski, Polish (pl-PL) strings for Options UI.

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

                // Tabs (match Setting.cs tab ids)
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "Transport publiczny" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "O modzie" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Linie transportu (zakres suwaka w grze)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Rozszerz min/max linii transportu" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Zwiększa **zakres** suwaka linii transportu w grze dla każdej trasy.\n" +
                    "**Nawet do (1)** na wszystkich testowanych trasach.\n" +
                    "**Maksymalny limit jest zmienny**; ale wszystkie są 3× lub więcej wyższe niż w vanilli.\n" +
                    "Uwaga techniczna: gra używa czasu trasy (czas jazdy + liczba przystanków); to tworzy zmienne maksimum (ten mod trzyma się logiki gry, więc nie ustawia stałego maksimum jak 200).\n" +
                    "Działa dla całego transportu: autobus, prom, tramwaj, pociąg, metro, statek, samolot.\n\n" +
                    "**---------------**\n" +
                    "Wskazówka: jeśli chcesz trochę bardziej zwiększyć górny koniec suwaka, dodaj kilka przystanków do trasy.\n" +
                    "Gra automatycznie zwiększa maksimum na podstawie dodanych przystanków + czynników; dodanie przystanków to łatwa korekta dla gracza.\n" +
                    "<Unikaj konfliktów>: usuń mody, które edytują tę samą politykę linii transportu.\n" +
                    "Wyłącz, jeśli ta funkcja nie jest potrzebna albo jeśli musi być wyłączona, by użyć innego moda robiącego to samo."
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Pojemność zajezdni (maks. pojazdów na zajezdnię)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Zajezdnia autobusowa" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Zmieniaj liczbę autobusów, które każda **zajezdnia autobusowa** może utrzymać/wypuścić.\n" +
                    "**100%** = vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej.\n" +
                    "Dotyczy podstawowego budynku." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Zajezdnia promowa" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**Zajezdnia promowa**: maks. pojazdów na budynek.\n" +
                    "**100%** = vanilla (domyślna wartość gry).\n" +
                    "Dotyczy podstawowego budynku."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "Zajezdnia metra" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Zmieniaj liczbę pojazdów metra, które każda **zajezdnia metra** może utrzymać.\n" +
                    "Dotyczy podstawowego budynku."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Zajezdnia taksówek" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Ile taksówek może utrzymać każda **zajezdnia taksówek**.\n" +
                    "Ustawienie na maksimum może spowodować przesadnie dużą, wręcz komiczną liczbę taksówek."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Zajezdnia tramwajowa" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Zmieniaj liczbę tramwajów, które każda **zajezdnia tramwajowa** może utrzymać.\n" +
                    "Dotyczy podstawowego budynku." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Zajezdnia kolejowa" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Zmieniaj liczbę pociągów, które każda **zajezdnia kolejowa** może utrzymać.\n" +
                    "Dotyczy podstawowego budynku." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Resetuj ustawienia zajezdni" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Ustaw wszystkie suwaki zajezdni z powrotem na **100%** (domyślna wartość gry / vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Pojemność pasażerska (maks. osób na pojazd)" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Autobus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Zmieniaj pojemność **pasażerów autobusów**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Tramwaj" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Zmieniaj pojemność **pasażerów tramwajów**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Pociąg" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Zmieniaj pojemność **pasażerów pociągów**.\n" +
                    "Dotyczy lokomotyw i sekcji.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "Metro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Zmieniaj pojemność **pasażerów metra**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Statek" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Zmieniaj pojemność **statków pasażerskich** (nie statków towarowych).\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Prom" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Zmieniaj pojemność **pasażerów promów**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Samolot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Zmieniaj pojemność **pasażerów samolotów**.\n" +
                    "**10%** = 10% miejsc vanilla.\n" +
                    "**100%** = miejsca vanilla (domyślna wartość gry).\n" +
                    "**1000%** = 10× więcej miejsc." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Podwój" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Ustaw każdy suwak pasażerów na **200%**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Resetuj wszystkich pasażerów" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Ustaw wszystkie suwaki pasażerów z powrotem na **100%**\n" +
                    "(domyślna wartość gry / vanilla)." },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Linki wsparcia" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Debug / Logowanie" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Wyświetlana nazwa tego moda." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Wersja" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Aktualna wersja moda." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Otwiera stronę Paradox Mods z modami autora." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Otwiera społecznościowy Discord w przeglądarce." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Szczegółowe logi debug" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Wysyła dodatkowe szczegóły do <AdjustTransit.log> do rozwiązywania problemów.\n" +
                    "Do normalnej rozgrywki **wyłącz**.\n" +
                    "<To tylko zwiększa logowanie i nie zmienia wartości rozgrywki.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Otwórz folder logów" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Otwiera folder logów.\n" +
                    "Następnie: otwórz <AdjustTransit.log> w edytorze tekstu (zalecany Notepad++)." },

            };
        }

        public void Unload( )
        {
        }
    }
}
