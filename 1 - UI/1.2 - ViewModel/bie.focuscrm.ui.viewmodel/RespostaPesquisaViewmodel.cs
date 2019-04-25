using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bie.focuscrm.domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace bie.focuscrm.ui.viewmodel
{
    public class RespostaPesquisaViewmodel
    {
        public int id_respostapesquisa { get; set; }

        public int id_pesquisa { get; set; }
        
        public string id_usuario { get; set; }
        
        public int id_atendimento { get; set; }
        
        public DateTime DataEnvio { get; set; }
        public DateTime DataResposta { get; set; }

        //public Pesquisa Pesq { get; set; }
        //public IEnumerable<RespostaPesquisaValor> Valores { get; set; }
        public Pesquisa Pesquisa { get; set; }
    }
}
