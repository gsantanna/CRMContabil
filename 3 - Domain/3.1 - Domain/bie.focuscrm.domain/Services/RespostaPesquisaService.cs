using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.domain.Services
{
    public class RespostaPesquisaService : ServiceBase<RespostaPesquisa>, IRespostaPesquisaService
    {
        private readonly IRespostaPesquisaRepository _Repo;
        public RespostaPesquisaService(IRespostaPesquisaRepository Repo)
            : base(Repo)
        {
            _Repo = Repo;
        }
    }
}