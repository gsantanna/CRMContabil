using System;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.application
{
    public class UsuarioAppService : AppServiceBase<Usuario>, IUsuarioAppService
    {
        private readonly IUsuarioService _svc;
        public UsuarioAppService(IUsuarioService Svc)
            : base(Svc)
        {
            _svc = Svc;
        }

        public Usuario GetById(string id)
        {
            return _svc.GetById(id);
        }
    }
}