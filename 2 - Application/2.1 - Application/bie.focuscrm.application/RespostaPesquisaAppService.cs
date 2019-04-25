using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.application
{
    public class RespostaPesquisaAppService : AppServiceBase<RespostaPesquisa>, IRespostaPesquisaAppService
    {
        private readonly IRespostaPesquisaService _svc;
        public RespostaPesquisaAppService(IRespostaPesquisaService Svc)
            : base(Svc)
        {
            _svc = Svc;
        }
    }
}
