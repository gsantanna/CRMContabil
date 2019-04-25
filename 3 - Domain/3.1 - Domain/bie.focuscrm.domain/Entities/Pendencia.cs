using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bie.focuscrm.domain.Entities
{
    public class Pendencia
    {
        public int id_pendencia { get; set; }

        public int id_atendimento { get; set; }

        public virtual Atendimento Atendimento { get; set; }

        public string Titulo { get; set; }

        public DateTime Prazo { get; set; }

        public string id_responsavel { get; set; }

        public virtual Usuario Responsavel { get; set; }

        public string Status { get; set; }

        public DateTime Criado { get; set; }

        public DateTime Modificado { get; set; }

        public string Historico { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }

        //public class Comentario
        //{
        //    public int id_pendenciacomentario { get; set; }
        //    public int id_pendencia { get; set; }
        //    public virtual Pendencia Pendencia { get; set; }
        //    public string id_autor { get; set; }
        //    public Usuario Usuario { get; set; }
        //    public string Conteudo { get; set; }
        //    public DateTime Data { get; set; }

        //}

        //parametros somente leitura
        public static string[] TipoStatus => new string[] { "Pendente Contabilidade", "Pendente Cliente", "Concluído", "Cancelado" };


    }
}
