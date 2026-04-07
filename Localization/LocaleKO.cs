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

                // Tabs (match Setting.cs tab ids)
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "대중교통" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "정보" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "교통 노선 (게임 내 슬라이더 범위)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "교통 노선 최소/최대 확장" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "각 노선별 게임 내 교통 노선 슬라이더의 **범위**를 늘립니다.\n" +
                    "테스트된 모든 노선에서 **최저 (1)** 까지 내려갑니다.\n" +
                    "**최대 한도는 가변적**이지만, 모두 바닐라보다 3× 이상 높습니다.\n" +
                    "기술 참고: 게임은 노선 시간(주행 시간 + 정류장 수)을 사용하므로 최대값이 가변적입니다(이 모드는 게임 로직을 따르므로 200 같은 고정 최대값은 설정하지 않습니다).\n" +
                    "모든 교통수단에 적용됩니다: 버스, 페리, 트램, 기차, 지하철, 선박, 비행기.\n\n" +
                    "**---------------**\n" +
                    "팁: 슬라이더의 최대 끝값을 조금 더 올리고 싶다면 노선에 정류장을 몇 개 추가하세요.\n" +
                    "게임은 추가된 정류장 + 여러 요소를 기준으로 최대값을 자동 증가시킵니다. 정류장 추가는 간단한 플레이어 조정입니다.\n" +
                    "<충돌 방지>: 같은 교통 노선 정책을 수정하는 모드는 제거하세요.\n" +
                    "이 기능이 필요 없거나 같은 기능의 다른 모드를 쓰기 위해 꺼야 한다면 비활성화하세요."
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "차고 용량 (차고당 최대 차량 수)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "버스 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "각 **버스 차고**가 유지/출고할 수 있는 버스 수를 변경합니다.\n" +
                    "**100%** = 바닐라 (게임 기본값).\n" +
                    "**1000%** = 10배.\n" +
                    "기본 건물에 적용됩니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "페리 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**페리 차고** 건물당 최대 차량 수입니다.\n" +
                    "**100%** = 바닐라 (게임 기본값).\n" +
                    "기본 건물에 적용됩니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "지하철 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "각 **지하철 차고**가 유지할 수 있는 지하철 차량 수를 변경합니다.\n" +
                    "기본 건물에 적용됩니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "택시 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "각 **택시 차고**가 유지할 수 있는 택시 수입니다.\n" +
                    "최대로 설정하면 택시가 과하게 많아져 우스꽝스러울 수 있습니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "트램 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "각 **트램 차고**가 유지할 수 있는 트램 수를 변경합니다.\n" +
                    "기본 건물에 적용됩니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "기차 차고" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "각 **기차 차고**가 유지할 수 있는 기차 수를 변경합니다.\n" +
                    "기본 건물에 적용됩니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "차고 기본값 리셋" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "모든 차고 슬라이더를 **100%** (게임 기본값 / 바닐라)로 되돌립니다." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "승객 수용량 (차량당 최대 인원)" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "버스" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "**버스 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석 수의 10%.\n" +
                    "**100%** = 바닐라 좌석 수 (게임 기본값).\n" +
                    "**1000%** = 좌석 수 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "트램" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "**트램 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석 수의 10%.\n" +
                    "**100%** = 바닐라 좌석 수 (게임 기본값).\n" +
                    "**1000%** = 좌석 수 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "기차" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "**기차 승객** 수용량을 변경합니다.\n" +
                    "엔진과 객차 구간에 적용됩니다.\n" +
                    "**10%** = 바닐라 좌석 수의 10%.\n" +
                    "**100%** = 바닐라 좌석 수 (게임 기본값).\n" +
                    "**1000%** = 좌석 수 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "지하철" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "**지하철 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석 수의 10%.\n" +
                    "**100%** = 바닐라 좌석 수 (게임 기본값).\n" +
                    "**1000%** = 좌석 수 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "선박" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "**여객선** 수용량을 변경합니다 (화물선 제외).\n" +
                    "**100%** = 바닐라 좌석 수 (게임 기본값)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "페리" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "**페리 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석 수의 10%.\n" +
                    "**100%** = 바닐라 좌석 수 (게임 기본값).\n" +
                    "**1000%** = 좌석 수 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "비행기" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "**비행기 승객** 수용량을 변경합니다.\n" +
                    "**10%** = 바닐라 좌석 수의 10%.\n" +
                    "**100%** = 바닐라 좌석 수 (게임 기본값).\n" +
                    "**1000%** = 좌석 수 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "두 배" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "모든 승객 슬라이더를 **200%**로 설정합니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "모든 승객 리셋" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "모든 승객 슬라이더를 **100%**로 되돌립니다\n" +
                    "(게임 기본값 / 바닐라)." },


                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "정보" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "지원 링크" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "디버그 / 로깅" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "모드" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "이 모드의 표시 이름입니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "버전" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "현재 모드 버전입니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "작성자의 모드가 있는 Paradox Mods 웹사이트를 엽니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "커뮤니티 Discord를 브라우저에서 엽니다." },


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "상세 디버그 로그" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "문제 해결용 추가 세부 정보를 <AdjustTransit.log> 로 보냅니다.\n" +
                    "일반 플레이에서는 **비활성화**하세요.\n" +
                    "<이 옵션은 로깅만 늘리며 게임플레이 값은 변경하지 않습니다.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "로그 폴더 열기" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "로그 폴더를 엽니다.\n" +
                    "다음: 텍스트 편집기로 <AdjustTransit.log> 를 여세요 (Notepad++ 권장)." },

            };
        }

        public void Unload( )
        {
        }
    }
}
