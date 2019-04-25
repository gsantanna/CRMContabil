using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.ui.viewmodel
{
    public class QuestaoPesquisaViewmodel
    {
        public int id_questaopesquisa { get; set; }
        public int id_pesquisa { get; set; }
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo Ordem é obrigatório")]
        public int Ordem { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        [Display(Name ="Descrição")]
        public string Descricao { set; get; }

        [Required(ErrorMessage = "O campo Valores é obrigatório")]
        public string Valores { set; get; }

        //public IEnumerable<RespostaPesquisaValor> Respostas { get; set; }

        public string Ativo_desc
        {
            get
            {
                return Ativo ? "Sim" : "Não";

            }
        }
    }
}
