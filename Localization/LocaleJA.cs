// File: Localization/LocaleJA.cs
// Japanese (ja-JP) strings for Options UI.

namespace AdjustTransit
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocaleJA : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleJA(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "情報" },

                // --------------------
                // Public Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "交通路線（ゲーム内スライダー範囲）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "路線スライダー拡張" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "各路線ごとのゲーム内交通路線スライダーの**範囲**を拡張します。\n" +
                    "テストしたすべての路線で**(1)** まで下げられます。\n" +
                    "**最大値は可変**で、テストしたすべての路線で少なくともバニラの3×以上に達しました。\n" +
                    "技術メモ：ゲームは路線時間（走行時間 + 停留所数）を使うため、最大値は固定ではなく可変です。\n" +
                    "バス、フェリー、路面電車、列車、地下鉄、客船、飛行機に対応します。\n\n" +
                    "<競合警告>：Public Works Plus または同じ交通路線ポリシーを変更する別のMODがある場合、この機能は1つのMODだけで有効にしてください。"
                },

                // Depot capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "車庫容量（車庫ごとの最大車両数）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "バス車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "各**バス車庫**が維持または出庫できるバスの数を変更します。\n" +
                    "**100%** = バニラ（ゲーム既定値）。\n" +
                    "**1000%** = 10倍。\n" +
                    "基本建物に適用されます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "フェリー車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "各**フェリー車庫**が維持または出庫できるフェリーの数を変更します。\n" +
                    "**100%** = バニラ（ゲーム既定値）。\n" +
                    "**1000%** = 10倍。\n" +
                    "基本建物に適用されます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "地下鉄車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "各**地下鉄車庫**が維持できる地下鉄車両の数を変更します。\n" +
                    "**100%** = バニラ（ゲーム既定値）。\n" +
                    "**1000%** = 10倍。\n" +
                    "基本建物に適用されます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "タクシー車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "各**タクシー車庫**が維持できるタクシーの数を変更します。\n" +
                    "**100%** = バニラ（ゲーム既定値）。\n" +
                    "**1000%** = 10倍。\n" +
                    "高い値ではタクシー交通が過剰になる場合があります。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "路面電車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "各**路面電車車庫**が維持できる路面電車の数を変更します。\n" +
                    "**100%** = バニラ（ゲーム既定値）。\n" +
                    "**1000%** = 10倍。\n" +
                    "基本建物に適用されます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "列車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "各**列車車庫**が維持できる列車の数を変更します。\n" +
                    "**100%** = バニラ（ゲーム既定値）。\n" +
                    "**1000%** = 10倍。\n" +
                    "基本建物に適用されます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "車庫設定をリセット" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "すべての車庫スライダーを **100%** に戻します（ゲーム既定値 / バニラ）。" },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "乗客容量（車両ごとの最大人数）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "バス" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "**バス乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "路面電車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "**路面電車乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "列車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "**列車乗客**容量を変更します。\n" +
                    "機関車と車両セクションに適用されます。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "地下鉄" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "**地下鉄乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "客船" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "**客船**の容量を変更します（貨物船は対象外）。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "フェリー" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "**フェリー乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "飛行機" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "**飛行機乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "全乗客を2倍" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "すべての乗客スライダーを **200%** に設定します。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "乗客設定をすべてリセット" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "すべての乗客スライダーを **100%** に戻します（ゲーム既定値 / バニラ）。" },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "情報" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "サポートリンク" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "デバッグ / ログ" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "MOD" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "このMODの表示名です。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "バージョン" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "現在のMODバージョンです。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "作者の Paradox Mods ページを開きます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "コミュニティ Discord をブラウザで開きます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "詳細デバッグログ" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "トラブルシューティング用に、このMODのログファイルへ追加の詳細を書き込みます。\n" +
                    "**通常プレイでは無効化**してください。\n" +
                    "<これはログ量を増やすだけで、ゲームプレイ値は変更しません。>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "ログフォルダを開く" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "このMODのログフォルダを開きます。"
                },
            };
        }

        public void Unload()
        {
        }
    }
}
