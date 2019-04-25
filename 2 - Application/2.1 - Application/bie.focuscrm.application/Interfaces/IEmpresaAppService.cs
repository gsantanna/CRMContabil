using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.application.Interfaces
{
    public interface IEmpresaAppService : IAppServiceBase<Empresa>
    {
        void RemoveFilial(Empresa obj, int id);
    }
}