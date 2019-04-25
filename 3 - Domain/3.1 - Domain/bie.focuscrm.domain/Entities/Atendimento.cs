using System;
using System.Collections.Generic;

namespace bie.focuscrm.domain.Entities
{
    public class Atendimento
    {
       
        public int id_atendimento { get; set; }

        //cliente 
        public int id_filial { get; set; }
        public virtual Filial Filial { get; set; }


        //Funcionario Responsável

        public virtual ICollection<Usuario> UsuariosFocus { get; set; }
        
        public virtual ICollection<Usuario> UsuariosCliente { get; set; }





        public string Assunto { get; set; }

        public DateTime DataAgendada { get; set; }

        public virtual ICollection<RespostaPesquisa> RespostasPesquisas { get; set; }

        public int id_setor { get; set; }
        public virtual Setor Setor { get; set; }








        #region ATA 

        public string id_autor { get; set; }
        public virtual Usuario Autor { get; set; }
        public string Presentes { get; set; }
        public string Local { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
        public string Resumo { get; set; }
        public DateTime Criado { get; set; }
        public DateTime Modificado { get; set; }
        public DateTime? Publicado { get; set; }
        public virtual ICollection<Pendencia> Pendencias { get; set; }
        public virtual ICollection<Anexo> Anexos { get; set; }

        #endregion





    }
}