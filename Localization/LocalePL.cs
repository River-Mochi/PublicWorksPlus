// File: Localization/LocalePL.cs
// Polski, Polish (pl-PL) strings for Options UI.

namespace DispatchBoss
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
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "Przemysł" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "Parki-Drogi" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "O modzie" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Linie transportu (zakres suwaka w grze)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Rozszerz min/max linii transportu" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Zwiększa **zakres** suwaka linii transportu w grze dla każdej trasy.\n" +
                    "**Nawet do (1)** na wszystkich testowanych trasach.\n" +
                    "**Maksymalny limit jest zmienny**; ale wszystkie są 3x lub więcej wyższe niż w vanilli, np. 30-60\n" +
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

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "Pojazdy dostawcze (pojemność ładunku)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "Ciągniki siodłowe" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "Pojemność **ciągników siodłowych**.\n" +
                    "Obejmuje:\n" +
                    "* Naczepy specjalistycznego przemysłu (farmy, rybołówstwo, leśnictwo itp.).\n" +
                    "* Ciągniki siodłowe przewożące pocztę do/z terminali cargo (to nie to samo co lokalne dostawy poczty).\n" +
                    "**1× = 25t** (vanilla)\n" +
                    "**10×** = 10× więcej." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "Furgonetki dostawcze" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**Furgonetki dostawcze**\n" +
                    "**1× = 4t** (vanilla)\n" +
                    "**10×** = 10× więcej." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilTruckCargoScalar)), "Ciężarówki surowcowe" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilTruckCargoScalar)),
                    "**Ciężarówki surowcowe** (ropa, węgiel, ruda, kamień)\n" +
                    "**1× = 20t** (vanilla)\n" +
                    "**10×** = 10× więcej." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "Motocykl dostawczy" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**Dostawa motocyklem** zwykle przewozi farmaceutyki do szpitala/kliniki.\n" +
                    "**1× = 0.1t** (vanilla)\n" +
                    "**10×** = 10× więcej." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "Resetuj ustawienia dostaw" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "Ustaw mnożniki dostaw z powrotem na **1×** (domyślna wartość gry / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "Flota cargo (port, kolej, lotnisko)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "Maks. flota stacji cargo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "Mnożnik maksymalnej liczby aktywnych transporterów dla **stacji transportu cargo**.\n" +
                    "**1×** = vanilla, **5×** = 5× więcej." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "Flota zakładów wydobywczych" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "Mnożnik dla **maks. ciężarówek zakładów wydobywczych**\n" +
                    "(farmy, rybołówstwo, leśnictwo, ruda, ropa, węgiel, kamień).\n" +
                    "**1×** = vanilla, **5×** = 5× więcej." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "Resetuj flotę cargo + wydobycie" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "Ustaw mnożniki stacji cargo + wydobycia z powrotem na **1×** (domyślna wartość gry / vanilla)." },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "Utrzymanie parków" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "Pojemność zmiany roboczej" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "Mnożnik **pojemności zmiany roboczej** (pojemności pojazdu).\n" +
                    "Całkowita ilość pracy, jaką ciężarówka może wykonać, zanim wróci do budynku.\n" +
                    "Pomyśl: więcej zapasów = dłuższa praca w terenie." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "Tempo pojazdu" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "Mnożnik **tempa pracy pojazdu**.\n" +
                    "Tempo = ile pracy wykonuje w jednym ticku symulacji podczas postoju." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "Rozmiar floty zajezdni" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "Mnożnik dla **maksymalnej liczby pojazdów** budynku zajezdni.\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "Resetuj utrzymanie parków" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "Resetuj wszystkie wartości do **100%** (domyślna wartość gry / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "Utrzymanie dróg" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "Rozmiar floty zajezdni" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "Mnożnik dla **maksymalnej liczby pojazdów zajezdni** na budynek.\n" +
                    "Wyżej = więcej ciężarówek.\n" +
                    "<Uwaga dot. balansu: zbyt mało lub zbyt dużo może szkodzić ruchowi.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "Pojemność zmiany roboczej" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "Mnożnik **pojemności zmiany roboczej**.\n" +
                    "Całkowita ilość pracy, jaką ciężarówka może wykonać, zanim wróci do zajezdni.\n" +
                    "**Wyżej = mniej powrotów.**" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "Tempo napraw" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "Tempo = ile pracy wykonuje w jednym ticku symulacji podczas postoju.\n" +
                    "Ciężarówki nadal robią szybkie stop+ruszenie nawet przy najwyższym tempie (wykonują więcej pracy na jeden postój).\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "Zużycie dróg" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<NOWA funkcja Alpha>\n" +
                    "Kontroluje, jak szybko drogi niszczeją od czynników **czasu i ruchu**.\n" +
                    "**10%** = 10× wolniejsze zużycie (mniej potrzebnych napraw)\n" +
                    "**100%** = vanilla\n" +
                    "**500%** = 5× szybsze uszkodzenia (więcej potrzebnych napraw/ciężarówek)\n" +
                    "Jeśli współczynnik m_Wear <= 2.5, brak spowolnienia.\n" +
                    "Jeśli m_Wear >= 17.5, maksymalna kara, pojazdy są o 50% wolniejsze na drogach.\n" +
                    "Zobacz widok informacji o drogach: mocno uszkodzone drogi są zaznaczone na czerwono i spowalniają pojazdy."

                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "Resetuj utrzymanie dróg" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "Ustaw wszystkie wartości z powrotem na **100%** (domyślna wartość gry / vanilla)." },

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

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "Raport skanowania (prefaby)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "Tworzy <jednorazowy> raport do debugowania.\n" +
                    "Nie jest potrzebny do normalnej rozgrywki.\n" +
                    "Lokalizacja pliku: <ModsData/DispatchBoss/ScanReport-Prefabs.txt>\n" +
                    "Wskazówka: kliknij <raz>, jeśli status pokazuje Done > wtedy użyj <Otwórz folder raportów>." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "Status skanowania prefabów" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "Pokazuje stan skanowania: Idle / Queued / Running / Done / No Data.\n" +
                    "Queued/Running pokazuje upływ czasu; Done pokazuje czas trwania + godzinę zakończenia." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Szczegółowe logi debug" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Wysyła dodatkowe szczegóły do <DispatchBoss.log> do rozwiązywania problemów.\n" +
                    "Do normalnej rozgrywki **wyłącz**.\n" +
                    "<To tylko zwiększa logowanie i nie zmienia wartości rozgrywki.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Otwórz folder logów" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Otwiera folder logów.\n" +
                    "Następnie: otwórz <DispatchBoss.log> w edytorze tekstu (zalecany Notepad++)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "Otwórz folder raportów" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "Otwiera folder raportów.\n" +
                    "Następnie: otwórz <ScanReport-Prefabs.txt> w edytorze tekstu (np. Notepad++)." },

                // ---- Scan Report Status Text (format string templates) ----
                { "DB_SCAN_IDLE", "Bezczynny" },
                { "DB_SCAN_QUEUED_FMT", "W kolejce ({0})" },
                { "DB_SCAN_RUNNING_FMT", "Uruchomione ({0})" },
                { "DB_SCAN_DONE_FMT", "Gotowe ({0} | {1})" },
                { "DB_SCAN_FAILED", "Niepowodzenie" },
                { "DB_SCAN_FAIL_NO_CITY", "Najpierw wczytaj miasto" },
                { "DB_SCAN_UNKNOWN_TIME", "nieznany czas" },

            };
        }

        public void Unload( )
        {
        }
    }
}
