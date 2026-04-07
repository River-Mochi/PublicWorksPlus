// File: Localization/LocaleZH_HANT.cs
// Traditional Chinese (zh-HANT) strings for Options UI.

namespace AdjustTransit
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


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "詳細除錯日誌" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "將額外細節寫入 <AdjustTransit.log> 以便排查問題。\n" +
                    "正常遊玩請**停用**。\n" +
                    "<這只會增加日誌記錄，不會改變遊戲數值。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "開啟日誌資料夾" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "開啟日誌資料夾。\n" +
                    "下一步：用文字編輯器開啟 <AdjustTransit.log>（推薦 Notepad++）。" },

            };
        }

        public void Unload( )
        {
        }
    }
}
