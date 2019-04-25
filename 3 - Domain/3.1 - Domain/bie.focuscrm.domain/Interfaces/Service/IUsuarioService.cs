using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.domain.Interfaces.Service
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Usuario GetById(string id);

    }
}