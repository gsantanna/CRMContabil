using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class SetorConfiguration : EntityTypeConfiguration<Setor>
    {

        public SetorConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_setor");
            HasKey(x => x.id_setor);

            Property(x => x.Nome).IsRequired();

            Property(x => x.id_usuarioresponsavel).IsOptional();

            //FK Setor --> Usuario Responsável -- Optional
            HasOptional(a => a.UsuarioResponsavel)
                .WithMany(b => b.ResponsavelSetores)
                .HasForeignKey(c => c.id_usuarioresponsavel)
                .WillCascadeOnDelete(false);



            HasMany(a => a.Funcionarios).WithMany(b => b.Setores).Map(m =>
            {
                m.MapLeftKey("id_setor");
                m.MapRightKey("id_usuario");
                m.ToTable("tb_setorfuncionario");
            });


        }

    }
}