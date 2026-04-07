// File: Localization/LocaleIT.cs
// Italian (it-IT) strings for Options UI.

namespace AdjustTransit
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

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Log debug dettagliati" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Invia dettagli extra a <AdjustTransit.log> per la risoluzione dei problemi.\n" +
                    "**Disattivare** per il gameplay normale.\n" +
                    "<Questo aumenta solo la registrazione e non cambia i valori di gioco.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Apri cartella log" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Apre la cartella dei log.\n" +
                    "Poi: aprire <AdjustTransit.log> con l’editor di testo (Notepad++ consigliato)." },

            };
        }

        public void Unload( )
        {
        }
    }
}
