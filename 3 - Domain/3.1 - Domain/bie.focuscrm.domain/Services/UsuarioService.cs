using System;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _Repo;
        public UsuarioService(IUsuarioRepository Repo)
            : base(Repo)
        {
            _Repo = Repo;
        }

        public Usuario GetById(string id)
        {
            return _Repo.GetById(id);
        }
    }
}