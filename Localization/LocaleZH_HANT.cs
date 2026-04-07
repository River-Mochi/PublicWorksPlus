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

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "公共交通" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "關於" },

                // --------------------
                // Public Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "交通路線（遊戲內滑桿範圍）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "擴展交通路線" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "擴展每條路線在遊戲內交通路線滑桿的**範圍**。\n" +
                    "在所有已測試路線上，最低可到 **(1)**。\n" +
                    "**最大上限會變動**；所有已測試路線都至少達到原版的 3× 以上。\n" +
                    "技術說明：遊戲使用路線時間（行駛時間 + 站點數量），因此最大值是可變的，而不是固定的。\n" +
                    "適用於公車、渡輪、電車、火車、地鐵、客船與飛機。\n\n" +
                    "<衝突警告>：如果 Public Works Plus 或其他模組修改相同的交通路線策略，請只在一個模組中啟用此功能。"
                },

                // Depot capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "車庫容量（每個車庫最大車輛數）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "公車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "更改每個**公車車庫**可維護或生成的公車數量。\n" +
                    "**100%** = 原版（遊戲預設值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "適用於基礎建築。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "渡輪車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "更改每個**渡輪車庫**可維護或生成的渡輪數量。\n" +
                    "**100%** = 原版（遊戲預設值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "適用於基礎建築。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "地鐵車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "更改每個**地鐵車庫**可維護的地鐵車輛數量。\n" +
                    "**100%** = 原版（遊戲預設值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "適用於基礎建築。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "計程車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "更改每個**計程車車庫**可維護的計程車數量。\n" +
                    "**100%** = 原版（遊戲預設值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "數值過高可能導致過多的計程車交通。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "電車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "更改每個**電車車庫**可維護的電車數量。\n" +
                    "**100%** = 原版（遊戲預設值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "適用於基礎建築。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "火車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "更改每個**火車車庫**可維護的火車數量。\n" +
                    "**100%** = 原版（遊戲預設值）。\n" +
                    "**1000%** = 10倍。\n" +
                    "適用於基礎建築。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "重設車庫預設值" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "將所有車庫滑桿重設回 **100%**（遊戲預設值 / 原版）。" },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "乘客容量（每輛車最大人數）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "公車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "更改**公車乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 座位數 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "電車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "更改**電車乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 座位數 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "火車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "更改**火車乘客**容量。\n" +
                    "適用於機車與車廂段。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 座位數 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "地鐵" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "更改**地鐵乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 座位數 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "客船" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "更改**客船**容量（不包含貨船）。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 座位數 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "渡輪" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "更改**渡輪乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 座位數 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "飛機" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "更改**飛機乘客**容量。\n" +
                    "**10%** = 原版座位數的 10%。\n" +
                    "**100%** = 原版座位數（遊戲預設值）。\n" +
                    "**1000%** = 座位數 10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "全部加倍" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "將所有乘客滑桿設為 **200%**。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "重設所有乘客" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "將所有乘客滑桿重設回 **100%**（遊戲預設值 / 原版）。" },

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
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "開啟作者的 Paradox Mods 頁面。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "在瀏覽器中開啟社群 Discord。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "詳細除錯日誌" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "將額外細節寫入此模組的日誌檔案以便排除問題。\n" +
                    "**正常遊玩時請停用**。\n" +
                    "<這只會增加日誌記錄，不會改變遊戲玩法數值。>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "開啟日誌資料夾" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "開啟此模組的日誌資料夾。"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
