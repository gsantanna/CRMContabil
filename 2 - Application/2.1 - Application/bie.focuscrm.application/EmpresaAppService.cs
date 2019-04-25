using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.application
{
    public class EmpresaAppService : AppServiceBase<Empresa>, IEmpresaAppService
    {
        private readonly IEmpresaService _svc;
        public EmpresaAppService(IEmpresaService Svc)
            : base(Svc)
        {
            _svc = Svc;
        }

        public void RemoveFilial(Empresa obj, int id)
        {
            _svc.RemoveFilial(obj, id);
        }
    }
}