// File: Localization/LocaleFR.cs
// French (fr-FR) strings for Options UI.

namespace AdjustTransit
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocaleFR : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleFR(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "Transports publics" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "À propos" },

                // --------------------
                // Public Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Lignes de transport (plage du curseur en jeu)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Lignes de transport étendues" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Étend la **plage** du curseur des lignes de transport en jeu pour chaque itinéraire.\n" +
                    "**Jusqu’à (1)** sur tous les itinéraires testés.\n" +
                    "La **limite maximale varie** ; tous les itinéraires testés ont atteint au moins 3× plus que le vanilla.\n" +
                    "Note technique : le jeu utilise le temps d’itinéraire (temps de conduite + nombre d’arrêts), donc le maximum est variable et non statique.\n" +
                    "Fonctionne pour bus, ferry, tram, train, métro, navire et avion.\n\n" +
                    "<Avertissement de conflit> : si Public Works Plus ou un autre mod modifie la même politique des lignes de transport, laisser cette fonction activée dans un seul mod."
                },

                // Depot capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Capacité des dépôts (véhicules max par dépôt)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Dépôt de bus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Modifie combien de bus chaque **dépôt de bus** peut entretenir ou générer.\n" +
                    "**100%** = vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus.\n" +
                    "S’applique au bâtiment de base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Dépôt de ferry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "Modifie combien de ferries chaque **dépôt de ferry** peut entretenir ou générer.\n" +
                    "**100%** = vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus.\n" +
                    "S’applique au bâtiment de base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "Dépôt de métro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Modifie combien de véhicules de métro chaque **dépôt de métro** peut entretenir.\n" +
                    "**100%** = vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus.\n" +
                    "S’applique au bâtiment de base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Dépôt de taxis" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Modifie combien de taxis chaque **dépôt de taxis** peut entretenir.\n" +
                    "**100%** = vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus.\n" +
                    "Des valeurs élevées peuvent créer un trafic de taxis excessif."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Dépôt de tram" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Modifie combien de trams chaque **dépôt de tram** peut entretenir.\n" +
                    "**100%** = vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus.\n" +
                    "S’applique au bâtiment de base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Dépôt de train" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Modifie combien de trains chaque **dépôt de train** peut entretenir.\n" +
                    "**100%** = vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus.\n" +
                    "S’applique au bâtiment de base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Réinitialiser les dépôts" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Remet tous les curseurs des dépôts à **100%** (valeur par défaut du jeu / vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Capacité passagers (max personnes par véhicule)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Bus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Modifie la capacité de **passagers des bus**.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Tram" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Modifie la capacité de **passagers des trams**.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Train" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Modifie la capacité de **passagers des trains**.\n" +
                    "S’applique aux locomotives et aux sections.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "Métro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Modifie la capacité de **passagers du métro**.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Navire" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Modifie la capacité des **navires à passagers** (pas des cargos).\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Ferry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Modifie la capacité de **passagers des ferries**.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Avion" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Modifie la capacité de **passagers des avions**.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Tout doubler" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Règle chaque curseur passagers sur **200%**."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Réinitialiser tous les passagers" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Remet tous les curseurs passagers à **100%** (valeur par défaut du jeu / vanilla)." },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Liens de support" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Débogage / Journalisation" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nom d’affichage de ce mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Version actuelle du mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Ouvre la page Paradox Mods de l’auteur." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Ouvre le Discord de la communauté dans un navigateur." },

                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Envoie des détails supplémentaires dans le fichier journal de ce mod pour le débogage.\n" +
                    "**Désactiver** pour une partie normale, car une journalisation excessive peut réduire les performances.\n" +
                    "<Cela augmente seulement la journalisation et ne change pas les valeurs de gameplay.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Ouvrir le dossier des logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Ouvre le dossier des logs de ce mod."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
