using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.application
{
    public class FilialAppService : AppServiceBase<Filial>, IFilialAppService
    {
        private readonly IFilialService _svc;
        public FilialAppService(IFilialService Svc)
            : base(Svc)
        {
            _svc = Svc;
        }
    }
}