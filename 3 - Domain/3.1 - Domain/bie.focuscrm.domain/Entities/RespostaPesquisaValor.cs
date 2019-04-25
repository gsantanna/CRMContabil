namespace bie.focuscrm.domain.Entities
{
    public class RespostaPesquisaValor
    {



        public int id_respostapesquisa { get; set; }
        public virtual RespostaPesquisa RespostaPesquisa { get; set; }


        public int id_questao { get; set; }
        public virtual QuestaoPesquisa Questao { get; set; }




        public string ValorResposta { get; set; }

    }
}