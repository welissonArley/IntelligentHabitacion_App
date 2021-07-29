using Homuai.Domain.Dto;
using Homuai.Domain.Services.SendEmail;
using Homuai.EmailHelper.Models;
using Homuai.EmailHelper.Setting;
using System.Threading.Tasks;

namespace Homuai.EmailHelper.Services.SendEmail.EmailsType
{
    public class SendCodeRemoveFriendHelper : SendEmailHelper, ISendCodeRemoveFriendEmail
    {
        private readonly ICustomRazorEngine _customRazorEngine;
        private SendCodeToPerformSomeActionDto _dto;

        public SendCodeRemoveFriendHelper(ICustomRazorEngine customRazorEngine, ISendEmail sendEmail) : base(customRazorEngine, sendEmail)
        {
            _customRazorEngine = customRazorEngine;
        }

        public async Task Send(SendCodeToPerformSomeActionDto dto)
        {
            _dto = dto;

            var resetPasswordModel = GetResetPasswordModel();

            string resetPasswordBody = await _customRazorEngine.RazorViewToHtmlAsync("Areas/MyFeature/Pages/CodeRemoveFriendEmailBody.cshtml", resetPasswordModel);

            await Send(dto.Email, resetPasswordBody);
        }

        private CodeToPermorfActionModel GetResetPasswordModel()
        {
            return new CodeToPermorfActionModel
            {
                Code = _dto.Code,
                UserName = _dto.UserName
            };
        }

        protected override string ContentPlainText()
        {
            return @$"{string.Format(Resources.ResourceText.TITLE_HI_NAME, _dto.UserName)},
                    {Resources.ResourceText.TITLE_TO_REMOVE_FRIEND_USE_CODE_BELOW}\n\n\n
                    {_dto.Code}\n\n\n {Resources.ResourceText.TITLE_BUT_REMEMBER_DONT_LEAVE_FOR_LATER_TEN_MINUTES}\n\n\n";
        }

        protected override string EmailSubject()
        {
            return Resources.ResourceText.SUBJECT_REMOVE_FRIEND;
        }

        protected override EmailBodyModel GetEmailBodyModel()
        {
            return new EmailBodyModel
            {
                Title = Resources.ResourceText.TITLE_CODE_REMOVE_FRIEND,
                IlustrationUrl = "https://i.ibb.co/T0VBVwH/image-4.png"
            };
        }
    }
}
