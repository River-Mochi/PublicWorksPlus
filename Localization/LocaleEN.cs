// File: Localization/LocaleEN.cs
// English (en-US) strings for Options UI.

namespace AdjustTransit
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleEN(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "Public Transit" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "About" },

                // --------------------
                // Public Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Transit lines (in-game slider range)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Extended transit lines" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Expands the **range** of the in-game Transit Line Slider per route.\n" +
                    "**As low as (1)** on all routes tested.\n" +
                    "**Maximum limit varies**; all tested routes reached at least 3× higher than vanilla.\n" +
                    "Tech note: game uses route time (driving time + stop count), so the maximum is variable rather than static.\n" +
                    "Works for bus, ferry, tram, train, subway, ship, and airplane.\n\n" +
                    "<Conflict warning>: if Public Works Plus or another mod edits the same Transit Line policy, keep this feature enabled in only one mod."
                },

                // Depot capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Depot capacity (max vehicles per depot)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Bus depot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Change how many buses each **Bus Depot** can maintain or spawn.\n" +
                    "**100%** = vanilla (game default).\n" +
                    "**1000%** = 10× more.\n" +
                    "Applies to the base building."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Ferry depot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "Change how many ferries each **Ferry Depot** can maintain or spawn.\n" +
                    "**100%** = vanilla (game default).\n" +
                    "**1000%** = 10× more.\n" +
                    "Applies to the base building."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "Subway depot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Change how many subway vehicles each **Subway Depot** can maintain.\n" +
                    "**100%** = vanilla (game default).\n" +
                    "**1000%** = 10× more.\n" +
                    "Applies to the base building."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Taxi depot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Change how many taxis each **Taxi Depot** can maintain.\n" +
                    "**100%** = vanilla (game default).\n" +
                    "**1000%** = 10× more.\n" +
                    "High values can create excessive taxi traffic."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Tram depot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Change how many trams each **Tram Depot** can maintain.\n" +
                    "**100%** = vanilla (game default).\n" +
                    "**1000%** = 10× more.\n" +
                    "Applies to the base building."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Train depot" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Change how many trains each **Train Depot** can maintain.\n" +
                    "**100%** = vanilla (game default).\n" +
                    "**1000%** = 10× more.\n" +
                    "Applies to the base building."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Reset depot defaults" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Set all depot sliders back to **100%** (game default / vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Passenger capacity (max people per vehicle)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Bus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Change **bus passenger** capacity.\n" +
                    "**10%** = 10% of vanilla seats.\n" +
                    "**100%** = vanilla seats (game default).\n" +
                    "**1000%** = 10× more seats."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Tram" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Change **tram passenger** capacity.\n" +
                    "**10%** = 10% of vanilla seats.\n" +
                    "**100%** = vanilla seats (game default).\n" +
                    "**1000%** = 10× more seats."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Train" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Change **train passenger** capacity.\n" +
                    "Applies to engines and sections.\n" +
                    "**10%** = 10% of vanilla seats.\n" +
                    "**100%** = vanilla seats (game default).\n" +
                    "**1000%** = 10× more seats."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "Subway" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Change **subway passenger** capacity.\n" +
                    "**10%** = 10% of vanilla seats.\n" +
                    "**100%** = vanilla seats (game default).\n" +
                    "**1000%** = 10× more seats."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Ship" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Change **passenger ship** capacity (not cargo ships).\n" +
                    "**10%** = 10% of vanilla seats.\n" +
                    "**100%** = vanilla seats (game default).\n" +
                    "**1000%** = 10× more seats."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Ferry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Change **ferry passenger** capacity.\n" +
                    "**10%** = 10% of vanilla seats.\n" +
                    "**100%** = vanilla seats (game default).\n" +
                    "**1000%** = 10× more seats."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Airplane" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Change **airplane passenger** capacity.\n" +
                    "**10%** = 10% of vanilla seats.\n" +
                    "**100%** = vanilla seats (game default).\n" +
                    "**1000%** = 10× more seats."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Double all passengers" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Set every passenger slider to **200%**."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Reset all passengers" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Set all passenger sliders back to **100%** (game default / vanilla)."
                },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Support links" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Debug / Logging" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Display name of this mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Current mod version." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Open the author's Paradox Mods page." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Open the community Discord in a browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Verbose debug logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Sends extra details to this mod's log file for troubleshooting.\n" +
                    "**Disable** for normal gameplay.\n" +
                    "<This only increases logging and does not change gameplay values.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Open log folder" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Open the logs folder for this mod."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
