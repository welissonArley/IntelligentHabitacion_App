using Homuai.EmailHelper.Models;
using Homuai.EmailHelper.Setting;
using System.Threading.Tasks;

namespace Homuai.EmailHelper.Services.SendEmail
{
    public abstract class SendEmailHelper
    {
        private readonly ICustomRazorEngine _customRazorEngine;
        private readonly ISendEmail _sendEmail;

        protected SendEmailHelper(ICustomRazorEngine customRazorEngine, ISendEmail sendEmail)
        {
            _customRazorEngine = customRazorEngine;
            _sendEmail = sendEmail;
        }

        protected async Task Send(string sendToEmail, string emailContentBody)
        {
            var emailBodyModel = GetEmailBodyModel();
            string emailBody = await _customRazorEngine.RazorViewToHtmlAsync("Areas/MyFeature/Pages/EmailBodyBase.cshtml", emailBodyModel);

            await _sendEmail.Send(new Domain.ValueObjects.EmailContent
            {
                HtmlContent = emailBody.Replace("{EMAIL_CONTENT_HERE}", emailContentBody),
                SendToEmail = sendToEmail,
                Subject = EmailSubject(),
                PlainTextContent = ContentPlainText()
            });
        }

        protected abstract EmailBodyModel GetEmailBodyModel();
        protected abstract string ContentPlainText();
        protected abstract string EmailSubject();
    }
}
