using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.domain.Interfaces.Service
{
    public interface IEmpresaService : IServiceBase<Empresa>
    {
        void RemoveFilial(Empresa obj, int id);
    }
}