using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace bie.focuscrm.infra.emailservice
{

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return configSendGridasync(message);
        }


        private async Task configSendGridasync(IdentityMessage message)
        {

            var apiKey = ConfigurationManager.AppSettings["API_SENDGRID_KEY"];
            Email from = new Email(ConfigurationManager.AppSettings["API_SENDGRID_FROM"]);
            Email to = new Email(message.Destination);
            Content content = new Content("text/html", message.Body);
            Mail mail = new Mail(from, message.Subject, to, content);
            dynamic sg = new SendGridAPIClient(apiKey);
            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());


            if (ConfigurationManager.AppSettings["EMAIL_DEBUG"] != null)
            {
                Mail MailDebug = new Mail(from, message.Subject + "["+ message.Destination +"]" , new Email( ConfigurationManager.AppSettings["EMAIL_DEBUG"])  , content);
                await sg.client.mail.send.post(requestBody: MailDebug.Get());
            }


        }


        public async Task SendAsyncRegular(string email, string assunto, string corpo)
        {

            var apiKey = ConfigurationManager.AppSettings["API_SENDGRID_KEY"];
            Email from = new Email(ConfigurationManager.AppSettings["API_SENDGRID_FROM"]);
            Email to = new Email(email);
            Content content = new Content("text/html", corpo);
            Mail mail = new Mail(from, assunto, to, content);
            dynamic sg = new SendGridAPIClient(apiKey);
            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());


            if (ConfigurationManager.AppSettings["EMAIL_DEBUG"] != null)
            {
                Mail MailDebug = new Mail(from, assunto + "[" + email + "]", new Email(ConfigurationManager.AppSettings["EMAIL_DEBUG"]), content);
                await sg.client.mail.send.post(requestBody: MailDebug.Get());
            }

        }

    }



}
