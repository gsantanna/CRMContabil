using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.ui.viewmodel
{
    /// <summary>
    /// Representa um setor dentro da empresa CLIENTE
    /// </summary>
    public class SetorGridViewmodel
    {
        public int id_setor { get; set; }
        public string Nome { get; set; }
        public string NomeResponsavel { get; set; }
        public bool Ativo { get; set; }
        public string FuncionariosSetor_desc { get; set; }
    }
}