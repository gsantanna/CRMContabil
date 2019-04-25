using System;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Repository;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.domain.Services
{
    public class PendenciaService : ServiceBase<Pendencia>, IPendenciaService
    {
        private readonly IPendenciaRepository _Repo;
        public PendenciaService(IPendenciaRepository Repo)
            : base(Repo)
        {
            _Repo = Repo;
        }

        public Comentario GetComentario(int id)
        {
            return _Repo.GetComentario(id);
        }

        public void RemoverComentario(int id_comentario)
        {
             _Repo.RemoverComentario(id_comentario);
        }
    }
}