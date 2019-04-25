using System;


namespace bie.focuscrm.domain.Entities.StoredProcedures
{
    public class AcompanhamentoAtendimentoMacro
    {
        public string grupo { get; set; }
        public int id_empresa { get; set; }
        public string nome_empresa { get; set; }        
        public int id_setor { get; set; }
        public string nome_setor { get; set; }
        public int intervalo_parametro { get; set; }
        public int? intervalo_ultima_visita { get; set; }
        public string filiais { get; set; }
        public int qtdpendencias { get; set; }
    }

    public class AcompanhamentoAtendimentoDetalhe
    {
        public int id_filial { get; set; }
        public int id_setor { get; set; }
        public int id_atendimento { get; set; }
        public string nome_setor { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
        public DateTime DataAgendada { get; set; }
        public DateTime? DataPublicada { get; set; }
        public string DataVisita { get; set; }
        public int id_empresa { get; set; }
    }

}
