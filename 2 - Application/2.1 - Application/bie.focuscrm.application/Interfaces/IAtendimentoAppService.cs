using System.Collections.Generic;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Entities.StoredProcedures;

namespace bie.focuscrm.application.Interfaces
{
    public interface IAtendimentoAppService : IAppServiceBase<Atendimento>
    {
        void AdicionarAnexo(Anexo anexo);
        void RemoverAnexo(Anexo anexo);
        Anexo GetAnexo(int id);

        IEnumerable<AcompanhamentoAtendimentoMacro> GetAcompanhamentoMacro();
        IEnumerable<AcompanhamentoAtendimentoDetalhe> GetAcompanhamentoDetalhe();
    }
}