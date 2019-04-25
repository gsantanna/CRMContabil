using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.application.Interfaces
{
    public interface IUsuarioAppService : IAppServiceBase<Usuario>
    {
        Usuario GetById(string id);
    }
}