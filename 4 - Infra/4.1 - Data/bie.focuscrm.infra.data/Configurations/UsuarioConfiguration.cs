using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_usuario");
            HasKey(c => c.id_usuario);

            Property(x => x.id_usuario).IsRequired();

            Property(x => x.id_empresa).IsOptional();

            Property(x => x.Telefone).IsOptional();
            Property(x => x.Telefone2).IsOptional();

            Property(x => x.Email).HasMaxLength(200).IsOptional();

            //FM --> USUARIO --> EMPERSA 
            HasOptional(a => a.Empresa)
                .WithMany(b => b.Usuarios)
                .HasForeignKey(c => c.id_empresa)
                .WillCascadeOnDelete(false);


            


        }
    }
}