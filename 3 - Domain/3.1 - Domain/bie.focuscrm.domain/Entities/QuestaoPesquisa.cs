using System.Collections.Generic;

namespace bie.focuscrm.domain.Entities
{
    public class QuestaoPesquisa
    {
        public int id_questaopesquisa { get; set; }

        public int Ordem { get; set; }

        public string Descricao { get; set; }

        public int id_pesquisa { get; set; }
        public virtual Pesquisa Pesquisa { get; set; }

        public string Valores { get; set; }

        public virtual ICollection<RespostaPesquisaValor> Respostas { get; set; }

        public bool Ativo { get; set; }

    }
}