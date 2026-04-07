// File: Localization/LocaleES.cs
// Spanish (es-ES) strings for Options UI.

namespace AdjustTransit
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
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "Acerca de" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Líneas de transporte (rango del deslizador en juego)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Ampliar mín/máx de líneas de transporte" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Aumenta el **rango** del deslizador de líneas de transporte en juego para cada ruta.\n" +
                    "**Tan bajo como (1)** en todas las rutas probadas.\n" +
                    "El **límite máximo varía**; pero todos son 3× o más altos que vanilla.\n" +
                    "Nota técnica: el juego usa el tiempo de ruta (tiempo de conducción + número de paradas); esto crea un máximo variable (este mod sigue la lógica del juego y por eso no fija un máximo estático como 200).\n" +
                    "Funciona para todo el transporte: autobús, ferry, tranvía, tren, metro, barco, avión.\n\n" +
                    "**---------------**\n" +
                    "Consejo: si quieres aumentar un poco más el máximo del deslizador, añade algunas paradas a la ruta.\n" +
                    "El juego aumenta automáticamente el máximo según las paradas añadidas + factores; añadir paradas es un ajuste sencillo para el jugador.\n" +
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

              
                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Enlaces de soporte" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Depuración / Registro" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nombre mostrado de este mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versión" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Versión actual del mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Abre el sitio web de Paradox Mods para los mods del autor." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Abre el Discord de la comunidad en un navegador." },

           

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Registros debug detallados" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Envía detalles adicionales a <AdjustTransit.log> para solucionar problemas.\n" +
                    "**Desactivar** para una partida normal.\n" +
                    "<Esto solo aumenta el registro y no cambia los valores de juego.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Abrir carpeta de logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Abre la carpeta de logs.\n" +
                    "Siguiente: abrir <AdjustTransit.log> con el editor de texto (se recomienda Notepad++)." },
            };
        }

        public void Unload( )
        {
        }
    }
}
