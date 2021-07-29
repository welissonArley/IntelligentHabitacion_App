using Homuai.Domain.Dto;
using Homuai.Domain.Services.SendEmail;
using Homuai.EmailHelper.Models;
using Homuai.EmailHelper.Setting;
using System.Threading.Tasks;

namespace Homuai.EmailHelper.Services.SendEmail.EmailsType
{
    public class SendContactUsHelper : ISendContactUsEmail
    {
        private readonly ICustomRazorEngine _customRazorEngine;
        private readonly ISendEmail _sendEmail;

        public SendContactUsHelper(ICustomRazorEngine customRazorEngine, ISendEmail sendEmail)
        {
            _customRazorEngine = customRazorEngine;
            _sendEmail = sendEmail;
        }

        public async Task Send(ContactUsDto dto)
        {
            var emailBodyModel = GetEmailBodyModel(dto);
            string emailBody = await _customRazorEngine.RazorViewToHtmlAsync("Areas/MyFeature/Pages/ContactUsEmailBody.cshtml", emailBodyModel);

            await _sendEmail.SendMessageSupport(new Domain.ValueObjects.EmailContent
            {
                HtmlContent = emailBody,
                Subject = "User Contact",
                PlainTextContent = ContentPlainText(dto)
            });
        }

        private string ContentPlainText(ContactUsDto dto)
        {
            return @$"O usuário {dto.UserName} com o e-mail ({dto.Email}) enviou a seguinte mensagem:\n\n\n
                {dto.Message}\n\n\n";
        }

        private ContactUsModel GetEmailBodyModel(ContactUsDto dto)
        {
            return new ContactUsModel
            {
                Email = dto.Email,
                Message = dto.Message,
                UserName = dto.UserName
            };
        }
    }
}
