using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.domain.Services
{
    public class PesquisaService : ServiceBase<Pesquisa>, IPesquisaService
    {
        private readonly IPesquisaRepository _Repo;
        public PesquisaService(IPesquisaRepository Repo)
            : base(Repo)
        {
            _Repo = Repo;
        }

        public void RemoveQuestao(Pesquisa obj, int id)
        {
            _Repo.RemoveQuestao(obj, id);
        }
    }
}