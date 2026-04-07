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

                // Tabs (match Setting.cs tab ids)
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "公共交通" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "关于" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "交通线路（游戏内滑块范围）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "扩展交通线路最小/最大值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "增加每条线路的游戏内交通线路滑块**范围**。\n" +
                    "在所有已测试线路上，**最低可到 (1)**。\n" +
                    "**最大上限会变化**；但都比原版高 3× 或更多。\n" +
                    "技术说明：游戏使用线路时间（行驶时间 + 站点数量）；这会形成可变的最大值（本模组遵循游戏逻辑，因此不会设置像 200 这样的固定上限）。\n" +
                    "适用于所有公共交通：公交、渡轮、电车、火车、地铁、客船、飞机。\n\n" +
                    "**---------------**\n" +
                    "提示：如果想把滑块上限再稍微提高一些，可以给线路增加几个站点。\n" +
                    "游戏会根据新增站点 + 各种因素自动提高最大值；增加站点是玩家很容易做到的调整。\n" +
                    "<避免冲突>：移除修改同一交通线路策略的模组。\n" +
                    "如果不需要此功能，或需要关闭它以使用其他实现相同功能的模组，请禁用。"
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "车库容量（每个车库最大车辆数）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "公交车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "修改每个**公交车库**可维护/生成的公交数量。\n" +
                    "**100%** = 原版（游戏默认值）。\n" +
                    "**1000%** = 10× 更多。\n" +
                    "适用于基础建筑。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "渡轮车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**渡轮车库**每栋建筑的最大车辆数。\n" +
                    "**100%** = 原版（游戏默认值）。\n" +
                    "适用于基础建筑。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "地铁车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "修改每个**地铁车库**可维护的地铁车辆数量。\n" +
                    "适用于基础建筑。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "出租车车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "每个**出租车车库**可维护的出租车数量。\n" +
                    "如果设到最大，可能会出现数量过多、甚至有点搞笑的出租车。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "电车车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "修改每个**电车车库**可维护的电车数量。\n" +
                    "适用于基础建筑。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "火车车库" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "修改每个**火车车库**可维护的火车数量。\n" +
                    "适用于基础建筑。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "重置车库默认值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "将所有车库滑块恢复到 **100%**（游戏默认值 / 原版）。" },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "载客量（每辆车最大人数）" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "公交" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "修改**公交乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "电车" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "修改**电车乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "火车" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "修改**火车乘客**容量。\n" +
                    "适用于车头和车厢段。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "地铁" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "修改**地铁乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "船" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "修改**客船**容量（不包括货船）。\n" +
                    "**100%** = 原版座位数（游戏默认值）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "渡轮" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "修改**渡轮乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "飞机" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "修改**飞机乘客**容量。\n" +
                    "**10%** = 原版座位数的 10%。\n" +
                    "**100%** = 原版座位数（游戏默认值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "双倍" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "将所有乘客滑块设为 **200%**。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "重置所有乘客设置" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "将所有乘客滑块恢复到 **100%**\n" +
                    "（游戏默认值 / 原版）。" },

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
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "打开作者模组的 Paradox Mods 网站。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "在浏览器中打开社区 Discord。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "详细调试日志" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "将额外细节发送到 <AdjustTransit.log> 以便排查问题。\n" +
                    "正常游玩请**禁用**。\n" +
                    "<这只会增加日志记录，不会改变游戏数值。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "打开日志文件夹" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "打开日志文件夹。\n" +
                    "下一步：用文本编辑器打开 <AdjustTransit.log>（推荐 Notepad++）。" },

            };
        }

        public void Unload( )
        {
        }
    }
}
