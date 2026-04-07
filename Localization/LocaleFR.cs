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

                // Tabs (match Setting.cs tab ids)
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "Transports publics" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "À propos" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Lignes de transport (plage du curseur en jeu)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Étendre le min/max des lignes de transport" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Augmente la **plage** du curseur des lignes de transport en jeu pour chaque itinéraire.\n" +
                    "**Jusqu’à (1)** sur tous les itinéraires testés.\n" +
                    "La **limite maximale varie** ; mais toutes sont 3× ou plus au-dessus du vanilla.\n" +
                    "Note technique : le jeu utilise le temps d’itinéraire (temps de conduite + nombre d’arrêts) ; cela crée un maximum variable (ce mod suit la logique du jeu et ne définit donc pas de limite maximale statique comme 200).\n" +
                    "Fonctionne pour tous les transports : bus, ferry, tram, train, métro, navire, avion.\n\n" +
                    "**---------------**\n" +
                    "Astuce : si le maximum du curseur doit être encore un peu plus élevé, ajouter quelques arrêts à l’itinéraire.\n" +
                    "Le jeu augmente automatiquement le maximum selon les arrêts ajoutés + des facteurs ; ajouter des arrêts est un ajustement simple pour le joueur.\n" +
                    "<Éviter les conflits> : retirer les mods qui modifient la même politique des lignes de transport.\n" +
                    "Désactiver si la fonctionnalité n’est pas nécessaire ou si elle doit être désactivée pour utiliser un autre mod pour la même chose."
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Capacité des dépôts (véhicules max par dépôt)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Dépôt de bus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Modifie combien de bus chaque **dépôt de bus** peut entretenir/générer.\n" +
                    "**100%** = vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus.\n" +
                    "S’applique au bâtiment de base." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Dépôt de ferry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**Dépôt de ferry** : véhicules max par bâtiment.\n" +
                    "**100%** = vanilla (valeur par défaut du jeu).\n" +
                    "S’applique au bâtiment de base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "Dépôt de métro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Modifie combien de véhicules de métro chaque **dépôt de métro** peut entretenir.\n" +
                    "S’applique au bâtiment de base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Dépôt de taxis" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Combien de taxis chaque **dépôt de taxis** peut entretenir.\n" +
                    "Si réglé au maximum, cela pourrait provoquer une quantité excessive et comique de taxis."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Dépôt de tram" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Modifie combien de trams chaque **dépôt de tram** peut entretenir.\n" +
                    "S’applique au bâtiment de base." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Dépôt de train" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Modifie combien de trains chaque **dépôt de train** peut entretenir.\n" +
                    "S’applique au bâtiment de base." },

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
                    "**1000%** = 10× plus de places." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Tram" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Modifie la capacité de **passagers des trams**.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Train" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Modifie la capacité de **passagers des trains**.\n" +
                    "S’applique aux locomotives et aux sections.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "Métro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Modifie la capacité de **passagers du métro**.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Navire" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Modifie la capacité des **navires à passagers** (pas des cargos).\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Ferry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Modifie la capacité de **passagers des ferries**.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Avion" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Modifie la capacité de **passagers des avions**.\n" +
                    "**10%** = 10% des places vanilla.\n" +
                    "**100%** = places vanilla (valeur par défaut du jeu).\n" +
                    "**1000%** = 10× plus de places." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Doubler" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Règle chaque curseur passagers sur **200%**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Réinitialiser tous les passagers" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Remet tous les curseurs passagers à **100%**\n" +
                    "(valeur par défaut du jeu / vanilla)." },

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
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Ouvre le site Paradox Mods pour les mods de l’auteur." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Ouvre le Discord de la communauté dans un navigateur." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Journaux debug détaillés" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Envoie des détails supplémentaires dans <AdjustTransit.log> pour le dépannage.\n" +
                    "**Désactiver** pour une partie normale.\n" +
                    "<Cela augmente seulement la journalisation et ne change pas les valeurs de gameplay.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Ouvrir le dossier des logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Ouvre le dossier des logs.\n" +
                    "Ensuite : ouvrir <AdjustTransit.log> avec un éditeur de texte (Notepad++ recommandé)." },
            };
        }

        public void Unload( )
        {
        }
    }
}
