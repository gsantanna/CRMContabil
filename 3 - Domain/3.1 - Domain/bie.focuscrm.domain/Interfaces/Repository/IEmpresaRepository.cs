using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.domain.Interfaces.Repository
{
    public interface IEmpresaRepository : IRepositoryBase<Empresa>
    {
        void RemoveFilial(Empresa obj, int id);
    }
}