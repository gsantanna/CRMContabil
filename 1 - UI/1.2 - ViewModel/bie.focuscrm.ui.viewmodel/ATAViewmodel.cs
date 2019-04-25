using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bie.focuscrm.ui.viewmodel
{
    public class ATAViewmodel
    {
        public ATAViewmodel()
        {
            this.Modificado = DateTime.Now;
            this.Criado = DateTime.Now;
            this.Publicado = null;
            this.Publicar = false;

        }

        

        public int id_atendimento { get; set; }
        //public virtual Atendimento Atendimento { get; set; }

        public string Presentes { get; set; }

        [Required(ErrorMessage ="Informe o local da reunião")]
        public string Local { get; set; }

        public DateTime DataVisita { get; set; }

        //^([01]?\d|2[0-3]):[0-5]\d$
        [RegularExpression(@"^([0-1]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Hora inválida. (hh:mm)")]
        [Required(ErrorMessage ="Informe a hora de início hh:mm")]
        [Display(Name ="Início")]
        public string HoraInicio { get; set; }

        [RegularExpression(@"^([0-1]\d|2[0-3]):([0-5]\d)$", ErrorMessage = "Hora inválida. (hh:mm)")]
        [Required(ErrorMessage = "Informe a hora de término hh:mm")]
        [Display(Name ="Fim")]
        public string HoraFim { get; set; }

        [AllowHtml]
        public string Resumo { get; set; }

        public string id_autor { get; set; }
        // public virtual Usuario Autor { get; set; }



        public DateTime Criado { get; set; }
        public DateTime Modificado { get; set; }



        //  public virtual ICollection<PendenciaViewmodel> Pendencias { get; set; }

        public ICollection<AnexoViewmodel> Anexos { get; set; }


        public string NomeCliente { get; set; }
        public string AssuntoVisita { get; set; }

        public DateTime? Publicado { get; set; }

        public bool Publicar { get; set; }

  




    }
}