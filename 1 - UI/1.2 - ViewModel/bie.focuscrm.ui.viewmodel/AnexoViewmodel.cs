using System.Web;

namespace bie.focuscrm.ui.viewmodel
{

    public class AnexoViewmodel
    {
        public int id_anexo { get; set; }
        public int id_atendimento { get; set; }
        public string NomeArquivo { get; set; }
        public string Mime { get; set; }
        public string Icone { get; set; }
        public byte[] Conteudo { get; set; }
        public long Tamanho { get; set; }

        


    }

}