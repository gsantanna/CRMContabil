using System.Linq;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;

namespace bie.focuscrm.infra.data.Repository
{
    public class PesquisaRepository : RepositoryBase<Pesquisa>, IPesquisaRepository
    {
        public new void Remove(Pesquisa obj)
        {
            //Encontra as respotas 
            foreach (var resposta in obj.Respostas.ToList())
            {
                foreach (var respostaValor in resposta.Valores.ToList())
                {
                    Db.Set<RespostaPesquisaValor>().Remove(respostaValor);

                    Db.SaveChanges();
                }

                Db.Set<RespostaPesquisa>().Remove(resposta);

                Db.SaveChanges();
            }

            // encontra as questoes
            foreach (var questao in obj.Questoes.ToList())
            {
                Db.Set<QuestaoPesquisa>().Remove(questao);

                Db.SaveChanges();
            }

            Db.Set<Pesquisa>().Remove(obj);

            Db.SaveChanges();
        }
        
        public void RemoveQuestao(Pesquisa obj, int id)
        {
            //Encontra as respotas 
            foreach (var resposta in obj.Respostas.ToList())
            {
                foreach (var respostaValor in resposta.Valores.ToList())
                {
                    if (respostaValor.id_questao == id)
                    {
                        Db.Set<RespostaPesquisaValor>().Remove(respostaValor);

                        Db.SaveChanges();
                    }
                }
            }

            // encontra as questoes
            foreach (var questao in obj.Questoes.ToList())
            {
                if (questao.id_questaopesquisa == id)
                {
                    Db.Set<QuestaoPesquisa>().Remove(questao);

                    Db.SaveChanges();
                }
            }
        }
    }
}