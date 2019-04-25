using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;

namespace bie.focuscrm.infra.data.Repository
{
    public class PendenciaRepository : RepositoryBase<Pendencia>, IPendenciaRepository
    {
        public void RemoverComentario(int id_comentario)
        {
            var entidade = Db.Set<Comentario>().Find(id_comentario);
            Db.Set<Comentario>().Remove(entidade);
            Db.SaveChanges();
        }

        public Comentario GetComentario(int id)
        {
            var entidade = Db.Set<Comentario>().Find(id);
            return entidade;
        }


    }
}