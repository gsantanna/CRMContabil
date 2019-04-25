using System;
using System.Collections.Generic;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.domain.Entities
{
    public class Pesquisa
    {
        public int id_pesquisa { get; set; }

        public TipoPesquisa tp_pesquisa { get; set; }

        public bool Ativo { get; set; }

        public DateTime Criado { get; set; }

        public DateTime Modificado { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<QuestaoPesquisa> Questoes { get; set; }

        public virtual ICollection<RespostaPesquisa> Respostas { get; set; }

    }
}