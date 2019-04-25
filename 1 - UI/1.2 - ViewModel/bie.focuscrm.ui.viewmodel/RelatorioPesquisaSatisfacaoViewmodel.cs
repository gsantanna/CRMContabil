using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bie.focuscrm.domain.Entities;
using System.ComponentModel.DataAnnotations;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.ui.viewmodel
{
    public class RelatorioPesquisaSatisfacaoViewmodel  
    {
        public int id_pesquisa { get; set; }

        public TipoPesquisa tp_pesquisa { get; set; }

        public bool Ativo { get; set; }

        public DateTime Criado { get; set; }

        public DateTime Modificado { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public IEnumerable<QuestaoPesquisaViewmodel> Questoes { get; set; }

        public ICollection<RespostaPesquisaViewmodel> Respostas { get; set; }
    }
}
