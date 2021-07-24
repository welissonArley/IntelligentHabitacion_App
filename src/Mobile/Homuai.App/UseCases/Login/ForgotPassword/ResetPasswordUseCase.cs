using Homuai.App.Model;
using Homuai.App.Services.Communication.Login;
using Homuai.App.ValueObjects.Validator;
using Homuai.Communication.Request;
using Homuai.Exception.Exceptions;
using Refit;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Login.ForgotPassword
{
    public class ResetPasswordUseCase : UseCaseBase, IResetPasswordUseCase
    {
        private readonly ILoginRestService _restService;

        public ResetPasswordUseCase() : base("Login")
        {
            _restService = RestService.For<ILoginRestService>(BaseAddress());
        }

        public async Task Execute(ForgetPasswordModel model)
        {
            Validate(model);

            await _restService.ChangePasswordForgotPassword(new RequestResetYourPasswordJson
            {
                Email = model.Email,
                Code = model.CodeReceived,
                Password = model.NewPassword,
            }, GetLanguage());
        }

        private void Validate(ForgetPasswordModel model)
        {
            new EmailValidator().IsValid(model.Email);
            new PasswordValidator().IsValid(model.NewPassword);

            if (string.IsNullOrWhiteSpace(model.CodeReceived))
                throw new CodeEmptyException();
        }
    }
}
