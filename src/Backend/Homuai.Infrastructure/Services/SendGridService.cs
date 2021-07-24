using Homuai.Domain.Services;
using Homuai.Domain.ValueObjects;
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

        private string EmailHeader()
        {
            return @"<div style=""background-color: #FEBF3B; height: 63px;"">
					<ul style=""list-style-type: none;float: left;margin-top: 0px;margin-bottom: 0px;padding-left: 0px;"">
						<li>
							<div style=""height: 100%; width: 100%; margin: 0 auto; display: flex; justify-content: center; align-items: center; overflow: auto;"">
								<img alt=""Icone"" src=""https://66.media.tumblr.com/201a6b70ca50f294561e749843b0ab4b/9baf459185561f04-bf/s1280x1920/273808597d4185e2f208648e3c632b1529eb4867.png"" width=""60px"" height=""60px"">
							</div>
						</li>
					</ul>
			
					<div style=""display: inline-grid;"">
						<ul style='list-style-type: none;'>
							<li>
								<div style=""height: 100%; width: 100%; margin: 0 auto; display: flex; justify-content: center; align-items: center; overflow: auto;"">
									<span style=""font-family: 'Raleway';font-size: 16px;"">Porque morar com os amigos</span>
								</div>
							</li>
							<li>
								<div style=""height: 100%; width: 100%; margin: 0 auto; display: flex; justify-content: center; align-items: center; overflow: auto;"">
									<span style=""font-family: 'Raleway';font-size: 12px;font-weight: 300;text-align: center;"">deve ser divertido e organizado</span>
								</div>
							</li>
						</ul>
					</div>
				</div>";
        }

        public async Task Send(EmailContent content)
        {
            var to = new EmailAddress(content.SendToEmail, content.Subject);
            var plainTextContent = content.PlainText;
            var htmlContent = $"{EmailHeader()}{content.HtmlText}";
            var msg = MailHelper.CreateSingleEmail(_from, to, content.Subject, plainTextContent, htmlContent);

            await _client.SendEmailAsync(msg);
        }
        public async Task SendMessageSupport(EmailContent content)
        {
            var to = new EmailAddress(_emailConfig.SupportEmail, content.Subject);
            var plainTextContent = content.PlainText;
            var htmlContent = content.HtmlText;
            var msg = MailHelper.CreateSingleEmail(_from, to, content.Subject, plainTextContent, htmlContent);

            await _client.SendEmailAsync(msg);
        }
    }
}
