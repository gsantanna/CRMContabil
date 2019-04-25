using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.domain.Services
{
    public class EmpresaService : ServiceBase<Empresa>, IEmpresaService
    {
        private readonly IEmpresaRepository _Repo;
        public EmpresaService(IEmpresaRepository Repo)
            : base(Repo)
        {
            _Repo = Repo;
        }
        
        public void RemoveFilial(Empresa obj, int id)
        {
            _Repo.RemoveFilial(obj, id);
        }
    }
}