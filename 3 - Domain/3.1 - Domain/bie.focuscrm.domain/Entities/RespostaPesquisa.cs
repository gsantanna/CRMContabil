
using System;
using System.Collections.Generic;

namespace bie.focuscrm.domain.Entities
{
    public class RespostaPesquisa
    {
        public int id_respostapesquisa { get; set; }
        
        //FK - id pesquisa 
        public int id_pesquisa { get; set; }
        public virtual Pesquisa Pesquisa { get; set; }

        //FK - id usuario 
        public string id_usuario { get; set; }
        public virtual Usuario Usuario { get; set; }


        //FK - id atendimento 
        public int id_atendimento { get; set; }
        public virtual Atendimento Atendimento { get; set; }

        public DateTime DataEnvio { get; set; }
        public DateTime? DataResposta { get; set; }
        
        public virtual ICollection<RespostaPesquisaValor> Valores { get; set; }


    }
}