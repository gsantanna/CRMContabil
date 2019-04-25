using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class EmpresaConfiguration : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_empresa");
            HasKey(x => x.id_empresa);

            Property(x => x.Nome).HasMaxLength(300).IsRequired();

            Property(x => x.CNPJ).HasMaxLength(40).IsRequired();

            Property(x => x.Socio).HasMaxLength(200);

            Property(x => x.TelefoneSocio).HasMaxLength(50);

            Property(x => x.EmailSocio).HasMaxLength(100);

            Property(x => x.Site).HasMaxLength(300);

            Property(x => x.Atividade).HasMaxLength(400);

            Property(x => x.Bancos).HasMaxLength(60);

            Property(x => x.Endividamento).HasMaxLength(100);

            Property(x => x.AcoesFiscais).HasMaxLength(500);

            Property(x => x.RegimeTributacao).HasMaxLength(100);

            Property(x => x.SistemaRetaguarda).HasMaxLength(100);

            Property(x => x.DiaRetiradaDocumentos).HasMaxLength(30);

            Property(x => x.RespostaWS).HasColumnType("text").HasMaxLength(int.MaxValue);

            Property(x => x.Grupo).HasMaxLength(255).IsRequired();

            Property(x => x.CEP).HasMaxLength(15);



        }

    }
}