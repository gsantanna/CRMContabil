using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bie.focuscrm.domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace bie.focuscrm.ui.viewmodel
{
    public class ParametroAgendamentoViewmodel
    {
        public int id_empresa { get; set; }

        [Required(ErrorMessage ="Selecione o setor")]
        [Display(Name="Setor")]
        public int id_setor { get; set; }

        public string SetorNome { get; set; }
        public int Intervalo { get; set; }
    }
}
