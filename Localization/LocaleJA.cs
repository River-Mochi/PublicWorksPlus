// File: Localization/LocaleJA.cs
// Japanese (ja-JP) strings for Options UI.

namespace PublicWorksPlus
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
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "産業" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "公園・道路" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "情報" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "交通路線（ゲーム内スライダー範囲）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "交通路線の最小/最大を拡張" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "各路線ごとのゲーム内交通路線スライダーの**範囲**を広げます。\n" +
                    "テストしたすべての路線で**最小 (1)** まで下げられます。\n" +
                    "**最大上限は可変**ですが、すべてバニラより3x以上高くなります。例: 30-60\n" +
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

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "配送車両（貨物容量）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "セミトラック" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "**セミトラック**容量です。\n" +
                    "対象:\n" +
                    "* 特化産業のセミトラック（農業、漁業、林業など）。\n" +
                    "* 貨物駅との間で郵便を運ぶセミトラック（地域の郵便配達とは別）。\n" +
                    "**1× = 25t**（バニラ）\n" +
                    "**10×** = 10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "配送バン" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**配送バン**\n" +
                    "**1× = 4t**（バニラ）\n" +
                    "**10×** = 10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilTruckCargoScalar)), "原材料トラック" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilTruckCargoScalar)),
                    "**原材料トラック**（石油、石炭、鉱石、石材）\n" +
                    "**1× = 20t**（バニラ）\n" +
                    "**10×** = 10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "配送バイク" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**バイク配送**は通常、薬品を病院/診療所へ運びます。\n" +
                    "**1× = 0.1t**（バニラ）\n" +
                    "**10×** = 10倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "配送設定をリセット" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "配送倍率を**1×**（ゲーム既定値 / バニラ）に戻します。" },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "貨物フリート（港、鉄道、空港）" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "貨物駅最大フリート" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "**貨物輸送駅**のアクティブ輸送車両最大数に対する倍率です。\n" +
                    "**1×** = バニラ、**5×** = 5倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "採取施設フリート" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "産業用**採取施設の最大トラック数**に対する倍率です\n" +
                    "（農業、漁業、林業、鉱石、石油、石炭、石材）。\n" +
                    "**1×** = バニラ、**5×** = 5倍。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "貨物 + 採取施設フリートをリセット" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "貨物駅 + 採取施設の倍率を**1×**（ゲーム既定値 / バニラ）に戻します。" },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "公園メンテナンス" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "作業シフト容量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "**作業シフト容量**（車両容量）への倍率です。\n" +
                    "トラックが建物へ戻るまでにこなせる総作業量です。\n" +
                    "イメージ: 補給が多い = より長く外で作業できる。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "車両作業率" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "**車両作業率**への倍率です。\n" +
                    "作業率 = 停車中にシミュレーションtickごとにこなす作業量。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "車庫フリートサイズ" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "車庫建物の**最大車両数**への倍率です。\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "公園メンテナンスをリセット" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "すべての値を**100%**（ゲーム既定値 / バニラ）に戻します。" },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "道路メンテナンス" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "車庫フリートサイズ" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "建物ごとの**車庫最大車両数**への倍率です。\n" +
                    "高いほど = トラックが増える。\n" +
                    "<バランス注記: 少なすぎても多すぎても交通に悪影響があります。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "作業シフト容量" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "**作業シフト容量**への倍率です。\n" +
                    "トラックが車庫へ戻るまでにこなせる総作業量です。\n" +
                    "**高いほど = 戻る回数が減る。**" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "修理率" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "作業率 = 停車中にシミュレーションtickごとにこなす作業量。\n" +
                    "最高レートでもトラックは短い停止+発進を行います（1回の停止でこなす作業量が増えます）。\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "道路摩耗" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<新しいAlpha機能>\n" +
                    "**時間と交通量**の要因によって道路がどれだけ速く劣化するかを制御します。\n" +
                    "**10%** = 摩耗が10×遅い（修理回数減少）\n" +
                    "**100%** = バニラ\n" +
                    "**500%** = ダメージが5×速い（より多くの修理/トラックが必要）\n" +
                    "m_Wear <= 2.5 の場合、減速なし。\n" +
                    "m_Wear >= 17.5 の場合、最大ペナルティで車両は道路上で50%遅くなります。\n" +
                    "道路インフォビュー参照: ひどく損傷した道路は赤く表示され、車両を減速させます。"

                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "道路メンテナンスをリセット" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "すべての値を**100%**（ゲーム既定値 / バニラ）に戻します。" },

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

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "スキャンレポート（prefab）" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "デバッグ用の<一回限り>レポートを作成します。\n" +
                    "通常プレイには不要です。\n" +
                    "ファイル場所: <ModsData/PublicWorksPlus/ScanReport-Prefabs.txt>\n" +
                    "ヒント: <一度>クリックし、状態が完了になったら <レポートフォルダーを開く> を使ってください。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "Prefabスキャン状態" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "スキャン状態を表示します: Idle / Queued / Running / Done / No Data.\n" +
                    "Queued/Running は経過時間を表示し、Done は所要時間 + 完了時刻を表示します。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "詳細デバッグログ" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "トラブルシュート用に <PublicWorksPlus.log> へ追加詳細を書き込みます。\n" +
                    "通常プレイでは**無効化**してください。\n" +
                    "<これはログ量を増やすだけで、ゲームプレイ値は変更しません。>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "ログフォルダーを開く" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "ログフォルダーを開きます。\n" +
                    "次に: テキストエディタで <PublicWorksPlus.log> を開きます（Notepad++ 推奨）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "レポートフォルダーを開く" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "レポートフォルダーを開きます。\n" +
                    "次に: テキストエディタで <ScanReport-Prefabs.txt> を開きます（例: Notepad++）。" },

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
