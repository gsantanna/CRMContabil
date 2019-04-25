using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.domain.Interfaces.Service
{
    public interface IPendenciaService : IServiceBase<Pendencia>
    {
        void RemoverComentario(int id_comentario);
        Comentario GetComentario(int id);
    }
}