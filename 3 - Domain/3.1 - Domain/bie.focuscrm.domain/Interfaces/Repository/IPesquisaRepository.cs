using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.domain.Interfaces.Repository
{
    public interface IPesquisaRepository : IRepositoryBase<Pesquisa>
    {
        void RemoveQuestao(Pesquisa obj, int id);
    }
}