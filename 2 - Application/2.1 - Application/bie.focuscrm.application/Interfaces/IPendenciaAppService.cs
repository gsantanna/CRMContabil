using bie.focuscrm.domain.Entities;

namespace bie.focuscrm.application.Interfaces
{
    public interface IPendenciaAppService : IAppServiceBase<Pendencia>
    {
        void RemoverComentario(int id_comentario);
        Comentario GetComentario(int id);
    }
}