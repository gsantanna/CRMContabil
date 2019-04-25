using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class AlertaAlcanceConfiguration : EntityTypeConfiguration<AlertaAlcance>
    {
        public AlertaAlcanceConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_alertaalcance");
            HasKey(x => new { x.id_alerta, x.id_usuario });
            Property(x => x.id_alerta).HasColumnName("id_alerta");
            

            HasRequired(a => a.Alerta).WithMany(b => b.Alcances).HasForeignKey(c => c.id_alerta).WillCascadeOnDelete(true); //FK -- AlertaAlcance --> Alerta 

            //HasOptional(a => a.Empresa).WithMany(b => b.AlertasAlcances).HasForeignKey(c => c.id_empresa).WillCascadeOnDelete(true); //FK -- AlertaAlcance --> Cliente 

            HasRequired(b => b.Usuario).WithMany(b => b.AlertasAlcances).HasForeignKey(c => c.id_usuario).WillCascadeOnDelete(true); //FK -- AlertaAlcance --> Usuário





        }
    }
}