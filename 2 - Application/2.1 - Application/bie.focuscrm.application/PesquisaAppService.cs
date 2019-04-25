using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.application
{
    public class PesquisaAppService : AppServiceBase<Pesquisa>, IPesquisaAppService
    {
        private readonly IPesquisaService _svc;
        public PesquisaAppService(IPesquisaService Svc)
            : base(Svc)
        {
            _svc = Svc;
        }

        public void RemoveQuestao(Pesquisa obj, int id)
        {
            _svc.RemoveQuestao(obj, id);
        }
    }
}