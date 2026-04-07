// File: Localization/LocaleZH_CN.cs
// Simplified Chinese (zh-HANS) strings for Options UI.

namespace AdjustTransit
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocaleZH_CN : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleZH_CN(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "公共交通" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "关于" },

                // --------------------
                // Public Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "交通线路（游戏内滑块范围）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "扩展交通线路" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "扩展每条线路在游戏内交通线路滑块的**范围**。\n" +
                    "在所有已测试线路上，最低可到 **(1)**。\n" +
                    "**最大上限会变化**；所有已测试线路都至少达到原版的 3× 以上。\n" +
                    "技术说明：游戏使用线路时间（行驶时间 + 站点数量），因此最大值是可变的，而不是固定的。\n" +
                    "适用于公交、渡轮、电车、火车、地铁、客船和飞机。\n\n" +
                    "<冲突警告>：如果 Public Works Plus 或其他模组修改同一交通线路策略，请只在一个模组中启用此功能。"
                },

                // Depot capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "车库容量（每个车库最大车辆数）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "公交车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "更改每个**公交车库**可维护或生成的公交数量。\n" +
                    "**100%** = 原版（游戏默认值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "适用于基础建筑。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "渡轮车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "更改每个**渡轮车库**可维护或生成的渡轮数量。\n" +
                    "**100%** = 原版（游戏默认值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "适用于基础建筑。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "地铁车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "更改每个**地铁车库**可维护的地铁车辆数量。\n" +
                    "**100%** = 原版（游戏默认值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "适用于基础建筑。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "出租车车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "更改每个**出租车车库**可维护的出租车数量。\n" +
                    "**100%** = 原版（游戏默认值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "数值过高可能导致出租车流量过多。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "电车车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "更改每个**电车车库**可维护的电车数量。\n" +
                    "**100%** = 原版（游戏默认值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "适用于基础建筑。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "火车车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "更改每个**火车车库**可维护的火车数量。\n" +
                    "**100%** = 原版（游戏默认值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "适用于基础建筑。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "重置车库默认值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "将所有车库滑块重置回 **100%**（游戏默认值 / 原版）。" },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "乘客容量（每辆车最大人数）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "公交" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "更改**公交乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 座位数 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "电车" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "更改**电车乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 座位数 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "火车" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "更改**火车乘客**容量。\n" +
                    "适用于机车和车厢段。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 座位数 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "地铁" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "更改**地铁乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 座位数 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "客船" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "更改**客船**容量（不包括货船）。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 座位数 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "渡轮" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "更改**渡轮乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 座位数 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "飞机" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "更改**飞机乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 座位数 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "全部翻倍" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "将所有乘客滑块设为 **200%**。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "重置所有乘客" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "将所有乘客滑块重置回 **100%**（游戏默认值 / 原版）。" },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "信息" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "支持链接" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "调试 / 日志" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "模组" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "此模组的显示名称。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "当前模组版本。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "打开作者的 Paradox Mods 页面。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "在浏览器中打开社区 Discord。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "详细调试日志" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "将额外细节发送到此模组的日志文件中以便排查问题。\n" +
                    "**正常游玩时请禁用**。\n" +
                    "<这只会增加日志记录，不会改变游戏玩法数值。>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "打开日志文件夹" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "打开此模组的日志文件夹。"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
