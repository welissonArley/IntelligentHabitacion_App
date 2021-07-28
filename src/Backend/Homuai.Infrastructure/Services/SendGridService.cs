using Homuai.Domain.ValueObjects;
using Homuai.EmailHelper.Services.SendEmail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Homuai.Infrastructure.Services
{
    public class SendGridService : ISendEmail
    {
        private readonly SendGridClient _client;
        private readonly EmailAddress _from;

        private readonly EmailConfig _emailConfig;

        public SendGridService(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
            _client = new SendGridClient(emailConfig.ApiKey);
            _from = new EmailAddress(emailConfig.Email, emailConfig.Name);
        }

        public async Task Send(EmailContent content)
        {
            var to = new EmailAddress(content.SendToEmail, content.Subject);
            var msg = MailHelper.CreateSingleEmail(_from, to, content.Subject, content.PlainTextContent, content.HtmlContent);

            await _client.SendEmailAsync(msg);
        }

        public async Task SendMessageSupport(EmailContent content)
        {
            var to = new EmailAddress(_emailConfig.SupportEmail, content.Subject);
            var msg = MailHelper.CreateSingleEmail(_from, to, content.Subject, content.HtmlContent, content.HtmlContent);

            await _client.SendEmailAsync(msg);
        }
    }
}
