using System;
using System.Collections.Generic;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.domain.Entities
{
    public class Alerta
    {

        public int id_alerta { get; set; }

        public ClassificacaoAlerta Classificacao { get; set; }

        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }

        public int max_exibicoes { get; set; }

        public virtual ICollection<AlertaAlcance> Alcances { get; set; }

        public virtual ICollection<AlertaVisualizacao> Visualizacoes { get; set; }

    }
}