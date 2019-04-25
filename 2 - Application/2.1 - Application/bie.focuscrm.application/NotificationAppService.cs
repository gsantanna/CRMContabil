
using System;
using System.IO;
using System.Threading.Tasks;
using bie.focuscrm.application.Interfaces;
using bie.focuscrm.domain.Entities;
using bie.focuscrm.domain.Enums;
using bie.focuscrm.infra.emailservice;
using System.Web;

namespace bie.focuscrm.application
{
    public class NotificationAppService : INotificationAppService
    {

        private readonly EmailService _MailSvc;

        public NotificationAppService()
        {
            _MailSvc = new EmailService();
        }

        public async Task<RespostaNotificacao> DispararNotificacaoAsync(Notification objNotific, TipoEntregaNotificacao tipoEntrega = TipoEntregaNotificacao.Email, string nomeTemplate = "base")
        {

            var strCorpo = string.Empty;

            if (tipoEntrega == TipoEntregaNotificacao.EmailTemplate)
            {
                var strCaminhoTemplate =
                    HttpContext.Current.Server.MapPath(@"~/Content/template/" + nomeTemplate + ".html");

                try
                {
                    strCorpo = File.ReadAllText(strCaminhoTemplate, System.Text.Encoding.GetEncoding(1252));

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao carregar o template de nome: " + nomeTemplate + " caminho: " +
                                        strCaminhoTemplate + "  o erro foi: " + ex.Message);
                }

            }
            else if (tipoEntrega == TipoEntregaNotificacao.Email)
            {
                strCorpo = objNotific.Mensagem;
            }
            else
            {
                throw new NotImplementedException();
            }

            //substitui os valores 
            foreach (var valor in objNotific.Valores)
            {
                strCorpo = strCorpo.Replace("{{" + valor.Chave + "}}", valor.Valor);
            }

            foreach (var item in objNotific.Destinatarios)
            {
                await _MailSvc.SendAsyncRegular(item, objNotific.Assunto, strCorpo);
            }

            return RespostaNotificacao.Sucesso;



        }
    }
}
