using System.Collections.Generic;

namespace bie.focuscrm.domain.Entities
{
    /// <summary>
    /// Representa um setor dentro da empresa FOCUS
    /// </summary>
    public class Setor
    {
        public int id_setor { get; set; }

        public string Nome { get; set; }

        public virtual ICollection<Usuario> Funcionarios { get; set; }
        
        public string id_usuarioresponsavel { get; set; }

        public virtual Usuario UsuarioResponsavel { get; set; }
        
        public bool Ativo { get; set; }
        
        public virtual ICollection<ParametroAgendamento> ParametrosAgendamento { get; set; }

        public virtual ICollection<Atendimento> Atendimentos { get; set; }


    }
}