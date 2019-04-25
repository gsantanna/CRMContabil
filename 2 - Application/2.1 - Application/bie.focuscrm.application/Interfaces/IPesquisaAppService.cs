using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.application.Interfaces
{
    public interface IPesquisaAppService : IAppServiceBase<Pesquisa>
    {
        void RemoveQuestao(Pesquisa obj, int id);
    }
}