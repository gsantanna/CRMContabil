using System.Collections.Generic;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.domain.Entities
{
    public class Notification
    {
        public Notification()
        {
            Valores = new List<ValoresTemplate>();
        }

        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public List<string> Destinatarios { get; set; }
        public List<Arquivo> Anexos { get; set; }


        public List<ValoresTemplate> Valores { get; set; }

        public class ValoresTemplate
        {
            public string Chave { get; set; }
            public string Valor { get; set; }

        }

    }




}