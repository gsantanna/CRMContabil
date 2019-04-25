using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class PendenciaConfiguration : EntityTypeConfiguration<Pendencia>
    {
        public PendenciaConfiguration(string schema="dbo")
        {
            ToTable(schema + ".tb_pendencia");
            HasKey(x => x.id_pendencia);


            Property(x => x.Titulo).HasMaxLength(800).IsRequired();

            Property(x => x.Status).HasMaxLength(30).IsRequired();

            Property(x => x.Historico).HasMaxLength(int.MaxValue).HasColumnType("text");


            //FK Pendencia --> Usuário 
            HasRequired(a => a.Responsavel)
                .WithMany(b => b.Pendencias)
                .HasForeignKey(c => c.id_responsavel)
                .WillCascadeOnDelete(false);
            
            //FK Pendencia --> ATA
            HasRequired(a => a.Atendimento)
                .WithMany(b => b.Pendencias)
                .HasForeignKey(c => c.id_atendimento)
                .WillCascadeOnDelete(false);


        }


    }
}