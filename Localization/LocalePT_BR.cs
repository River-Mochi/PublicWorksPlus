// File: Localization/LocalePT_BR.cs
// Portuguese-Brazil (pt-BR) strings for Options UI.

namespace PublicWorksPlus
{
    using Colossal;
    using System.Collections.Generic;

    public sealed class LocalePT_BR : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocalePT_BR(Setting setting)
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
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "Transporte público" },
                { m_Setting.GetOptionTabLocaleID(Setting.IndustryTab),      "Indústria" },
                { m_Setting.GetOptionTabLocaleID(Setting.ParksRoadsTab),    "Parques-Estradas" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab),         "Sobre" },

                // --------------------
                // Public-Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Linhas de transporte (intervalo do controle no jogo)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Expandir mín/máx da linha de transporte" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Aumenta o **intervalo** do controle deslizante de Linha de Transporte no jogo para cada rota.\n" +
                    "**Tão baixo quanto (1)** em todas as rotas testadas.\n" +
                    "O **limite máximo varia**; mas todos ficam 3× ou mais acima do vanilla.\n" +
                    "Nota técnica: o jogo usa o tempo da rota (tempo de direção + número de paradas); isso cria um máximo variável (este mod segue a lógica do jogo, então não define um máximo fixo como 200).\n" +
                    "Funciona para todo transporte: ônibus, ferry, bonde, trem, metrô, navio, avião.\n\n" +
                    "**---------------**\n" +
                    "Dica: se quiser aumentar um pouco mais o limite máximo do controle, adicione algumas paradas à rota.\n" +
                    "O jogo aumenta automaticamente o máximo com base nas paradas adicionadas + fatores; adicionar paradas é um ajuste simples para o jogador.\n" +
                    "<Evitar conflitos>: remova mods que editem a mesma política de Linha de Transporte.\n" +
                    "Desative se não precisar do recurso ou se precisar deixá-lo desligado para usar outro mod para a mesma coisa."
                },

                // Depot Capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Capacidade de depósitos (máx. de veículos por depósito)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Depósito de ônibus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Altera quantos ônibus cada **Depósito de Ônibus** pode manter/gerar.\n" +
                    "**100%** = vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais.\n" +
                    "Aplica-se ao prédio base." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Depósito de ferry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "**Depósito de Ferry**: máximo de veículos por prédio.\n" +
                    "**100%** = vanilla (padrão do jogo).\n" +
                    "Aplica-se ao prédio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "Depósito de metrô" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Altera quantos veículos de metrô cada **Depósito de Metrô** pode manter.\n" +
                    "Aplica-se ao prédio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Depósito de táxi" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Quantos táxis cada **Depósito de Táxi** pode manter.\n" +
                    "Se for definido no máximo, pode causar uma quantidade excessiva e cômica de táxis."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Depósito de bonde" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Altera quantos bondes cada **Depósito de Bonde** pode manter.\n" +
                    "Aplica-se ao prédio base." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Depósito de trem" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Altera quantos trens cada **Depósito de Trem** pode manter.\n" +
                    "Aplica-se ao prédio base." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Redefinir depósitos" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Define todos os controles dos depósitos de volta para **100%** (padrão do jogo / vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Capacidade de passageiros (máx. de pessoas por veículo)" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Ônibus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Altera a capacidade de **passageiros do ônibus**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Bonde" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Altera a capacidade de **passageiros do bonde**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Trem" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Altera a capacidade de **passageiros do trem**.\n" +
                    "Aplica-se a locomotivas e seções.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "Metrô" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Altera a capacidade de **passageiros do metrô**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Navio" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Altera a capacidade de **navios de passageiros** (não navios de carga).\n" +
                    "**100%** = assentos vanilla (padrão do jogo)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Ferry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Altera a capacidade de **passageiros do ferry**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Avião" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Altera a capacidade de **passageiros do avião**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Dobrar" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Define cada controle de passageiros para **200%**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Redefinir todos os passageiros" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Define todos os controles de passageiros de volta para **100%**\n" +
                    "(padrão do jogo / vanilla)." },

                // ----------------
                // INDUSTRY tab
                // ----------------

                { m_Setting.GetOptionGroupLocaleID(Setting.DeliveryGroup), "Veículos de entrega (capacidade de carga)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SemiTruckCargoScalar)), "Caminhões semirreboque" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SemiTruckCargoScalar)),
                    "Capacidade dos **caminhões semirreboque**.\n" +
                    "Inclui:\n" +
                    "* Semirreboques da indústria especializada (fazendas, pesca, silvicultura etc.).\n" +
                    "* Semirreboques transportando correio para/de estações de carga (não é o mesmo que entrega local de correio).\n" +
                    "**1× = 25t** (vanilla)\n" +
                    "**10×** = 10× mais." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DeliveryVanCargoScalar)), "Vans de entrega" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DeliveryVanCargoScalar)),
                    "**Vans de entrega**\n" +
                    "**1× = 4t** (vanilla)\n" +
                    "**10×** = 10× mais." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilTruckCargoScalar)), "Caminhões de matéria-prima" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilTruckCargoScalar)),
                    "**Caminhões de matéria-prima** (petróleo, carvão, minério, pedra)\n" +
                    "**1× = 20t** (vanilla)\n" +
                    "**10×** = 10× mais." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)), "Moto de entrega" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.MotorbikeDeliveryCargoScalar)),
                    "**Entrega por moto** normalmente leva produtos farmacêuticos a um hospital/clínica.\n" +
                    "**1× = 0.1t** (vanilla)\n" +
                    "**10×** = 10× mais." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)), "Redefinir entregas" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDeliveryToVanillaButton)),
                    "Define os multiplicadores de entrega de volta para **1×** (padrão do jogo / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.CargoStationsGroup), "Frota de carga (porto, trem, aeroporto)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)), "Máx. frota da estação de carga" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CargoStationMaxTrucksScalar)),
                    "Multiplicador para o máximo de transportadores ativos das **estações de transporte de carga**.\n" +
                    "**1×** = vanilla, **5×** = 5× mais." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)), "Frota dos extratores" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ExtractorMaxTrucksScalar)),
                    "Multiplicador para os **caminhões máximos dos extratores** industriais\n" +
                    "(fazendas, pesca, silvicultura, minério, petróleo, carvão, pedra).\n" +
                    "**1×** = vanilla, **5×** = 5× mais." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)), "Redefinir frota de carga + extratores" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetCargoStationsToVanillaButton)),
                    "Define os multiplicadores da estação de carga + extratores de volta para **1×** (padrão do jogo / vanilla)." },

                // -------------------
                // Parks-Roads
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.ParkMaintenanceGroup), "Manutenção de parques" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)), "Capacidade do turno de trabalho" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleCapacityScalar)),
                    "Multiplicador para a **capacidade do turno de trabalho** (capacidade do veículo).\n" +
                    "Quantidade total de trabalho que um caminhão pode fazer antes de voltar ao prédio.\n" +
                    "Pense assim: suprimentos extras = fica mais tempo na rua." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)), "Taxa do veículo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceVehicleRateScalar)),
                    "Multiplicador para a **taxa de trabalho do veículo**.\n" +
                    "Taxa = quanto trabalho ele faz por tick de simulação enquanto está parado." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)), "Tamanho da frota do depósito" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ParkMaintenanceDepotScalar)),
                    "Multiplicador para os **veículos máximos** do prédio do depósito.\n" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)), "Redefinir manutenção de parques" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetParkMaintenanceToVanillaButton)),
                    "Redefine todos os valores para **100%** (padrão do jogo / vanilla)." },

                { m_Setting.GetOptionGroupLocaleID(Setting.RoadMaintenanceGroup), "Manutenção de estradas" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)), "Tamanho da frota do depósito" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceDepotScalar)),
                    "Multiplicador para os **veículos máximos do depósito** por prédio.\n" +
                    "Maior = mais caminhões.\n" +
                    "<Nota de equilíbrio: poucos ou muitos demais podem prejudicar o trânsito.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)), "Capacidade do turno de trabalho" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleCapacityScalar)),
                    "Multiplicador para a **capacidade do turno de trabalho**.\n" +
                    "Quantidade total de trabalho que um caminhão pode fazer antes de voltar ao depósito.\n" +
                    "**Maior = menos retornos** ao prédio principal. Mais eficiente." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)), "Taxa de reparo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMaintenanceVehicleRateScalar)),
                    "Taxa = quanto trabalho ele faz por tick de simulação enquanto está parado.\n" +
                    "Os caminhões ainda fazem uma rápida parada+arrancada mesmo com a maior taxa; eles apenas fazem mais trabalho por parada.\n" +
                    "No vanilla, uma parada não necessariamente leva a estrada de volta a 100% de reparo, então esse recurso melhora com o tempo.\n"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadWearScalar)), "Desgaste das estradas" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadWearScalar)),
                    "<NOVO recurso Alpha>\n" +
                    "Controla a velocidade com que as estradas se deterioram por fatores de **tempo e tráfego**.\n" +
                    "**10%** = desgaste 10× mais lento (menos reparos necessários)\n" +
                    "**100%** = vanilla\n" +
                    "**500%** = dano 5× mais rápido (mais reparos/caminhões necessários)\n" +
                    "Como funciona no jogo:\n" +
                    "Se m_Wear <= 2.5, não há lentidão.\n" +
                    "Se m_Wear >= 17.5, penalidade máxima, os veículos ficam 50% mais lentos nas estradas.\n" +
                    "Veja a Infoview de Estradas: mostra em vermelho as estradas muito danificadas que reduzem a velocidade dos veículos."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)), "Redefinir manutenção de estradas" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetRoadMaintenanceToVanillaButton)),
                    "Define todos os valores de volta para **100%** (padrão do jogo / vanilla)." },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Links de suporte" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Debug / Log" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nome exibido deste mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versão" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Versão atual do mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Abre o site Paradox Mods para os mods do autor." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Abre o Discord da comunidade no navegador." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RunPrefabScanButton)), "Relatório de varredura (prefabs)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RunPrefabScanButton)),
                    "Cria um relatório <único> para depuração.\n" +
                    "Não é necessário para jogabilidade normal.\n" +
                    "Local do arquivo: <ModsData/PublicWorksPlus/ScanReport-Prefabs.txt>\n" +
                    "Dica: clique <uma vez>; quando o status mostrar Concluído, use <Abrir pasta do relatório>." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PrefabScanStatus)), "Status da varredura de prefabs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PrefabScanStatus)),
                    "Mostra o estado da varredura: Inativo / Na fila / Em execução / Concluído / Sem dados.\n" +
                    "Na fila/Em execução mostra o tempo decorrido; Concluído mostra duração + horário de conclusão." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Logs de debug detalhados" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Envia detalhes extras para <PublicWorksPlus.log> para solução de problemas.\n" +
                    "**Desative** para jogabilidade normal.\n" +
                    "<Isto só aumenta o log e não altera valores de jogabilidade.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Abrir pasta de logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Abre a pasta de logs.\n" +
                    "Próximo: abra <PublicWorksPlus.log> com o editor de texto (Notepad++ recomendado)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenReportButton)), "Abrir pasta do relatório" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenReportButton)),
                    "Abre a pasta do relatório.\n" +
                    "Próximo: abra <ScanReport-Prefabs.txt> com o editor de texto (por ex., Notepad++)." },

                // ---- Scan Report Status Text (format string templates) ----
                { "PWP_SCAN_IDLE", "Inativo" },
                { "PWP_SCAN_QUEUED_FMT", "Na fila ({0})" },
                { "PWP_SCAN_RUNNING_FMT", "Em execução ({0})" },
                { "PWP_SCAN_DONE_FMT", "Concluído ({0} | {1})" },
                { "PWP_SCAN_FAILED", "Falhou" },
                { "PWP_SCAN_FAIL_NO_CITY", "Carregue a cidade primeiro" },
                { "PWP_SCAN_UNKNOWN_TIME", "hora desconhecida" },

            };
        }

        public void Unload( )
        {
        }
    }
}
