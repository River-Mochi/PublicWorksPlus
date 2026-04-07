// File: Localization/LocalePT_BR.cs
// Portuguese-Brazil (pt-BR) strings for Options UI.

namespace AdjustTransit
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


                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Logs de debug detalhados" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Envia detalhes extras para <AdjustTransit.log> para solução de problemas.\n" +
                    "**Desative** para jogabilidade normal.\n" +
                    "<Isto só aumenta o log e não altera valores de jogabilidade.>" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Abrir pasta de logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Abre a pasta de logs.\n" +
                    "Próximo: abra <AdjustTransit.log> com o editor de texto (Notepad++ recomendado)." },

            };
        }

        public void Unload( )
        {
        }
    }
}
