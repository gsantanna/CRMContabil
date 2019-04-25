using System;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.domain.Services
{
    public class SetorService : ServiceBase<Setor>, ISetorService
    {
        private readonly ISetorRepository _Repo;
        public SetorService(ISetorRepository Repo)
            : base(Repo)
        {
            _Repo = Repo;
        }
    }
}