using System;
using System.Collections.Generic;

namespace bie.focuscrm.ui.viewmodel
{
    public class HomeViewmodel
    {
        public List<AlertaViewmodel> Alertas { get; set; }
        public List<AtendimentoViewmodel> MeusAtendimentos { get; set; }
        public List<PendenciaViewmodel> MinhasPendencias { get; set; }
        public List<ConsolidadoPendenciasViewmodel> PendenciasConsolidadas { get; set; }

        public HomeViewmodel()
        {
            Alertas = new List<AlertaViewmodel>();
            MeusAtendimentos = new List<AtendimentoViewmodel>();
            MinhasPendencias = new List<PendenciaViewmodel>();
            PendenciasConsolidadas = new List<ConsolidadoPendenciasViewmodel>();
        }

        public class ConsolidadoPendenciasViewmodel 
        {
            public string nomesetor { get; set; }
            public int id_setor { get; set; }
            public int qtd { get; set; }

        }
    }
}
