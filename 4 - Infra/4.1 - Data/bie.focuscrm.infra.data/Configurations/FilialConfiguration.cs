using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class FilialConfiguration : EntityTypeConfiguration<Filial>
    {
        public FilialConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_filial");

            HasKey(x => x.id_filial);

            Property(x => x.Nome).HasMaxLength(255).IsRequired();

            Property(x => x.CNPJ).HasMaxLength(40).IsRequired();

            Property(x => x.Telefone).HasMaxLength(50);

            Property(x => x.RespostaWS).HasColumnType("text").HasMaxLength(int.MaxValue);

            Property(x => x.OBS).HasColumnType("text").HasMaxLength(int.MaxValue);

            //FK -->  FILIAL --> EMPRESA
            HasRequired(a => a.Empresa)
                .WithMany(b => b.Filiais)
                .HasForeignKey(c => c.id_empresa)
                .WillCascadeOnDelete(true);
            


        }
    }
}