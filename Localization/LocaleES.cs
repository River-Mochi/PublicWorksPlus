// File: Localization/LocaleES.cs
// Spanish (es-ES) strings for Options UI.

namespace PublicWorksPlus
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocaleES : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleES(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "Transporte público" },
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "Industria" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "Parques-Carreteras" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "Acerca de" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Líneas de transporte (rango del deslizador en juego)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Ampliar min/máx de líneas de transporte" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Aumenta el **rango** del deslizador de líneas de transporte en juego para cada ruta.\n" +
                    "**Tan bajo como (1)** en todas las rutas probadas.\n" +
                    "El **límite máximo varía**; pero todos son 3x o más altos que vanilla, por ejemplo, 30-60\n" +
                    "Nota técnica: el juego usa el tiempo de ruta (tiempo de conducción + número de paradas); esto crea un máximo variable (este mod sigue la lógica del juego y por eso no fija un máximo estático como 200).\n" +
                    "Funciona para todo el transporte: autobús, ferry, tranvía, tren, metro, barco, avión.\n\n" +
                    "**---------------**\n" +
                    "Consejo: si se quiere aumentar un poco más el máximo del deslizador, agregar algunas paradas a la ruta.\n" +
                    "El juego aumenta automáticamente el máximo según las paradas añadidas + factores; agregar paradas es un ajuste sencillo para el jugador.\n" +
                    "<Evitar conflictos>: quitar mods que editen la misma política de líneas de transporte.\n" +
                    "Desactivar si la función no es necesaria o si debe desactivarse para usar otro mod para lo mismo."
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Capacidad de depósitos (vehículos máx por depósito)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Depósito de autobuses" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Cambia cuántos autobuses puede mantener/generar cada **depósito de autobuses**.\n" +
                    "**100%** = vanilla (valor predeterminado del juego).\n" +
                    "**1000%** = 10× más.\n" +
                    "Se aplica al edificio base." },

                 { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Depósito de ferris" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**Depósito de ferris**: vehículos máximos por edificio.\n" +
                    "**100%** = vanilla (valor predeterminado del juego).\n" +
                    "Se aplica al edificio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "Depósito de metro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Cambia cuántos vehículos de metro puede mantener cada **depósito de metro**.\n" +
                    "Se aplica al edificio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Depósito de taxis" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Cuántos taxis puede mantener cada **depósito de taxis**.\n" +
                    "Si se pone al máximo, podría causar una cantidad excesiva y cómica de taxis."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Depósito de tranvías" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Cambia cuántos tranvías puede mantener cada **depósito de tranvías**.\n" +
                    "Se aplica al edificio base." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Depósito de trenes" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Cambia cuántos trenes puede mantener cada **depósito de trenes**.\n" +
                    "Se aplica al edificio base." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Restablecer depósitos" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Devuelve todos los deslizadores de depósitos a **100%** (valor predeterminado del juego / vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Capacidad de pasajeros (máx personas por vehículo)" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Autobús" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Cambia la capacidad de **pasajeros del autobús**.\n" +
                    "**10%** = 10% de los asientos vanilla.\n" +
                    "**100%** = asientos vanilla (valor predeterminado del juego).\n" +
                    "**1000%** = 10× más asientos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Tranvía" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Cambia la capacidad de **pasajeros del tranvía**.\n" +
                    "**10%** = 10% de los asientos vanilla.\n" +
                    "**100%** = asientos vanilla (valor predeterminado del juego).\n" +
                    "**1000%** = 10× más asientos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Tren" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Cambia la capacidad de **pasajeros del tren**.\n" +
                    "Se aplica a locomotoras y secciones.\n" +
                    "**10%** = 10% de los asientos vanilla.\n" +
                    "**100%** = asientos vanilla (valor predeterminado del juego).\n" +
                    "**1000%** = 10× más asientos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "Metro" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Cambia la capacidad de **pasajeros del metro**.\n" +
                    "**10%** = 10% de los asientos vanilla.\n" +
                    "**100%** = asientos vanilla (valor predeterminado del juego).\n" +
                    "**1000%** = 10× más asientos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Barco" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Cambia la capacidad de **barcos de pasajeros** (no barcos de carga).\n" +
                    "**100%** = asientos vanilla (valor predeterminado del juego)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Ferri" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Cambia la capacidad de **pasajeros del ferri**.\n" +
                    "**10%** = 10% de los asientos vanilla.\n" +
                    "**100%** = asientos vanilla (valor predeterminado del juego).\n" +
                    "**1000%** = 10× más asientos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Avión" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Cambia la capacidad de **pasajeros del avión**.\n" +
                    "**10%** = 10% de los asientos vanilla.\n" +
                    "**100%** = asientos vanilla (valor predeterminado del juego).\n" +
                    "**1000%** = 10× más asientos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Duplicar" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Ajusta todos los deslizadores de pasajeros a **200%**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Restablecer todos los pasajeros" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Devuelve todos los deslizadores de pasajeros a **100%**\n" +
                    "(valor predeterminado del juego / vanilla)." },

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "Vehículos de reparto (capacidad de carga)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "Camiones articulados" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "Capacidad de los **camiones articulados**.\n" +
                    "Incluye:\n" +
                    "* Camiones articulados de industria especializada (granjas, pesca, silvicultura, etc.).\n" +
                    "* Camiones articulados que llevan correo hacia/desde estaciones de carga (no es lo mismo que el reparto local de correo).\n" +
                    "**1× = 25t** (vanilla)\n" +
                    "**10×** = 10× más." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "Furgonetas de reparto" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**Furgonetas de reparto**\n" +
                    "**1× = 4t** (vanilla)\n" +
                    "**10×** = 10× más." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilTruckCargoScalar)), "Camiones de materias primas" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilTruckCargoScalar)),
                    "**Camiones de materias primas** (petróleo, carbón, mineral, piedra)\n" +
                    "**1× = 20t** (vanilla)\n" +
                    "**10×** = 10× más." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "Moto de reparto" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**El reparto en moto** normalmente lleva productos farmacéuticos a un hospital/clínica.\n" +
                    "**1× = 0.1t** (vanilla)\n" +
                    "**10×** = 10× más." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "Restablecer reparto" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "Devuelve los multiplicadores de reparto a **1×** (valor predeterminado del juego / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "Flota de carga (puerto, tren, aeropuerto)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "Flota máx de estación de carga" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "Multiplicador para los transportistas activos máximos de las **estaciones de transporte de carga**.\n" +
                    "**1×** = vanilla, **5×** = 5× más." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "Flota de extractores" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "Multiplicador para los **camiones máximos de extractores** industriales\n" +
                    "(granjas, pesca, silvicultura, mineral, petróleo, carbón, piedra).\n" +
                    "**1×** = vanilla, **5×** = 5× más." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "Restablecer carga + extractores" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "Devuelve los multiplicadores de estaciones de carga + extractores a **1×** (valor predeterminado del juego / vanilla)." },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "Mantenimiento de parques" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "Capacidad del turno de trabajo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "Multiplicador para la **capacidad del turno de trabajo** (capacidad del vehículo).\n" +
                    "Trabajo total que puede hacer un camión antes de volver al edificio.\n" +
                    "Piénsalo así: más suministros = permanece fuera más tiempo." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "Ritmo del vehículo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "Multiplicador para la **tasa de trabajo del vehículo**.\n" +
                    "Tasa = cuánto trabajo hace por tick de simulación mientras está parado." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "Tamaño de flota del depósito" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "Multiplicador para los **vehículos máximos** del edificio depósito.\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "Restablecer mantenimiento de parques" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "Restablece todos los valores a **100%** (valor predeterminado del juego / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "Mantenimiento de carreteras" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "Tamaño de flota del depósito" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "Multiplicador para los **vehículos máximos del depósito** por edificio.\n" +
                    "Más alto = más camiones.\n" +
                    "<Nota de equilibrio: demasiado pocos o demasiados pueden perjudicar el tráfico.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "Capacidad del turno de trabajo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "Multiplicador para la **capacidad del turno de trabajo**.\n" +
                    "Trabajo total que puede hacer un camión antes de volver al depósito.\n" +
                    "**Más alto = menos regresos.**" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "Tasa de reparación" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "Tasa = cuánto trabajo hace por tick de simulación mientras está parado.\n" +
                    "Los camiones aún hacen una parada+avance rápido incluso con la tasa más alta (hacen más trabajo por parada).\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "Desgaste de carreteras" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<NUEVA función Alpha>\n" +
                    "Controla qué tan rápido se deterioran las carreteras por factores de **tiempo y tráfico**.\n" +
                    "**10%** = desgaste 10× más lento (se necesitan menos reparaciones)\n" +
                    "**100%** = vanilla\n" +
                    "**500%** = daño 5× más rápido (se necesitan más reparaciones/camiones)\n" +
                    "Si el factor m_Wear <= 2.5, no hay ralentización.\n" +
                    "Si m_Wear >= 17.5, penalización máxima, los vehículos son 50% más lentos en las carreteras.\n" +
                    "Ver infovista de carreteras: muestra en rojo las carreteras muy dañadas que ralentizan a los vehículos."

                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "Restablecer mantenimiento de carreteras" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "Devuelve todos los valores a **100%** (valor predeterminado del juego / vanilla)." },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Enlaces de soporte" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Debug / Registro" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nombre mostrado de este mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versión" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Versión actual del mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Abre el sitio web de Paradox Mods para los mods del autor." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Abre el Discord de la comunidad en un navegador." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "Informe de escaneo (prefabs)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "Crea un informe <único> para depuración.\n" +
                    "No es necesario para una partida normal.\n" +
                    "Ubicación del archivo: <ModsData/PublicWorksPlus/ScanReport-Prefabs.txt>\n" +
                    "Consejo: haz clic <una vez>; si el estado muestra Hecho, usa <Abrir carpeta de informes>." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "Estado del escaneo de prefabs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "Muestra el estado del escaneo: Idle / Queued / Running / Done / No Data.\n" +
                    "Queued/Running muestra el tiempo transcurrido; Done muestra duración + hora de finalización." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Registros debug detallados" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Envía detalles adicionales a <PublicWorksPlus.log> para solucionar problemas.\n" +
                    "**Desactivar** para una partida normal.\n" +
                    "<Esto solo aumenta el registro y no cambia los valores de juego.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Abrir carpeta de logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Abre la carpeta de logs.\n" +
                    "Siguiente: abrir <PublicWorksPlus.log> con el editor de texto (se recomienda Notepad++)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "Abrir carpeta de informes" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "Abre la carpeta de informes.\n" +
                    "Siguiente: abrir <ScanReport-Prefabs.txt> con el editor de texto (por ejemplo, Notepad++)." },

                // ---- Scan Report Status Text (format string templates) ----
                { "PWP_SCAN_IDLE", "Inactivo" },
                { "PWP_SCAN_QUEUED_FMT", "En cola ({0})" },
                { "PWP_SCAN_RUNNING_FMT", "En ejecución ({0})" },
                { "PWP_SCAN_DONE_FMT", "Hecho ({0} | {1})" },
                { "PWP_SCAN_FAILED", "Falló" },
                { "PWP_SCAN_FAIL_NO_CITY", "Cargar ciudad primero" },
                { "PWP_SCAN_UNKNOWN_TIME", "hora desconocida" },

            };
        }

        public void Unload( )
        {
        }
    }
}
