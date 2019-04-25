using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.ui.viewmodel
{
    /// <summary>
    /// Representa um setor dentro da empresa FOCUS
    /// </summary>
    public class SetorViewmodel
    {
        public SetorViewmodel()
        {
            FuncionariosSetor = new string[0];
            
        }

        public int id_setor { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }

        public ICollection<Usuario> Funcionarios { get; set; }

        [Display(Name ="Usuário Responsável")]
        [Required(ErrorMessage = "Selecione um resposável")]
        public string id_usuarioresponsavel { get; set; }

        public string NomeResponsavel { get; set; }

        public bool Ativo { get; set; }

        public string[] FuncionariosSetor { get; set; }
        
        public string Ativo_desc
        {
            get
            {
                return Ativo ? "Sim" : "Não";
            }
        }
        
    }
}