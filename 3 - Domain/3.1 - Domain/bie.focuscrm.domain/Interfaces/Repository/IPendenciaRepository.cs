using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.domain.Interfaces.Repository
{
    public interface IPendenciaRepository : IRepositoryBase<Pendencia>
    {
        void RemoverComentario(int id_comentario);
        Comentario GetComentario(int id);

    }
}