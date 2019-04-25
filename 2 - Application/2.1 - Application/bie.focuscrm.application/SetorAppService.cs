using System;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.application
{
    public class SetorAppService : AppServiceBase<Setor>, ISetorAppService
    {
        private readonly ISetorService _svc;
        public SetorAppService(ISetorService Svc)
            : base(Svc)
        {
            _svc = Svc;
        }
    }
}