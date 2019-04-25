namespace bie.focuscrm.domain.Entities
{
    public class Arquivo
    {
        public string Nome { get; set; }
        public string Mime { get; set; }
        public string Ext { get; set; }
        public long Tamanho { get; set; }

        public string Url { get; set; }
        public byte[] Conteudo { get; set; }

    }
}