using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class QuestaoPesquisaConfiguration : EntityTypeConfiguration<QuestaoPesquisa>
    {
        public QuestaoPesquisaConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_questaopesquisa");
            HasKey(x => x.id_questaopesquisa);

            Property(x => x.Descricao).HasMaxLength(300).IsRequired();

            HasRequired(a => a.Pesquisa)
                .WithMany(b => b.Questoes)
                .HasForeignKey(c => c.id_pesquisa)
                .WillCascadeOnDelete(true);




        }
    }
}