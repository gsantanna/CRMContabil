using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class AlertaVisualizacaoConfiguration : EntityTypeConfiguration<AlertaVisualizacao>
    {
        public AlertaVisualizacaoConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_alertaVisualizacao");
            HasKey(x => x.id_visualizacao);

            Property(x => x.id_usuario).HasColumnName("id_usuario").HasMaxLength(128).IsUnicode(false).IsRequired();
            Property(x => x.id_alerta).HasColumnName("id_alerta");
            Property(x => x.data_visualizacao).HasColumnName("din_visualizacao");

            HasRequired(a => a.Alerta).WithMany(b => b.Visualizacoes).HasForeignKey(c => c.id_alerta).WillCascadeOnDelete(true); //FK -- AlertaVisualizacao --> Alerta 


        }
    }
}