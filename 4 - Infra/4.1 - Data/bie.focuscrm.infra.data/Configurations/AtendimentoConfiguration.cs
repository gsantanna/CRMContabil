using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class AnexoConfiguration : EntityTypeConfiguration<Anexo>
    {
        
    }

    internal class AtendimentoConfiguration : EntityTypeConfiguration<Atendimento>
    {

        public AtendimentoConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_atendimento");
            HasKey(x => x.id_atendimento);
            Property(x => x.Assunto).HasMaxLength(800).IsRequired();


            HasRequired(a => a.Setor)
                .WithMany(b => b.Atendimentos)
                .HasForeignKey(c => c.id_setor)
                .WillCascadeOnDelete(false);


            HasRequired(a => a.Filial)
                .WithMany(b => b.Atendimentos)
                .HasForeignKey(c => c.id_filial)
                .WillCascadeOnDelete(false);

            //FK - ATA --> USUARIO  (AUTOR) 
            HasRequired(a => a.Autor)
                .WithMany(b => b.Atas)
                .HasForeignKey(c => c.id_autor)
                .WillCascadeOnDelete(false);


            //HasOptional(a => a.UsuarioCliente)
            //    .WithMany(b => b.AtendimentosUsuarioCliente)
            //    .HasForeignKey(c => c.id_usuariocliente)
            //    .WillCascadeOnDelete(false);

            //HasRequired(a => a.UsuarioResponsavel)
            //    .WithMany(b => b.Atendimentos)
            //    .HasForeignKey(c => c.id_usuarioresponsavel)
            //    .WillCascadeOnDelete(false);



            #region USUARIOS

            HasMany(a => a.UsuariosCliente).WithMany(b => b.AtendimentosCliente).Map(m =>
            {
                m.MapLeftKey("id_atendimento");
                m.MapRightKey("id_usuario");
                m.ToTable("tb_atendimentousuariocliente");
            });

            HasMany(a => a.UsuariosFocus).WithMany(b => b.AtendimentosFocus).Map(m =>
            {
                m.MapLeftKey("id_atendimento");
                m.MapRightKey("id_usuario");
                m.ToTable("tb_atendimentousuariofocus");
            });


            #endregion



            #region ATA
            Property(x => x.Resumo).HasColumnType("text").HasMaxLength(int.MaxValue);
            Property(x => x.HoraInicio).HasMaxLength(5);
            Property(x => x.HoraFim).HasMaxLength(5);
            Property(x => x.Local).HasMaxLength(100);
            #endregion




        }
    }
}