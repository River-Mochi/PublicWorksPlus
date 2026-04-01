// File: Localization/LocaleFR.cs
// French (fr-FR) strings for Options UI.

namespace PublicWorksPlus
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
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "Industrie" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "Parcs-Routes" },
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

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "Véhicules de livraison (capacité de charge)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "Semi-remorques" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "Capacité des **semi-remorques**.\n" +
                    "Comprend :\n" +
                    "* Semi-remorques d’industrie spécialisée (fermes, pêche, foresterie, etc.).\n" +
                    "* Semi-remorques transportant du courrier vers/depuis les gares de fret (pas la même chose que la distribution locale du courrier).\n" +
                    "**1× = 25t** (vanilla)\n" +
                    "**10×** = 10× plus." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "Fourgonnettes de livraison" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**Fourgonnettes de livraison**\n" +
                    "**1× = 4t** (vanilla)\n" +
                    "**10×** = 10× plus." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilTruckCargoScalar)), "Camions de matières premières" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilTruckCargoScalar)),
                    "**Camions de matières premières** (pétrole, charbon, minerai, pierre)\n" +
                    "**1× = 20t** (vanilla)\n" +
                    "**10×** = 10× plus." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "Moto de livraison" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**La livraison à moto** transporte généralement des produits pharmaceutiques vers un hôpital/une clinique.\n" +
                    "**1× = 0.1t** (vanilla)\n" +
                    "**10×** = 10× plus." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "Réinitialiser les livraisons" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "Remet les multiplicateurs de livraison à **1×** (valeur par défaut du jeu / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "Flotte de fret (port, train, aéroport)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "Flotte max des gares de fret" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "Multiplicateur pour le maximum de transporteurs actifs des **stations de transport de fret**.\n" +
                    "**1×** = vanilla, **5×** = 5× plus." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "Flotte des extracteurs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "Multiplicateur pour les **camions max des extracteurs** industriels\n" +
                    "(fermes, pêche, foresterie, minerai, pétrole, charbon, pierre).\n" +
                    "**1×** = vanilla, **5×** = 5× plus." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "Réinitialiser fret + extracteurs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "Remet les multiplicateurs des gares de fret + extracteurs à **1×** (valeur par défaut du jeu / vanilla)." },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "Entretien des parcs" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "Capacité du quart de travail" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "Multiplicateur pour la **capacité du quart de travail** (capacité du véhicule).\n" +
                    "Travail total qu’un camion peut effectuer avant de retourner au bâtiment.\n" +
                    "En clair : plus de fournitures = reste dehors plus longtemps." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "Cadence du véhicule" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "Multiplicateur pour la **cadence de travail du véhicule**.\n" +
                    "Cadence = quantité de travail effectuée par tick de simulation à l’arrêt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "Taille de flotte du dépôt" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "Multiplicateur pour les **véhicules maximum** du bâtiment dépôt.\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "Réinitialiser l’entretien des parcs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "Remet toutes les valeurs à **100%** (valeur par défaut du jeu / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "Entretien des routes" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "Taille de flotte du dépôt" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "Multiplicateur pour les **véhicules maximum du dépôt** par bâtiment.\n" +
                    "Plus élevé = plus de camions.\n" +
                    "<Note d’équilibrage : trop peu ou trop peuvent nuire au trafic.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "Capacité du quart de travail" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "Multiplicateur pour la **capacité du quart de travail**.\n" +
                    "Travail total qu’un camion peut effectuer avant de retourner au dépôt.\n" +
                    "**Plus élevé = moins de retours** nécessaires vers le bâtiment principal. Plus efficace." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "Cadence de réparation" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "Cadence = quantité de travail effectuée par tick de simulation à l’arrêt.\n" +
                    "Les camions font quand même un arrêt+repart rapide même avec la cadence la plus élevée ; ils effectuent simplement plus de travail par arrêt.\n" +
                    "En vanilla, un seul arrêt ne ramène pas forcément la route à 100% de réparation, donc cette fonctionnalité devient meilleure avec le temps.\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "Usure des routes" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<NOUVELLE fonctionnalité Alpha>\n" +
                    "Contrôle la vitesse de détérioration des routes selon des facteurs de **temps et de trafic**.\n" +
                    "**10%** = usure 10× plus lente (moins de réparations nécessaires)\n" +
                    "**100%** = vanilla\n" +
                    "**500%** = dégâts 5× plus rapides (plus de réparations/camions nécessaires)\n" +
                    "Comment cela fonctionne en jeu :\n" +
                    "Si le facteur m_Wear <= 2.5, pas de ralentissement.\n" +
                    "Si m_Wear >= 17.5, pénalité maximale, les véhicules sont 50% plus lents sur les routes.\n" +
                    "Voir l’infovue Routes : les routes très endommagées apparaissent en rouge et ralentissent les véhicules."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "Réinitialiser l’entretien des routes" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "Remet toutes les valeurs à **100%** (valeur par défaut du jeu / vanilla)." },

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

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "Rapport d’analyse (prefabs)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "Crée un rapport <ponctuel> pour le débogage.\n" +
                    "Inutile pour une partie normale.\n" +
                    "Emplacement du fichier : <ModsData/PublicWorksPlus/ScanReport-Prefabs.txt>\n" +
                    "Astuce : cliquer <une fois>, puis si l’état affiche Terminé, utiliser <Ouvrir le dossier du rapport>." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "État de l’analyse des prefabs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "Affiche l’état de l’analyse : Inactif / En file / En cours / Terminé / Aucune donnée.\n" +
                    "En file/En cours affiche le temps écoulé ; Terminé affiche la durée + l’heure de fin." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Journaux debug détaillés" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Envoie des détails supplémentaires dans <PublicWorksPlus.log> pour le dépannage.\n" +
                    "**Désactiver** pour une partie normale.\n" +
                    "<Cela augmente seulement la journalisation et ne change pas les valeurs de gameplay.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Ouvrir le dossier des logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Ouvre le dossier des logs.\n" +
                    "Ensuite : ouvrir <PublicWorksPlus.log> avec un éditeur de texte (Notepad++ recommandé)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "Ouvrir le dossier du rapport" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "Ouvre le dossier du rapport.\n" +
                    "Ensuite : ouvrir <ScanReport-Prefabs.txt> avec un éditeur de texte (par ex. Notepad++)." },

                // ---- Scan Report Status Text (format string templates) ----
                { "PWP_SCAN_IDLE", "Inactif" },
                { "PWP_SCAN_QUEUED_FMT", "En file ({0})" },
                { "PWP_SCAN_RUNNING_FMT", "En cours ({0})" },
                { "PWP_SCAN_DONE_FMT", "Terminé ({0} | {1})" },
                { "PWP_SCAN_FAILED", "Échec" },
                { "PWP_SCAN_FAIL_NO_CITY", "Charger d’abord une ville" },
                { "PWP_SCAN_UNKNOWN_TIME", "heure inconnue" },

            };
        }

        public void Unload( )
        {
        }
    }
}
