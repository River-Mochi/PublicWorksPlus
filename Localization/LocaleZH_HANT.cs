// File: Localization/LocaleZH_HANT.cs
// Traditional Chinese (zh-HANT) strings for Options UI.

namespace PublicWorksPlus
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocaleZH_HANT : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleZH_HANT(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "大眾運輸" },
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "工業" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "公園-道路" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "關於" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "交通路線（遊戲內滑桿範圍）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "擴充交通路線最小/最大值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "增加每條路線的遊戲內交通路線滑桿**範圍**。\n" +
                    "在所有已測試路線上，**最低可到 (1)**。\n" +
                    "**最大上限會變動**；但都比原版高 3× 或更多。\n" +
                    "技術說明：遊戲使用路線時間（行駛時間 + 站點數量）；這會形成可變的最大值（本模組遵循遊戲邏輯，因此不會設定像 200 這樣的固定上限）。\n" +
                    "適用於所有大眾運輸：公車、渡輪、電車、火車、地鐵、客船、飛機。\n\n" +
                    "**---------------**\n" +
                    "提示：如果想把滑桿上限再稍微提高一些，可以替路線增加幾個站點。\n" +
                    "遊戲會依照新增站點 + 各種因素自動提高最大值；增加站點是玩家很容易做到的調整。\n" +
                    "<避免衝突>：移除修改同一交通路線政策的模組。\n" +
                    "如果不需要此功能，或需要關閉它以使用其他實作相同功能的模組，請停用。"
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "車庫容量（每個車庫最大車輛數）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "公車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "修改每個**公車車庫**可維護/生成的公車數量。\n" +
                    "**100%** = 原版（遊戲預設值）。\n" +
                    "**1000%** = 10× 更多。\n" +
                    "適用於基礎建築。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "渡輪車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**渡輪車庫**每棟建築的最大車輛數。\n" +
                    "**100%** = 原版（遊戲預設值）。\n" +
                    "適用於基礎建築。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "地鐵車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "修改每個**地鐵車庫**可維護的地鐵車輛數量。\n" +
                    "適用於基礎建築。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "計程車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "每個**計程車車庫**可維護的計程車數量。\n" +
                    "若設到最大，可能會出現數量過多、甚至有點滑稽的計程車。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "電車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "修改每個**電車車庫**可維護的電車數量。\n" +
                    "適用於基礎建築。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "火車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "修改每個**火車車庫**可維護的火車數量。\n" +
                    "適用於基礎建築。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "重設車庫預設值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "將所有車庫滑桿恢復到 **100%**（遊戲預設值 / 原版）。" },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "載客量（每輛車最大人數）" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "公車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "修改**公車乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "電車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "修改**電車乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "火車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "修改**火車乘客**容量。\n" +
                    "適用於車頭與車廂段。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "地鐵" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "修改**地鐵乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "船" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "修改**客船**容量（不包含貨船）。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "渡輪" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "修改**渡輪乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "飛機" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "修改**飛機乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 10× 更多座位。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "雙倍" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "將所有乘客滑桿設為 **200%**。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "重設所有乘客設定" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "將所有乘客滑桿恢復到 **100%**\n" +
                    "（遊戲預設值 / 原版）。" },

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "配送車輛（貨物容量）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "半掛卡車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "**半掛卡車**容量。\n" +
                    "包括：\n" +
                    "* 專業工業半掛（農場、漁業、林業等）。\n" +
                    "* 往返貨運站運送郵件的半掛卡車（不同於本地郵件投遞）。\n" +
                    "**1× = 25t**（原版）\n" +
                    "**10×** = 10× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "配送廂型車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**配送廂型車**\n" +
                    "**1× = 4t**（原版）\n" +
                    "**10×** = 10× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CoalTruckScalar)), "原材料卡車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CoalTruckScalar)),
                    "**原材料卡車**（石油、煤炭、礦石、石材，以及用於工業廢棄物的傾卸卡車 - 屬於同一種共用卡車類型）\n" +
                    "**1× = 20t**（原版）\n" +
                    "**10×** = 10× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "配送機車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**機車配送**通常會把藥品送到醫院/診所。\n" +
                    "**1× = 0.1t**（原版）\n" +
                    "**10×** = 10× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "重設配送預設值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "將配送倍率恢復到 **1×**（遊戲預設值 / 原版）。" },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "貨運車隊（港口、鐵路、機場）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "貨運站最大車隊" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "**貨運運輸站**最大活躍運輸車輛數的倍率。\n" +
                    "**1×** = 原版，**5×** = 5× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "採集設施車隊" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "工業**採集設施最大卡車數**的倍率\n" +
                    "（農場、漁業、林業、礦石、石油、煤炭、石材）。\n" +
                    "**1×** = 原版，**5×** = 5× 更多。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "重設貨運 + 採集設施車隊" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "將貨運站 + 採集設施倍率恢復到 **1×**（遊戲預設值 / 原版）。" },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "公園維護" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "工作班次容量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "**工作班次容量**（車輛容量）的倍率。\n" +
                    "卡車在返回建築前可完成的總工作量。\n" +
                    "可以理解為：補給更多 = 在外工作更久。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "車輛工作速率" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "**車輛工作速率**的倍率。\n" +
                    "速率 = 車輛停下時每個模擬 tick 完成的工作量。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "車庫車隊規模" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "車庫建築**最大車輛數**的倍率。\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "重設公園維護" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "將所有數值重設回 **100%**（遊戲預設值 / 原版）。" },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "道路維護" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "車庫車隊規模" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "每棟建築**車庫最大車輛數**的倍率。\n" +
                    "越高 = 卡車越多。\n" +
                    "<平衡說明：太少或太多都可能傷害交通。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "工作班次容量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "**工作班次容量**的倍率。\n" +
                    "卡車在返回車庫前可完成的總工作量。\n" +
                    "**越高 = 返回主建築次數越少。** 效率更高。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "修理速率" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "速率 = 車輛停下時每個模擬 tick 完成的工作量。\n" +
                    "即使在最高速率下，卡車仍會短暫停車再前進；只是每次停車完成更多工作。\n" +
                    "原版中，一次停車不一定能把道路修到 100%，所以這個功能會隨時間推移變得更有幫助。\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "道路磨損" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<新的 Alpha 功能>\n" +
                    "控制道路因**時間與交通**因素而劣化的速度。\n" +
                    "**10%** = 磨損速度慢 10×（所需維修更少）\n" +
                    "**100%** = 原版\n" +
                    "**500%** = 損壞速度快 5×（需要更多維修/卡車）\n" +
                    "遊戲內運作方式：\n" +
                    "如果 m_Wear <= 2.5，則無減速。\n" +
                    "如果 m_Wear >= 17.5，則達到最大懲罰，車輛在道路上速度會降低 50%。\n" +
                    "查看道路資訊檢視：嚴重損壞的道路會顯示為紅色，並減慢車輛速度。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "重設道路維護" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "將所有數值恢復到 **100%**（遊戲預設值 / 原版）。" },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "資訊" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "支援連結" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "除錯 / 日誌" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "模組" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "此模組的顯示名稱。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "目前模組版本。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "開啟作者模組的 Paradox Mods 網站。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "在瀏覽器中開啟社群 Discord。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "掃描報告（prefab）" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "建立用於除錯的<一次性>報告。\n" +
                    "正常遊玩不需要。\n" +
                    "檔案位置：<ModsData/PublicWorksPlus/ScanReport-Prefabs.txt>\n" +
                    "提示：點擊<一次>；當狀態顯示為完成時，使用 <開啟報告資料夾>。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "Prefab 掃描狀態" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "顯示掃描狀態：閒置 / 排隊中 / 執行中 / 完成 / 無資料。\n" +
                    "排隊中/執行中 會顯示已用時間；完成 會顯示耗時 + 完成時間。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "詳細除錯日誌" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "將額外細節寫入 <PublicWorksPlus.log> 以便排查問題。\n" +
                    "正常遊玩請**停用**。\n" +
                    "<這只會增加日誌記錄，不會改變遊戲數值。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "開啟日誌資料夾" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "開啟日誌資料夾。\n" +
                    "下一步：用文字編輯器開啟 <PublicWorksPlus.log>（推薦 Notepad++）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "開啟報告資料夾" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "開啟報告資料夾。\n" +
                    "下一步：用文字編輯器開啟 <ScanReport-Prefabs.txt>（例如 Notepad++）。" },

                // ---- Scan Report Status Text (format string templates) ----
                { "PWP_SCAN_IDLE", "閒置" },
                { "PWP_SCAN_QUEUED_FMT", "排隊中 ({0})" },
                { "PWP_SCAN_RUNNING_FMT", "執行中 ({0})" },
                { "PWP_SCAN_DONE_FMT", "完成 ({0} | {1})" },
                { "PWP_SCAN_FAILED", "失敗" },
                { "PWP_SCAN_FAIL_NO_CITY", "請先載入城市" },
                { "PWP_SCAN_UNKNOWN_TIME", "未知時間" },

            };
        }

        public void Unload( )
        {
        }
    }
}
