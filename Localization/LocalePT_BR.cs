// File: Localization/LocalePT_BR.cs
// Portuguese (pt-BR) strings for Options UI.

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

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.PublicTransitTab), "Transporte público" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "Sobre" },

                // --------------------
                // Public Transit tab
                // --------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.LineVehiclesGroup), "Linhas de transporte (faixa do controle no jogo)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLineVehicleCountTuner)), "Linhas de transporte estendidas" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLineVehicleCountTuner)),
                    "Expande a **faixa** do controle deslizante de linhas de transporte no jogo para cada rota.\n" +
                    "**Até (1)** em todas as rotas testadas.\n" +
                    "O **limite máximo varia**; todas as rotas testadas alcançaram pelo menos 3× acima do vanilla.\n" +
                    "Nota técnica: o jogo usa o tempo da rota (tempo de viagem + número de paradas), então o máximo é variável e não estático.\n" +
                    "Funciona para ônibus, ferry, bonde, trem, metrô, navio e avião.\n\n" +
                    "<Aviso de conflito>: se Public Works Plus ou outro mod editar a mesma política de linhas de transporte, deixe este recurso ativado em apenas um mod."
                },

                // Depot capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.DepotGroup), "Capacidade dos depósitos (máx. de veículos por depósito)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusDepotScalar)), "Depósito de ônibus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusDepotScalar)),
                    "Altera quantos ônibus cada **depósito de ônibus** pode manter ou gerar.\n" +
                    "**100%** = vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais.\n" +
                    "Aplica-se ao prédio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryDepotScalar)), "Depósito de ferry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryDepotScalar)),
                    "Altera quantos ferries cada **depósito de ferry** pode manter ou gerar.\n" +
                    "**100%** = vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais.\n" +
                    "Aplica-se ao prédio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayDepotScalar)), "Depósito de metrô" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayDepotScalar)),
                    "Altera quantos veículos de metrô cada **depósito de metrô** pode manter.\n" +
                    "**100%** = vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais.\n" +
                    "Aplica-se ao prédio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiDepotScalar)), "Depósito de táxi" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiDepotScalar)),
                    "Altera quantos táxis cada **depósito de táxi** pode manter.\n" +
                    "**100%** = vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais.\n" +
                    "Valores altos podem criar tráfego excessivo de táxis."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramDepotScalar)), "Depósito de bonde" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramDepotScalar)),
                    "Altera quantos bondes cada **depósito de bonde** pode manter.\n" +
                    "**100%** = vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais.\n" +
                    "Aplica-se ao prédio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainDepotScalar)), "Depósito de trem" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainDepotScalar)),
                    "Altera quantos trens cada **depósito de trem** pode manter.\n" +
                    "**100%** = vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais.\n" +
                    "Aplica-se ao prédio base."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetDepotToVanillaButton)), "Redefinir depósitos" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetDepotToVanillaButton)),
                    "Retorna todos os controles de depósito para **100%** (padrão do jogo / vanilla)." },

                // Passenger capacity sliders
                { m_Setting.GetOptionGroupLocaleID(Setting.PassengerGroup), "Capacidade de passageiros (máx. de pessoas por veículo)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusPassengerScalar)), "Ônibus" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusPassengerScalar)),
                    "Altera a capacidade de **passageiros do ônibus**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramPassengerScalar)), "Bonde" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramPassengerScalar)),
                    "Altera a capacidade de **passageiros do bonde**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainPassengerScalar)), "Trem" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainPassengerScalar)),
                    "Altera a capacidade de **passageiros do trem**.\n" +
                    "Aplica-se a locomotivas e seções.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwayPassengerScalar)), "Metrô" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwayPassengerScalar)),
                    "Altera a capacidade de **passageiros do metrô**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipPassengerScalar)), "Navio" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipPassengerScalar)),
                    "Altera a capacidade de **navios de passageiros** (não navios de carga).\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FerryPassengerScalar)), "Ferry" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FerryPassengerScalar)),
                    "Altera a capacidade de **passageiros do ferry**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AirplanePassengerScalar)), "Avião" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AirplanePassengerScalar)),
                    "Altera a capacidade de **passageiros do avião**.\n" +
                    "**10%** = 10% dos assentos vanilla.\n" +
                    "**100%** = assentos vanilla (padrão do jogo).\n" +
                    "**1000%** = 10× mais assentos."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DoublePassengersButton)), "Dobrar tudo" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DoublePassengersButton)),
                    "Define todos os controles de passageiros para **200%**."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetPassengerToVanillaButton)), "Redefinir todos os passageiros" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetPassengerToVanillaButton)),
                    "Retorna todos os controles de passageiros para **100%** (padrão do jogo / vanilla)." },

                // -------------------
                // About tab
                // -------------------

                { m_Setting.GetOptionGroupLocaleID(Setting.AboutInfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.AboutLinksGroup), "Links de suporte" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Debug / Logs" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModNameDisplay)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModNameDisplay)), "Nome exibido deste mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ModVersionDisplay)), "Versão" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ModVersionDisplay)), "Versão atual do mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxMods)), "Paradox" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxMods)), "Abre a página do autor no Paradox Mods." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscord)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscord)), "Abre o Discord da comunidade no navegador." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDebugLogging)), "Logs debug detalhados" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDebugLogging)),
                    "Envia detalhes extras para o arquivo de log deste mod para solução de problemas.\n" +
                    "**Desativar** para jogo normal.\n" +
                    "<Isso apenas aumenta o registro e não altera os valores de jogabilidade.>"
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Abrir pasta de logs" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)),
                    "Abre a pasta de logs deste mod."
                },
            };
        }

        public void Unload()
        {
        }
    }
}
