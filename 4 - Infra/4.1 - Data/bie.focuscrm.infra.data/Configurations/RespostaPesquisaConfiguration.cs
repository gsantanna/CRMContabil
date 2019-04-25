using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class RespostaPesquisaConfiguration : EntityTypeConfiguration<RespostaPesquisa>
    {
        public RespostaPesquisaConfiguration(string schama = "dbo")
        {
            ToTable(schama + ".tb_respostapesquisa");
            HasKey(x => x.id_respostapesquisa);

            //FK --> RespostaPesquisa --> Usuario 
            HasRequired(a => a.Usuario)
                .WithMany(b => b.Pesquisas)
                .HasForeignKey(c => c.id_usuario)
                .WillCascadeOnDelete(false);

            //FK --> RespostaPesquisa --> Atendimento 
            HasRequired(a => a.Atendimento)
                .WithMany(b => b.RespostasPesquisas)
                .HasForeignKey(c => c.id_atendimento)
                .WillCascadeOnDelete(false);


            //FK --> RespostaPesquisa --> Pesquisa 
            HasRequired(a => a.Pesquisa)
                .WithMany(b => b.Respostas)
                .HasForeignKey(c => c.id_pesquisa)
                .WillCascadeOnDelete(false);

            Property(a => a.DataResposta)
                .IsOptional();

        }

    }
}