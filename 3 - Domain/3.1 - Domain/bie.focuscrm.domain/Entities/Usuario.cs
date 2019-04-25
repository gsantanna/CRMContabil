using System.Collections.Generic;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.domain.Entities
{
    /// <summary>
    /// Proxy de usuário para o usuário do Asp NET Identity, esta classe mantém o id_usuário entre as duas tabelas (sistema e asp net identity) 
    /// </summary>
    public class Usuario
    {
        //string para sincronizar com o login 
        public string id_usuario { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public string Telefone { get; set; }
        public string Telefone2 { get; set; }

        public int? id_empresa { get; set; }
        public virtual Empresa Empresa { get; set; }

        public virtual ICollection<Atendimento> Atas { get; set; }

        public virtual ICollection<Pendencia> Pendencias { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }

        public virtual ICollection<Setor> ResponsavelSetores { get; set; }

        public virtual ICollection<Setor> Setores { get; set; }

        public virtual ICollection<RespostaPesquisa> Pesquisas { get; set; }

        public virtual ICollection<Atendimento> AtendimentosFocus { get; set; }

        public virtual ICollection<Atendimento> AtendimentosCliente { get; set; }

        public virtual ICollection<AlertaAlcance> AlertasAlcances { get; set; }

        public string Email { get; set; }

    }
}