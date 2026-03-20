// File: Localization/LocaleZH_CN.cs
// Simplified Chinese (zh-HANS) strings for Options UI.

namespace DispatchBoss
{
    using Colossal;
    using Colossal.IO.AssetDatabase.Internal;
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
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "工业" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "公园-道路" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "关于" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "交通线路（游戏内滑块范围）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "扩展交通线路最小/最大值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "增加每条线路的游戏内交通线路滑块**范围**。\n" +
                    "在所有已测试线路上，**最低可到 (1)**。\n" +
                    "**最大上限会变化**；但都比原版高 3x 或更多，例如 30-60\n" +
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

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "配送车辆（货物容量）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "半挂卡车" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "**半挂卡车**容量。\n" +
                    "包括：\n" +
                    "* 专业工业半挂（农场、渔业、林业等）。\n" +
                    "* 往返货运站运输邮件的半挂卡车（不同于本地邮件投递）。\n" +
                    "**1× = 25t**（原版）\n" +
                    "**10×** = 10× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "配送面包车" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**配送面包车**\n" +
                    "**1× = 4t**（原版）\n" +
                    "**10×** = 10× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilTruckCargoScalar)), "原材料卡车" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilTruckCargoScalar)),
                    "**原材料卡车**（石油、煤炭、矿石、石材）\n" +
                    "**1× = 20t**（原版）\n" +
                    "**10×** = 10× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "配送摩托车" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**摩托车配送**通常会把药品送到医院/诊所。\n" +
                    "**1× = 0.1t**（原版）\n" +
                    "**10×** = 10× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "重置配送默认值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "将配送倍率恢复到 **1×**（游戏默认值 / 原版）。" },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "货运车队（港口、铁路、机场）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "货运站最大车队" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "**货运运输站**最大活跃运输车辆数的倍率。\n" +
                    "**1×** = 原版，**5×** = 5× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "采集设施车队" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "工业**采集设施最大卡车数**的倍率\n" +
                    "（农场、渔业、林业、矿石、石油、煤炭、石材）。\n" +
                    "**1×** = 原版，**5×** = 5× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "重置货运 + 采集设施车队" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "将货运站 + 采集设施倍率恢复到 **1×**（游戏默认值 / 原版）。" },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "公园维护" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "工作班次容量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "**工作班次容量**（车辆容量）的倍率。\n" +
                    "卡车在返回建筑前可完成的总工作量。\n" +
                    "可以理解为：补给更多 = 在外工作更久。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "车辆工作速率" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "**车辆工作速率**的倍率。\n" +
                    "速率 = 车辆停下时每个模拟 tick 完成的工作量。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "车库车队规模" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "车库建筑**最大车辆数**的倍率。\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "重置公园维护" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "将所有数值重置回 **100%**（游戏默认值 / 原版）。" },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "道路维护" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "车库车队规模" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "每栋建筑**车库最大车辆数**的倍率。\n" +
                    "越高 = 卡车越多。\n" +
                    "<平衡说明：太少或太多都可能损害交通。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "工作班次容量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "**工作班次容量**的倍率。\n" +
                    "卡车在返回车库前可完成的总工作量。\n" +
                    "**越高 = 返回次数越少。**" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "修理速率" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "速率 = 车辆停下时每个模拟 tick 完成的工作量。\n" +
                    "即使在最高速率下，卡车仍会短暂停车+再前进（只是每次停车完成更多工作）。\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "道路磨损" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<新的 Alpha 功能>\n" +
                    "控制道路因**时间和交通**因素而劣化的速度。\n" +
                    "**10%** = 磨损速度慢 10×（所需维修更少）\n" +
                    "**100%** = 原版\n" +
                    "**500%** = 损坏速度快 5×（需要更多维修/卡车）\n" +
                    "如果 m_Wear <= 2.5，则无减速。\n" +
                    "如果 m_Wear >= 17.5，则达到最大惩罚，车辆在道路上速度会降低 50%。\n" +
                    "查看道路信息视图：严重损坏的道路会显示为红色，并减慢车辆速度。"

                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "重置道路维护" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "将所有数值恢复到 **100%**（游戏默认值 / 原版）。" },

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

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "扫描报告（prefab）" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "创建用于调试的<一次性>报告。\n" +
                    "正常游玩不需要。\n" +
                    "文件位置：<ModsData/DispatchBoss/ScanReport-Prefabs.txt>\n" +
                    "提示：点击<一次>；当状态显示为完成时，使用 <打开报告文件夹>。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "Prefab 扫描状态" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "显示扫描状态：Idle / Queued / Running / Done / No Data.\n" +
                    "Queued/Running 显示已用时间；Done 显示耗时 + 完成时间。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "详细调试日志" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "将额外细节发送到 <DispatchBoss.log> 以便排查问题。\n" +
                    "正常游玩请**禁用**。\n" +
                    "<这只会增加日志记录，不会改变游戏数值。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "打开日志文件夹" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "打开日志文件夹。\n" +
                    "下一步：用文本编辑器打开 <DispatchBoss.log>（推荐 Notepad++）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "打开报告文件夹" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "打开报告文件夹。\n" +
                    "下一步：用文本编辑器打开 <ScanReport-Prefabs.txt>（例如 Notepad++）。" },

                // ---- Scan Report Status Text (format string templates) ----
                { "DB_SCAN_IDLE", "空闲" },
                { "DB_SCAN_QUEUED_FMT", "排队中 ({0})" },
                { "DB_SCAN_RUNNING_FMT", "运行中 ({0})" },
                { "DB_SCAN_DONE_FMT", "完成 ({0} | {1})" },
                { "DB_SCAN_FAILED", "失败" },
                { "DB_SCAN_FAIL_NO_CITY", "请先加载城市" },
                { "DB_SCAN_UNKNOWN_TIME", "未知时间" },

            };
        }

        public void Unload( )
        {
        }
    }
}
