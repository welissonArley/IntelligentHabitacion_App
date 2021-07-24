using Homuai.App.Services.Communication.Login;
using Homuai.App.ValueObjects.Validator;
using Refit;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Login.ForgotPassword
{
    public class RequestCodeResetPasswordUseCase : UseCaseBase, IRequestCodeResetPasswordUseCase
    {
        private readonly ILoginRestService _restService;

        public RequestCodeResetPasswordUseCase() : base("Login")
        {
            _restService = RestService.For<ILoginRestService>(BaseAddress());
        }

        public async Task Execute(string email)
        {
            ValidateEmail(email);
            await _restService.RequestCodeResetPassword(email, GetLanguage());
        }

        private void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }
    }
}
