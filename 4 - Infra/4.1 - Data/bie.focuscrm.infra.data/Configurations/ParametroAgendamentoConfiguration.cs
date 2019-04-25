using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class ParametroAgendamentoConfiguration : EntityTypeConfiguration<ParametroAgendamento>
    {

        public ParametroAgendamentoConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_parametroagendamento");

            //PK (id_empresa + id_setor) 
            HasKey(x => new { x.id_empresa, x.id_setor });

            Property(x => x.Intervalo).IsRequired();

            //FK --> Parametro Agendamento --> Empresa 
            HasRequired(a => a.Empresa)
                .WithMany(b => b.ParametrosAgendamento)
                .HasForeignKey(c => c.id_empresa)
                .WillCascadeOnDelete(true);

            //FK --> Parametro Agendamento --> Setor 
            HasRequired(a => a.Setor)
                .WithMany(b => b.ParametrosAgendamento)
                .HasForeignKey(c => c.id_setor)
                .WillCascadeOnDelete(true);

        }


    }
}