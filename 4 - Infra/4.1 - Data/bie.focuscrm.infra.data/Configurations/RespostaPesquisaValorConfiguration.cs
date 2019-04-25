using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class RespostaPesquisaValorConfiguration : EntityTypeConfiguration<RespostaPesquisaValor>
    {
        public RespostaPesquisaValorConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_respostapesquisavalor");
            HasKey(a => new {a.id_questao, a.id_respostapesquisa});


            HasRequired(a => a.RespostaPesquisa)
                .WithMany(b => b.Valores)
                .HasForeignKey(c => c.id_respostapesquisa)
                .WillCascadeOnDelete(false);

            HasRequired(a => a.Questao)
                .WithMany(b => b.Respostas)
                .HasForeignKey(c => c.id_questao)
                .WillCascadeOnDelete(false);


            Property(a => a.ValorResposta).IsRequired();

        }
    }
}