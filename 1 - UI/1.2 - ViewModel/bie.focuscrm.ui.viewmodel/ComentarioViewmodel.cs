using System;
using System.ComponentModel.DataAnnotations;

namespace bie.focuscrm.ui.viewmodel
{
    public class ComentarioViewmodel
    {
        public int id_pendenciacomentario { get; set; }
        public int id_pendencia { get; set; }
        public string id_autor { get; set; }




        [Required(ErrorMessage ="O comentário é obrigatório")]
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }

        public string NomeAutor { get; set; }

    }
}