namespace bie.focuscrm.domain.Entities
{
    public class Anexo
    {
        public int id_anexo { get; set; }
        public int id_atendimento { get; set; }
        public virtual Atendimento Atendimento { get; set; }

        public string NomeArquivo { get; set; }
        public string Mime { get; set; }
        public string Icone { get; set; }
        public byte[] Conteudo { get; set; }
        public long Tamanho { get; set; }
    }

}