using Homuai.App.Services;
using Homuai.App.UseCases.Login.DoLogin;
using Homuai.App.Views.View.Dashboard.NotPartOfHome;
using Homuai.App.Views.View.Dashboard.PartOfHome;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.Login.DoLogin
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly Lazy<ILoginUseCase> useCase;
        private ILoginUseCase _useCase => useCase.Value;
        private readonly UserPreferences _userPreferences;

        public ICommand ButtonLoginCommand { protected set; get; }
        public ICommand ForgotPasswordCommand { protected set; get; }
        public ICommand UsingFigerprintToLoginCommand { protected set; get; }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool CanUseFigerprintToLogin { get; set; }

        public LoginViewModel(Lazy<ILoginUseCase> useCase, UserPreferences userPreferences)
        {
            _userPreferences = userPreferences;
            this.useCase = useCase;

            ForgotPasswordCommand = new Command(async () => await OnForgotPassword());
            ButtonLoginCommand = new Command(async () =>
            {
                await DoLogin(Email, Password);
            });
        }

        private async Task OnForgotPassword()
        {
            try
            {
                await Navigation.PushAsync<ForgotPassword.RequestEmailViewModel>((viewModel, page) => viewModel.Model = new Model.ForgetPasswordModel());
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task DoLogin(string email, string password)
        {
            try
            {
                SendingData();

                var userIsPartOfOneHome = await _useCase.Execute(email, password);

                if (userIsPartOfOneHome)
                    Application.Current.MainPage = new NavigationPage(new UserIsPartOfHomeFlyoutPage());
                else
                    Application.Current.MainPage = new NavigationPage(new UserWithoutPartOfHomePage());

                await Navigation.PopToRootAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        public async Task Initialize()
        {
            CanUseFigerprintToLogin = await _userPreferences.AlreadySignedIn() && await CrossFingerprint.Current.IsAvailableAsync();

            if (CanUseFigerprintToLogin)
            {
                UsingFigerprintToLoginCommand = new Command(async () =>
                {
                    var request = new AuthenticationRequestConfiguration(ResourceText.TITLE_LOGIN_WITH_FINGERPRINT_ACCESS, "");
                    var result = await CrossFingerprint.Current.AuthenticateAsync(request);

                    if (result.Authenticated)
                    {
                        var infoToLogin = await _userPreferences.GetInfoToLogin();
                        await DoLogin(infoToLogin.Email, infoToLogin.Password);
                    }
                });

                UsingFigerprintToLoginCommand.Execute(null);
            }
        }
    }
}
