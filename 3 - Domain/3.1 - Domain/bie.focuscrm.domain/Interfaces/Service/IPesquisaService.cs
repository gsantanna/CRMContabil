using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.domain.Interfaces.Service
{
    public interface IPesquisaService : IServiceBase<Pesquisa>
    {
        void RemoveQuestao(Pesquisa obj, int id);
    }
}