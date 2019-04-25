using System;
using System.Collections.Generic;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Entities.StoredProcedures;
using bie.focuscrm.domain.Interfaces.Repository;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.domain.Services
{
    public class AtendimentoService : ServiceBase<Atendimento>, IAtendimentoService
    {
        private readonly IAtendimentoRepository _Repo;
        public AtendimentoService(IAtendimentoRepository Repo)
            : base(Repo)
        {
            _Repo = Repo;
        }

        public void AdicionarAnexo(Anexo anexo)
        {
            _Repo.AdicionarAnexo(anexo);
        }

        public IEnumerable<AcompanhamentoAtendimentoDetalhe> GetAcompanhamentoDetalhe()
        {
            return _Repo.GetAcompanhamentoDetalhe();
        }

        public IEnumerable<AcompanhamentoAtendimentoMacro> GetAcompanhamentoMacro()
        {
            return _Repo.GetAcompanhamentoMacro();
        }

        public Anexo GetAnexo(int id)
        {
            return _Repo.GetAnexo(id);
        }

        public void RemoverAnexo(Anexo anexo)
        {
            _Repo.RemoverAnexo(anexo);
        }
    }
}