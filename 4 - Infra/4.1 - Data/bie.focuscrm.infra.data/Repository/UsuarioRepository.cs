using System.Linq;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;

namespace bie.focuscrm.infra.data.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public Usuario GetById(string id)
        {
            var resp = from u in Db.Set<Usuario>()
                       where u.id_usuario == id
                       select u;

            return resp.FirstOrDefault();

        }

    }
}