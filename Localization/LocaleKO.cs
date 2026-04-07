// File: Localization/LocaleKO.cs
// Korean (ko-KR) strings for Options UI.

namespace AdjustTransit
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocaleKO : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleKO(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "대중교통" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "정보" },

                // --------------------
                // Public Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "교통 노선 (게임 내 슬라이더 범위)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "확장 교통 노선" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "각 노선별 게임 내 교통 노선 슬라이더의 **범위**를 확장합니다.\n" +
                    "테스트한 모든 노선에서 **(1)** 까지 낮출 수 있습니다.\n" +
                    "**최대 한도는 가변적**이며, 테스트한 모든 노선에서 바닐라보다 최소 3배 이상 높아졌습니다.\n" +
                    "기술 메모: 게임은 노선 시간(주행 시간 + 정류장 수)을 사용하므로 최대값은 고정이 아니라 가변입니다.\n" +
                    "버스, 페리, 트램, 열차, 지하철, 여객선, 비행기에 적용됩니다.\n\n" +
                    "<충돌 경고>: Public Works Plus 또는 같은 교통 노선 정책을 수정하는 다른 모드가 있다면, 이 기능은 한 모드에서만 켜 두세요."
                },

                // Depot capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "차고 용량 (차고당 최대 차량 수)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "버스 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "각 **버스 차고**가 유지하거나 출고할 수 있는 버스 수를 변경합니다.\n" +
                    "**100%** = 바닐라 (게임 기본값).\n" +
                    "**1000%** = 10배 더 많음.\n" +
                    "기본 건물에 적용됩니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "페리 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "각 **페리 차고**가 유지하거나 출고할 수 있는 페리 수를 변경합니다.\n" +
                    "**100%** = 바닐라 (게임 기본값).\n" +
                    "**1000%** = 10배 더 많음.\n" +
                    "기본 건물에 적용됩니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "지하철 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "각 **지하철 차고**가 유지할 수 있는 지하철 차량 수를 변경합니다.\n" +
                    "**100%** = 바닐라 (게임 기본값).\n" +
                    "**1000%** = 10배 더 많음.\n" +
                    "기본 건물에 적용됩니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "택시 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "각 **택시 차고**가 유지할 수 있는 택시 수를 변경합니다.\n" +
                    "**100%** = 바닐라 (게임 기본값).\n" +
                    "**1000%** = 10배 더 많음.\n" +
                    "값이 너무 높으면 택시 교통이 과도해질 수 있습니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "트램 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "각 **트램 차고**가 유지할 수 있는 트램 수를 변경합니다.\n" +
                    "**100%** = 바닐라 (게임 기본값).\n" +
                    "**1000%** = 10배 더 많음.\n" +
                    "기본 건물에 적용됩니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "열차 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "각 **열차 차고**가 유지할 수 있는 열차 수를 변경합니다.\n" +
                    "**100%** = 바닐라 (게임 기본값).\n" +
                    "**1000%** = 10배 더 많음.\n" +
                    "기본 건물에 적용됩니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "차고 기본값으로 재설정" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "모든 차고 슬라이더를 **100%** 로 되돌립니다 (게임 기본값 / 바닐라)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "승객 수용량 (차량당 최대 인원)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "버스" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "**버스 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석의 10%.\n" +
                    "**100%** = 바닐라 좌석 (게임 기본값).\n" +
                    "**1000%** = 좌석 10배."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "트램" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "**트램 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석의 10%.\n" +
                    "**100%** = 바닐라 좌석 (게임 기본값).\n" +
                    "**1000%** = 좌석 10배."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "열차" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "**열차 승객** 수용량을 변경합니다.\n" +
                    "기관차와 객차 섹션에 적용됩니다.\n" +
                    "**10%** = 바닐라 좌석의 10%.\n" +
                    "**100%** = 바닐라 좌석 (게임 기본값).\n" +
                    "**1000%** = 좌석 10배."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "지하철" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "**지하철 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석의 10%.\n" +
                    "**100%** = 바닐라 좌석 (게임 기본값).\n" +
                    "**1000%** = 좌석 10배."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "여객선" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "**여객선** 수용량을 변경합니다 (화물선 제외).\n" +
                    "**10%** = 바닐라 좌석의 10%.\n" +
                    "**100%** = 바닐라 좌석 (게임 기본값).\n" +
                    "**1000%** = 좌석 10배."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "페리" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "**페리 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석의 10%.\n" +
                    "**100%** = 바닐라 좌석 (게임 기본값).\n" +
                    "**1000%** = 좌석 10배."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "비행기" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "**비행기 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석의 10%.\n" +
                    "**100%** = 바닐라 좌석 (게임 기본값).\n" +
                    "**1000%** = 좌석 10배."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "전부 2배" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "모든 승객 슬라이더를 **200%** 로 설정합니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "모든 승객 재설정" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "모든 승객 슬라이더를 **100%** 로 되돌립니다 (게임 기본값 / 바닐라)." },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "정보" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "지원 링크" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "디버그 / 로그" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "모드" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "이 모드의 표시 이름입니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "버전" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "현재 모드 버전입니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "작성자의 Paradox Mods 페이지를 엽니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "커뮤니티 Discord를 브라우저에서 엽니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "상세 디버그 로그" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "문제 해결을 위해 이 모드의 로그 파일에 추가 정보를 기록합니다.\n" +
                    "**일반 플레이에서는 비활성화**하세요.\n" +
                    "<이 옵션은 로그만 늘리고 게임플레이 값은 바꾸지 않습니다.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "로그 폴더 열기" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "이 모드의 로그 폴더를 엽니다."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
