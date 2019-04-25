using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.domain.Services
{
    public class AlertaService : ServiceBase<Alerta>, IAlertaService
    {
        private readonly IAlertaRepository _Repo;
        public AlertaService(IAlertaRepository Repo)
            : base(Repo)
        {
            _Repo = Repo;
        }
    }
}