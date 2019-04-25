using System;
using System.Collections.Generic;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Service;
using bie.focuscrm.domain.Entities.StoredProcedures;

namespace bie.focuscrm.application
{
    public class AtendimentoAppService : AppServiceBase<Atendimento>, IAtendimentoAppService
    {
        private readonly IAtendimentoService _svc;
        public AtendimentoAppService(IAtendimentoService Svc)
            : base(Svc)
        {
            _svc = Svc;
        }

        public void AdicionarAnexo(Anexo anexo)
        {
            _svc.AdicionarAnexo(anexo);
        }

        public IEnumerable<AcompanhamentoAtendimentoDetalhe> GetAcompanhamentoDetalhe()
        {
            return _svc.GetAcompanhamentoDetalhe();
        }

        public IEnumerable<AcompanhamentoAtendimentoMacro> GetAcompanhamentoMacro()
        {
            return _svc.GetAcompanhamentoMacro();
        }

        public Anexo GetAnexo(int id)
        {
            return _svc.GetAnexo(id);
        }

        public void RemoverAnexo(Anexo anexo)
        {
            _svc.RemoverAnexo(anexo);
        }
    }
}