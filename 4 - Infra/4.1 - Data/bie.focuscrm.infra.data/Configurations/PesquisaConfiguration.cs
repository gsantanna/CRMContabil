using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class PesquisaConfiguration : EntityTypeConfiguration<Pesquisa>
    {
        public PesquisaConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_pesquisa");
            HasKey(x => x.id_pesquisa);
            Property(x => x.Descricao).HasColumnType("text");
            Property(x => x.Titulo).HasMaxLength(100).IsRequired();

        }
    }
}