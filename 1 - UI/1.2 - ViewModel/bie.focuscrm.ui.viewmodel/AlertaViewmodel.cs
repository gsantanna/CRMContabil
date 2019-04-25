using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.ui.viewmodel
{
    public class AlertaViewmodel
    {
        public AlertaViewmodel()
        {
            Alcances = new List<AlertaAlcanceViewmodel>();
            UsuariosSelecionados = new string[0];

        }
        public int id_alerta { get; set; }

        [Required(ErrorMessage = "Selecione uma classificação")]
        public ClassificacaoAlerta Classificacao { get; set; }

        public string Classificacao_desc { get { return Classificacao.ToString(); } }
                
        [Required(ErrorMessage = "O campo título é obrigatório")]
        public string Titulo { get; set; }
        public string Conteudo { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Favor inserir uma data válida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Inicio { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Favor inserir uma data válida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Fim { get; set; }

        [Display(Name="Exibições")]
        public int max_exibicoes { get; set; }

        public ICollection<AlertaAlcanceViewmodel> Alcances { get; set; }
        public string Alcances_desc
        {
            get
            {
                return Alcances.Count() > 0 ? Alcances.Count().ToString() : "Todos";
            }
        }


        public string[] UsuariosSelecionados { get; set; }


        public string classe_bootstrap
        {
            get
            {
                var classe_alerta = "";

                switch (Classificacao)
                {
                    case ClassificacaoAlerta.Aviso:
                        {
                            classe_alerta = "warning";
                            break;
                        }
                    case ClassificacaoAlerta.Informacao:
                        {
                            classe_alerta = "info";
                            break;
                        }
                    case ClassificacaoAlerta.Urgente:
                        {
                            classe_alerta = "danger";
                            break;
                        }

                    default:
                        {
                            classe_alerta = "info";
                            break;
                        }
                }

                return classe_alerta;



            }
        }




    }
}
