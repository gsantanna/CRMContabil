
using System.Threading.Tasks;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Enums;

namespace bie.focuscrm.application.Interfaces
{
    public interface INotificationAppService
    {
        //disparar notificações
        Task<RespostaNotificacao> DispararNotificacaoAsync(Notification objNotific, TipoEntregaNotificacao tipoEntrega = TipoEntregaNotificacao.Email, string nomeTemplate = "base");



    }
}