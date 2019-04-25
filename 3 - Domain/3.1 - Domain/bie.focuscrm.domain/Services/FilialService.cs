using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.domain.Services
{
    public class FilialService : ServiceBase<Filial>, IFilialService
    {
        private readonly IFilialRepository _Repo;
        public FilialService(IFilialRepository Repo)
            : base(Repo)
        {
            _Repo = Repo;
        }
    }
}