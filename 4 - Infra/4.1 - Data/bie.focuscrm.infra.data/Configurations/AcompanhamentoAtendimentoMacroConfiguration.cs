using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities.StoredProcedures;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class AcompanhamentoAtendimentoMacroConfiguration : 
        EntityTypeConfiguration<AcompanhamentoAtendimentoMacro>
    {
        public AcompanhamentoAtendimentoMacroConfiguration()
        {         
            MapToStoredProcedures();
            Ignore(x => x.filiais);
        }

    }

    internal class AcompanhamentoAtendimentoDetalheConfiguration :
        EntityTypeConfiguration<AcompanhamentoAtendimentoDetalhe>
    {
        public AcompanhamentoAtendimentoDetalheConfiguration()
        {
            MapToStoredProcedures();
        }
    }

}