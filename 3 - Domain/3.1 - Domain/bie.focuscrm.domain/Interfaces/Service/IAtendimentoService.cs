using System.Collections.Generic;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Entities.StoredProcedures;

namespace bie.focuscrm.domain.Interfaces.Service
{
    public interface IAtendimentoService : IServiceBase<Atendimento>
    {
        void AdicionarAnexo(Anexo anexo);
        void RemoverAnexo(Anexo anexo);

        Anexo GetAnexo(int id);


        IEnumerable<AcompanhamentoAtendimentoMacro> GetAcompanhamentoMacro();
        IEnumerable<AcompanhamentoAtendimentoDetalhe> GetAcompanhamentoDetalhe();

    }
}