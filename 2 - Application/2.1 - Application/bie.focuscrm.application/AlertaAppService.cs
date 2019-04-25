using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.application
{
    public class AlertaAppService : AppServiceBase<Alerta>, IAlertaAppService
    {
        private readonly IAlertaService _svc;
        public AlertaAppService(IAlertaService Svc)
            : base(Svc)
        {
            _svc = Svc;
        }
    }
}