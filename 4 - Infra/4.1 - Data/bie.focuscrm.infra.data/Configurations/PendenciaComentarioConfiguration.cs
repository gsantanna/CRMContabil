using System.Data.Entity.ModelConfiguration;
using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.infra.data.Configurations
{
    internal class PendenciaComentarioConfiguration : EntityTypeConfiguration<Comentario>
    {
        public PendenciaComentarioConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".tb_pendenciacomentario");
            HasKey(x => x.id_pendenciacomentario);
            

            Property(x => x.Conteudo).HasMaxLength(2000).IsRequired();
            
            //FK PendenciaComentario --> Pendencia 
            HasRequired(a => a.Pendencia)
                .WithMany(b => b.Comentarios)
                .HasForeignKey(c => c.id_pendencia)
                .WillCascadeOnDelete(true);


            //FK PendenciaComentario --> Usuário 
            HasRequired(a => a.Usuario)
                .WithMany(b => b.Comentarios)
                .HasForeignKey(c => c.id_autor)
                .WillCascadeOnDelete(true);


            

        }

    }
}