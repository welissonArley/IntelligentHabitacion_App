using Homuai.App.Services;
using Homuai.App.Services.Communication.Login;
using Homuai.App.ValueObjects.Dtos;
using Homuai.App.ValueObjects.Validator;
using Homuai.Communication.Request;
using Homuai.Exception.Exceptions;
using Refit;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Homuai.App.UseCases.Login.DoLogin
{
    public class LoginUseCase : UseCaseBase, ILoginUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly ILoginRestService _restService;

        public LoginUseCase(Lazy<UserPreferences> userPreferences) : base("Login")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<ILoginRestService>(BaseAddress());
        }

        public async Task<bool> Execute(string email, string password)
        {
            Validate(email, password);

            var response = await _restService.DoLogin(new RequestLoginJson
            {
                Password = password,
                User = email
            }, GetLanguage());

            ResponseValidate(response);

            await _userPreferences.SaveInitialUserInfos(new UserPreferenceDto
            {
                Email = email,
                Token = GetTokenOnHeaderRequest(response.Headers),
                Password = password,
                Name = response.Content.Name,
                IsAdministrator = response.Content.IsAdministrator,
                ProfileColorDarkMode = response.Content.ProfileColorDarkMode,
                ProfileColorLightMode = response.Content.ProfileColorLightMode,
                IsPartOfOneHome = response.Content.IsPartOfOneHome,
                Width = Application.Current.MainPage.Width,
                Id = response.Content.Id
            });

            return response.Content.IsPartOfOneHome;
        }

        private void Validate(string email, string password)
        {
            new EmailValidator().IsValid(email);

            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();
        }
    }
}
