// File: Localization/LocaleKO.cs
// Korean (ko-KR) strings for Options UI.

namespace PublicWorksPlus
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
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "산업" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "공원-도로" },
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

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "배송 차량 (화물 용량)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "세미트럭" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "**세미트럭** 용량.\n" +
                    "포함:\n" +
                    "* 특화 산업 세미트럭 (농장, 어업, 임업 등).\n" +
                    "* 화물역으로 우편을 운반하는 세미트럭 (지역 우편 배달과는 다름).\n" +
                    "**1× = 25t** (바닐라)\n" +
                    "**10×** = 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "배송 밴" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**배송 밴**\n" +
                    "**1× = 4t** (바닐라)\n" +
                    "**10×** = 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilTruckCargoScalar)), "원자재 트럭" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilTruckCargoScalar)),
                    "**원자재 트럭** (석유, 석탄, 광석, 석재)\n" +
                    "**1× = 20t** (바닐라)\n" +
                    "**10×** = 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "배송 오토바이" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**오토바이 배송**은 보통 약품을 병원/클리닉으로 운반합니다.\n" +
                    "**1× = 0.1t** (바닐라)\n" +
                    "**10×** = 10배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "배송 기본값 리셋" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "배송 배수를 **1×** (게임 기본값 / 바닐라)로 되돌립니다." },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "화물 플릿 (항구, 철도, 공항)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "화물역 최대 플릿" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "**화물 운송역**의 최대 활성 운송 차량 수에 대한 배수입니다.\n" +
                    "**1×** = 바닐라, **5×** = 5배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "채취 시설 플릿" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "산업용 **채취 시설 최대 트럭 수**에 대한 배수입니다\n" +
                    "(농장, 어업, 임업, 광석, 석유, 석탄, 석재).\n" +
                    "**1×** = 바닐라, **5×** = 5배." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "화물 + 채취 시설 플릿 리셋" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "화물역 + 채취 시설 배수를 **1×** (게임 기본값 / 바닐라)로 되돌립니다." },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "공원 유지관리" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "작업 교대 용량" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "**작업 교대 용량** (차량 용량)에 대한 배수입니다.\n" +
                    "트럭이 건물로 돌아가기 전에 수행할 수 있는 총 작업량입니다.\n" +
                    "쉽게 말해: 보급이 많을수록 더 오래 현장에 머뭅니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "차량 작업률" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "**차량 작업률**에 대한 배수입니다.\n" +
                    "작업률 = 정차 중 시뮬레이션 tick당 수행하는 작업량." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "차고 플릿 크기" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "차고 건물의 **최대 차량 수**에 대한 배수입니다.\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "공원 유지관리 리셋" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "모든 값을 **100%** (게임 기본값 / 바닐라)로 되돌립니다." },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "도로 유지관리" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "차고 플릿 크기" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "건물당 **차고 최대 차량 수**에 대한 배수입니다.\n" +
                    "높을수록 = 트럭 증가.\n" +
                    "<밸런스 참고: 너무 적거나 너무 많으면 교통에 악영향을 줄 수 있습니다.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "작업 교대 용량" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "**작업 교대 용량**에 대한 배수입니다.\n" +
                    "트럭이 차고로 돌아가기 전에 수행할 수 있는 총 작업량입니다.\n" +
                    "**높을수록 = 복귀 횟수 감소**. 더 효율적입니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "수리율" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "작업률 = 정차 중 시뮬레이션 tick당 수행하는 작업량.\n" +
                    "최고 수리율에서도 트럭은 잠깐 멈췄다 가는 동작을 합니다. 단지 한 번 멈출 때 더 많은 작업을 수행합니다.\n" +
                    "바닐라에서는 한 번의 정차로 도로가 반드시 100% 수리되는 것은 아니므로, 이 기능은 시간이 지날수록 더 유용해집니다.\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "도로 마모" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<새로운 Alpha 기능>\n" +
                    "**시간과 교통량** 요인으로 도로가 얼마나 빨리 손상되는지 제어합니다.\n" +
                    "**10%** = 마모 10× 느림 (수리 필요 감소)\n" +
                    "**100%** = 바닐라\n" +
                    "**500%** = 손상 5× 빠름 (더 많은 수리/트럭 필요)\n" +
                    "게임 내 작동 방식:\n" +
                    "m_Wear <= 2.5 이면 감속 없음.\n" +
                    "m_Wear >= 17.5 이면 최대 페널티, 도로 위 차량 속도가 50% 느려집니다.\n" +
                    "도로 인포뷰 참조: 심하게 손상된 도로는 빨간색으로 표시되며 차량을 감속시킵니다."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "도로 유지관리 리셋" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "모든 값을 **100%** (게임 기본값 / 바닐라)로 되돌립니다." },

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

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "스캔 보고서 (prefab)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "디버깅용 <1회성> 보고서를 생성합니다.\n" +
                    "일반 플레이에는 필요하지 않습니다.\n" +
                    "파일 위치: <ModsData/PublicWorksPlus/ScanReport-Prefabs.txt>\n" +
                    "팁: <한 번> 클릭하고, 상태가 완료로 표시되면 <보고서 폴더 열기>를 사용하세요." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "Prefab 스캔 상태" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "스캔 상태 표시: 대기 중 / 대기열 / 실행 중 / 완료 / 데이터 없음.\n" +
                    "대기열/실행 중 은 경과 시간을 표시하고, 완료 는 소요 시간 + 완료 시각을 표시합니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "상세 디버그 로그" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "문제 해결용 추가 세부 정보를 <PublicWorksPlus.log> 로 보냅니다.\n" +
                    "일반 플레이에서는 **비활성화**하세요.\n" +
                    "<이 옵션은 로깅만 늘리며 게임플레이 값은 변경하지 않습니다.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "로그 폴더 열기" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "로그 폴더를 엽니다.\n" +
                    "다음: 텍스트 편집기로 <PublicWorksPlus.log> 를 여세요 (Notepad++ 권장)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "보고서 폴더 열기" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "보고서 폴더를 엽니다.\n" +
                    "다음: 텍스트 편집기로 <ScanReport-Prefabs.txt> 를 여세요 (예: Notepad++)." },

                // ---- Scan Report Status Text (format string templates) ----
                { "PWP_SCAN_IDLE", "대기 중" },
                { "PWP_SCAN_QUEUED_FMT", "대기열 ({0})" },
                { "PWP_SCAN_RUNNING_FMT", "실행 중 ({0})" },
                { "PWP_SCAN_DONE_FMT", "완료 ({0} | {1})" },
                { "PWP_SCAN_FAILED", "실패" },
                { "PWP_SCAN_FAIL_NO_CITY", "먼저 도시 로드" },
                { "PWP_SCAN_UNKNOWN_TIME", "알 수 없는 시간" },

            };
        }

        public void Unload( )
        {
        }
    }
}
