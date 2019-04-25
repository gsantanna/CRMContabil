using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bie.focuscrm.ui.viewmodel
{
    public class PendenciaViewmodel
    {
        public int id_pendencia { get; set; }


        [Display(Name ="Atendimento")]
        public int id_atendimento { get; set; }
        public bool id_atendimentopassado { get; set; }


        [Display(Name ="Pendência")]
        public string Titulo { get; set; }

        [Required(ErrorMessage ="Favor informar o prazo")]
        public DateTime Prazo { get; set; }

        [Display(Name ="Responsável")]
        public string id_responsavel { get; set; }


        [Required(ErrorMessage = "Favor indicar o status da pendencia")]
        public string Status { get; set; }

        public DateTime Criado { get; set; }

        public DateTime Modificado { get; set; }

        public string Historico { get; set; }

        public List<ComentarioViewmodel> Comentarios { get; set; }

        public string NomeResponsavel { get; set; }
        public string AssuntoAtendimento { get; set; }
        public string NomeSetor { get; set; }
        public string NomeEmpresa { get; set; }

        public string InfoStatus
        {
            get //o agendmaento foi feito + não foi 
            {
                if (Status.ToLower() == "concluído" || Status.ToLower() == "cancelado")
                {
                    return Status;
                } else if (Prazo < DateTime.Now)
                {
                    return "Atraso";
                }

                return Status;

            }
        }


    }
}
