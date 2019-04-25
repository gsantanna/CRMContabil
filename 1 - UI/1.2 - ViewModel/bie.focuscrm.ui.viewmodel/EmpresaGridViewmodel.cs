using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.ui.viewmodel
{
    public class EmpresaGridViewmodel
    {
        public int id_empresa { get; set; }

        public string Nome { get; set; }

        public string CNPJ { get; set; }

        public string QtdFiliais { get; set; }


    }
}