using System;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Interfaces.Service;

namespace bie.focuscrm.application
{
    public class PendenciaAppService : AppServiceBase<Pendencia>, IPendenciaAppService
    {
        private readonly IPendenciaService _svc;
        public PendenciaAppService(IPendenciaService Svc)
            : base(Svc)
        {
            _svc = Svc;
        }

        public Comentario GetComentario(int id)
        {
            return _svc.GetComentario(id);
        }

        public void RemoverComentario(int id_comentario)
        {
            _svc.RemoverComentario(id_comentario);
        }
    }
}