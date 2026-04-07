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

                // Tabs (match Setting.cs tab ids)
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "公共交通" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "情報" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "交通路線（ゲーム内スライダー範囲）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "交通路線の最小/最大を拡張" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "各路線ごとのゲーム内交通路線スライダーの**範囲**を広げます。\n" +
                    "テストしたすべての路線で**最小 (1)** まで下げられます。\n" +
                    "**最大上限は可変**ですが、すべてバニラより3×以上高くなります。\n" +
                    "技術メモ: ゲームは路線時間（走行時間 + 停留所数）を使用するため、最大値は可変になります（このMODはゲームロジックに従うため、200のような固定上限は設定しません）。\n" +
                    "すべての交通機関で動作します: バス、フェリー、トラム、列車、地下鉄、船、飛行機。\n\n" +
                    "**---------------**\n" +
                    "ヒント: スライダー最大値をもう少し上げたい場合は、路線に停留所をいくつか追加してください。\n" +
                    "ゲームは追加された停留所 + 各種要素に応じて最大値を自動で増やします。停留所追加は簡単にできる調整です。\n" +
                    "<競合回避>: 同じ交通路線ポリシーを編集するMODは外してください。\n" +
                    "この機能が不要な場合、または同じ用途の別MODを使うために無効化が必要な場合はオフにしてください。"
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "車庫容量（車庫ごとの最大車両数）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "バス車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "各**バス車庫**が維持/出庫できるバスの数を変更します。\n" +
                    "**100%** = バニラ（ゲーム既定値）。\n" +
                    "**1000%** = 10倍。\n" +
                    "ベース建物に適用されます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "フェリー車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**フェリー車庫**の建物ごとの最大車両数です。\n" +
                    "**100%** = バニラ（ゲーム既定値）。\n" +
                    "ベース建物に適用されます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "地下鉄車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "各**地下鉄車庫**が維持できる地下鉄車両数を変更します。\n" +
                    "ベース建物に適用されます。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "タクシー車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "各**タクシー車庫**が維持できるタクシー台数です。\n" +
                    "最大にすると、タクシーが過剰でコミカルな量になる可能性があります。"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "トラム車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "各**トラム車庫**が維持できるトラム数を変更します。\n" +
                    "ベース建物に適用されます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "列車車庫" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "各**列車車庫**が維持できる列車数を変更します。\n" +
                    "ベース建物に適用されます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "車庫設定をリセット" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "すべての車庫スライダーを**100%**（ゲーム既定値 / バニラ）に戻します。" },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "乗客容量（車両ごとの最大人数）" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "バス" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "**バス乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "トラム" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "**トラム乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "列車" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "**列車乗客**容量を変更します。\n" +
                    "機関車と各セクションに適用されます。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "地下鉄" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "**地下鉄乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "船" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "**旅客船**の容量を変更します（貨物船は対象外）。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "フェリー" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "**フェリー乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "飛行機" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "**飛行機乗客**容量を変更します。\n" +
                    "**10%** = バニラ座席数の10%。\n" +
                    "**100%** = バニラ座席数（ゲーム既定値）。\n" +
                    "**1000%** = 座席数10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "2倍にする" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "すべての乗客スライダーを**200%**に設定します。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "全乗客をリセット" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "すべての乗客スライダーを**100%**に戻します\n" +
                    "（ゲーム既定値 / バニラ）。" },

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
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "作者のMODがある Paradox Mods のWebサイトを開きます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "コミュニティDiscordをブラウザで開きます。" },


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "詳細デバッグログ" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "トラブルシュート用に <AdjustTransit.log> へ追加詳細を書き込みます。\n" +
                    "通常プレイでは**無効化**してください。\n" +
                    "<これはログ量を増やすだけで、ゲームプレイ値は変更しません。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "ログフォルダーを開く" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "ログフォルダーを開きます。\n" +
                    "次に: テキストエディタで <AdjustTransit.log> を開きます（Notepad++ 推奨）。" },

                // ---- Scan Report Status Text (format string templates) ----
                { "PWP_SCAN_IDLE", "待機中" },
                { "PWP_SCAN_QUEUED_FMT", "待機列 ({0})" },
                { "PWP_SCAN_RUNNING_FMT", "実行中 ({0})" },
                { "PWP_SCAN_DONE_FMT", "完了 ({0} | {1})" },
                { "PWP_SCAN_FAILED", "失敗" },
                { "PWP_SCAN_FAIL_NO_CITY", "先に都市をロード" },
                { "PWP_SCAN_UNKNOWN_TIME", "時刻不明" },

            };
        }

        public void Unload( )
        {
        }
    }
}
