// File: Localization/LocaleIT.cs
// Italian (it-IT) strings for Options UI.

namespace PublicWorksPlus
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocaleIT : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleIT(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "Trasporto pubblico" },
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "Industria" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "Parchi-Strade" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "Info" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Linee di trasporto (intervallo cursore in gioco)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Espandi min/max linee di trasporto" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Aumenta l’**intervallo** del cursore delle linee di trasporto in gioco per ogni percorso.\n" +
                    "**Fino a (1)** su tutti i percorsi testati.\n" +
                    "Il **limite massimo varia**; ma tutti sono 3× o più alti del vanilla.\n" +
                    "Nota tecnica: il gioco usa il tempo del percorso (tempo di guida + numero di fermate); questo crea un massimo variabile (questa mod segue la logica del gioco quindi non imposta un limite massimo statico come 200).\n" +
                    "Funziona per tutti i trasporti: autobus, traghetto, tram, treno, metropolitana, nave, aereo.\n\n" +
                    "**---------------**\n" +
                    "Suggerimento: se si vuole aumentare ancora un po’ il limite massimo del cursore, aggiungere alcune fermate al percorso.\n" +
                    "Il gioco aumenta automaticamente il massimo in base alle fermate aggiunte + fattori; aggiungere fermate è una modifica semplice per il giocatore.\n" +
                    "<Evita conflitti>: rimuovere le mod che modificano la stessa policy delle linee di trasporto.\n" +
                    "Disattivare se la funzione non serve o se deve restare disattivata per usare un’altra mod per la stessa cosa."
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Capacità depositi (veicoli max per deposito)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Deposito autobus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Cambia quanti autobus ogni **deposito autobus** può mantenere/generare.\n" +
                    "**100%** = vanilla (predefinito del gioco).\n" +
                    "**1000%** = 10× in più.\n" +
                    "Si applica all’edificio base." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Deposito traghetti" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**Deposito traghetti** max veicoli per edificio.\n" +
                    "**100%** = vanilla (predefinito del gioco).\n" +
                    "Si applica all’edificio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "Deposito metropolitana" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Cambia quanti veicoli metropolitana ogni **deposito metropolitana** può mantenere.\n" +
                    "Si applica all’edificio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Deposito taxi" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Quanti taxi ogni **deposito taxi** può mantenere.\n" +
                    "Se impostato al massimo, potrebbe causare una quantità eccessiva e comica di taxi."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Deposito tram" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Cambia quanti tram ogni **deposito tram** può mantenere.\n" +
                    "Si applica all’edificio base." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Deposito treni" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Cambia quanti treni ogni **deposito treni** può mantenere.\n" +
                    "Si applica all’edificio base." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Reimposta depositi" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Riporta tutti i cursori dei depositi a **100%** (predefinito del gioco / vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Capacità passeggeri (persone max per veicolo)" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Autobus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Cambia la capacità **passeggeri autobus**.\n" +
                    "**10%** = 10% dei posti vanilla.\n" +
                    "**100%** = posti vanilla (predefinito del gioco).\n" +
                    "**1000%** = 10× più posti." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Tram" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Cambia la capacità **passeggeri tram**.\n" +
                    "**10%** = 10% dei posti vanilla.\n" +
                    "**100%** = posti vanilla (predefinito del gioco).\n" +
                    "**1000%** = 10× più posti." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Treno" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Cambia la capacità **passeggeri treno**.\n" +
                    "Si applica a motrici e sezioni.\n" +
                    "**10%** = 10% dei posti vanilla.\n" +
                    "**100%** = posti vanilla (predefinito del gioco).\n" +
                    "**1000%** = 10× più posti." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "Metropolitana" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Cambia la capacità **passeggeri metropolitana**.\n" +
                    "**10%** = 10% dei posti vanilla.\n" +
                    "**100%** = posti vanilla (predefinito del gioco).\n" +
                    "**1000%** = 10× più posti." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Nave" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Cambia la capacità delle **navi passeggeri** (non navi cargo).\n" +
                    "**100%** = posti vanilla (predefinito del gioco)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Traghetto" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Cambia la capacità **passeggeri traghetto**.\n" +
                    "**10%** = 10% dei posti vanilla.\n" +
                    "**100%** = posti vanilla (predefinito del gioco).\n" +
                    "**1000%** = 10× più posti." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Aereo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Cambia la capacità **passeggeri aereo**.\n" +
                    "**10%** = 10% dei posti vanilla.\n" +
                    "**100%** = posti vanilla (predefinito del gioco).\n" +
                    "**1000%** = 10× più posti." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Raddoppia" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Imposta ogni cursore passeggeri a **200%**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Reimposta tutti i passeggeri" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Riporta tutti i cursori passeggeri a **100%**\n" +
                    "(predefinito del gioco / vanilla)." },

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "Veicoli di consegna (capacità carico)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "Autoarticolati" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "Capacità degli **autoarticolati**.\n" +
                    "**100% = 25t** (vanilla)\n" +
                    "**500% = 125t**.\n" +
                    "Include:\n" +
                    " - Autoarticolati dell’industria specializzata (fattorie, pesca, silvicoltura, ecc.).\n" +
                    "Nota: include gli autoarticolati che trasportano posta da/verso le stazioni cargo.\n" +
                    "Non è la stessa cosa della consegna postale locale."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "Furgoni di consegna" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**Furgoni di consegna**\n" +
                    "**100% = 4t** (vanilla)\n" +
                    "**500% = 20t**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CoalTruckScalar)), "Camion materie prime" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CoalTruckScalar)),
                    "**Camion materie prime** (petrolio, carbone, minerale, pietra, camion ribaltabili per rifiuti industriali - stesso tipo di camion condiviso)\n" +
                    "**100% = 20t** (vanilla)\n" +
                    "**500% = 100t**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "Moto di consegna" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**La consegna in moto** porta in genere prodotti farmaceutici a un ospedale/clinica.\n" +
                    "**100% = 0.1t** (vanilla)\n" +
                    "**500% = 0.5t**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "Reimposta consegne" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "Riporta i cursori di consegna a **100%** (predefinito del gioco / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "Flotta cargo (porto, treno, aeroporto)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "Flotta max stazione cargo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "Modifica il massimo dei trasportatori attivi delle **stazioni di trasporto cargo**.\n" +
                    "**1×** = vanilla, **5×** = 5× in più." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "Flotta estrattori" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "Modifica il **numero massimo di camion** per gli estrattori industriali.\n" +
                    "(fattorie, pesca, silvicoltura, minerale, petrolio, carbone, pietra).\n" +
                    "**1×** = vanilla\n" +
                    "**5×** = 5 volte in più.\n" +
                    "Vanilla di solito consente 5 camion per edificio estrattore."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "Reimposta cargo + estrattori" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "Riporta i moltiplicatori di stazioni cargo + estrattori a **1×** (predefinito del gioco / vanilla)." },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "Manutenzione parchi" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "Capacità turno di lavoro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "Moltiplicatore per la **capacità turno di lavoro** (capacità veicolo).\n" +
                    "Lavoro totale che un camion può fare prima di tornare all’edificio.\n" +
                    "In pratica: più rifornimenti = resta fuori più a lungo." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "Velocità veicolo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "Moltiplicatore per la **velocità di lavoro del veicolo**.\n" +
                    "Velocità = quanto lavoro svolge per tick di simulazione mentre è fermo." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "Dimensione flotta deposito" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "Moltiplicatore per i **veicoli massimi** dell’edificio deposito.\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "Reimposta manutenzione parchi" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "Reimposta tutti i valori a **100%** (predefinito del gioco / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "Manutenzione strade" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "Dimensione flotta deposito" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "Moltiplicatore per i **veicoli massimi del deposito** per edificio.\n" +
                    "Più alto = più camion.\n" +
                    "<Nota bilanciamento: troppo pochi o troppi possono peggiorare il traffico.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "Capacità turno di lavoro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "Moltiplicatore per la **capacità turno di lavoro**.\n" +
                    "Lavoro totale che un camion può fare prima di tornare al deposito.\n" +
                    "**Più alto = meno ritorni** necessari verso l’edificio principale. Più efficiente." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "Velocità riparazione" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "Velocità = quanto lavoro svolge per tick di simulazione mentre è fermo.\n" +
                    "I camion fanno comunque una rapida sosta+ripartenza anche con la velocità più alta; fanno semplicemente più lavoro per sosta.\n" +
                    "In vanilla, una sola sosta non porta necessariamente la strada al 100% di riparazione, quindi questa funzione migliora nel tempo.\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "Usura stradale" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<Beta feature>\n" +
                    "Controlla la velocità con cui le strade si deteriorano per fattori di **tempo e traffico**.\n" +
                    "**10%** = usura 10× più lenta (meno riparazioni necessarie)\n" +
                    "**100%** = vanilla\n" +
                    "**500%** = danni 5× più rapidi (più riparazioni/camion necessari)\n" +
                    "Come funziona in gioco:\n" +
                    "Se fattore m_Wear <= 2.5, nessun rallentamento.\n" +
                    "Se m_Wear >= 17.5, penalità massima, i veicoli sono il 50% più lenti sulle strade.\n" +
                    "Vedi infovista Strade: mostra in rosso le strade molto danneggiate che rallentano i veicoli."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "Reimposta manutenzione strade" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "Riporta tutti i valori a **100%** (predefinito del gioco / vanilla)." },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Link di supporto" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Debug / Logging" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nome visualizzato di questa mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versione" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Versione attuale della mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Apre il sito Paradox Mods per le mod dell’autore." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Apre il Discord della community in un browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "Rapporto scansione (prefab)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "Crea un rapporto <una tantum> per il debug.\n" +
                    "Non serve per il gameplay normale.\n" +
                    "Posizione file: <ModsData/PublicWorksPlus/ScanReport-Prefabs.txt>\n" +
                    "Suggerimento: fare clic <una volta>; se lo stato mostra Completato, usare <Apri cartella rapporti>." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "Stato scansione prefab" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "Mostra lo stato della scansione: Inattivo / In coda / In esecuzione / Completato / Nessun dato.\n" +
                    "In coda/In esecuzione mostra il tempo trascorso; Completato mostra durata + ora di fine." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Log debug dettagliati" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Invia dettagli extra a <PublicWorksPlus.log> per la risoluzione dei problemi.\n" +
                    "**Disattivare** per il gameplay normale.\n" +
                    "<Questo aumenta solo la registrazione e non cambia i valori di gioco.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Apri cartella log" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Apre la cartella dei log.\n" +
                    "Poi: aprire <PublicWorksPlus.log> con l’editor di testo (Notepad++ consigliato)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "Apri cartella rapporti" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "Apre la cartella dei rapporti.\n" +
                    "Poi: aprire <ScanReport-Prefabs.txt> con l’editor di testo (es. Notepad++)." },

                // ---- Scan Report Status Text (format string templates) ----
                { "PWP_SCAN_IDLE", "Inattivo" },
                { "PWP_SCAN_QUEUED_FMT", "In coda ({0})" },
                { "PWP_SCAN_RUNNING_FMT", "In esecuzione ({0})" },
                { "PWP_SCAN_DONE_FMT", "Completato ({0} | {1})" },
                { "PWP_SCAN_FAILED", "Fallito" },
                { "PWP_SCAN_FAIL_NO_CITY", "Caricare prima una città" },
                { "PWP_SCAN_UNKNOWN_TIME", "ora sconosciuta" },

            };
        }

        public void Unload( )
        {
        }
    }
}
