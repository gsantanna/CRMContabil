using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class AlertaConfiguration : EntityTypeConfiguration<Alerta>
    {
        public AlertaConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_alerta");
            HasKey(x => x.id_alerta);
            Property(x => x.id_alerta).HasColumnName("id_alerta").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Classificacao).HasColumnName("tp_classificacao").IsRequired();

            Property(x => x.Conteudo).HasColumnName("dsc_conteudo").IsOptional().HasMaxLength(Int32.MaxValue).HasColumnType("text").IsUnicode(false);

            Property(x => x.Titulo).HasColumnName("nom_alerta").IsRequired().HasMaxLength(255).HasColumnType("varchar").IsUnicode(false);

            Property(x => x.Inicio).IsRequired().HasColumnName("din_inicio");

            Property(x => x.Fim).IsRequired().HasColumnName("din_fim");

            Property(x => x.max_exibicoes).HasColumnName("qtd_max_exibicoes");



        }
    }
}