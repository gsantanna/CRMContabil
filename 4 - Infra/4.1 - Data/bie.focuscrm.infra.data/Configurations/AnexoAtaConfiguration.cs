using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class AnexoAtaConfiguration : EntityTypeConfiguration<Anexo>
    {
        public AnexoAtaConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_ataanexo");
            HasKey(x => x.id_anexo);

            Property(x => x.NomeArquivo).HasMaxLength(1000).IsRequired();
            Property(x => x.Mime).HasMaxLength(40).IsRequired();
            Property(x => x.Conteudo).IsRequired();
            Property(x => x.Tamanho).IsRequired();

            //FK - AnexoATa --> Ata 
            HasRequired(a => a.Atendimento)
                .WithMany(b => b.Anexos)
                .HasForeignKey(c => c.id_atendimento)
                .WillCascadeOnDelete(true);


        }
    }
}