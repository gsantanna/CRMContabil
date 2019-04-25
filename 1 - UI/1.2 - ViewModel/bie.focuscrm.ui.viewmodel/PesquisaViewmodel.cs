using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.ui.viewmodel
{
    public class PesquisaViewmodel
    {
        public int id_pesquisa { get; set; }
        public bool Ativo { get; set; }
        public DateTime Criado { get; set; }
        public DateTime Modificado { get; set; }

        [Required(ErrorMessage = "Selecione um tipo")]
        [Display(Name ="Tipo de Pesquisa")]
        public TipoPesquisa tp_pesquisa { get; set; }

        public string tp_pesquisa_desc
        {
            get
            {
                return tp_pesquisa.ToString();
            }
        }

        [Required(ErrorMessage = "O campo título é obrigatório")]
        public string Titulo { get; set; }

        [Display(Name ="Descrição")]
        public string Descricao { get; set; }

        //public ICollection<QuestaoPesquisaViewmodel> Questoes { get; set; }
        //public ICollection<RespostaPesquisaViewmodel> Respostas { get; set; }

        public string Ativo_desc
        {
            get
            {
                return Ativo ? "Sim" : "Não";
            }
        }

        public string Questoes_desc
        {
            get
            {
                return "0"; // Questoes.Count().ToString();
            }

        }
    }
}
