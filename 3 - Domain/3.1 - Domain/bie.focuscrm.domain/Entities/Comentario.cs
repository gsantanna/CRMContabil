using System;

namespace bie.focuscrm.domain.Entities
{
    public class Comentario
    {
        public int id_pendenciacomentario { get; set; }
        public int id_pendencia { get; set; }
        public virtual Pendencia Pendencia { get; set; }




        public string id_autor { get; set; }
        public virtual Usuario Usuario { get; set; }


        public string Conteudo { get; set; }
        public DateTime Data { get; set; }

    }
}