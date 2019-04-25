
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {

        Usuario GetById(string id);

    }
}