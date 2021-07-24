using Homuai.App.Services;
using Homuai.App.ViewModel.Login.DoLogin;
using Homuai.App.ViewModel.Start;
using Homuai.App.Views.View.Dashboard.NotPartOfHome;
using Homuai.App.Views.View.Dashboard.PartOfHome;
using Homuai.App.Views.View.Login.DoLogin;
using Plugin.Fingerprint;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;

namespace Homuai.App.Views.View.Start
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InitializePage : ContentPage
    {
        public InitializePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SetMainPage();
        }

        private async Task SetMainPage()
        {
            var userPreferences = Resolver.Resolve<UserPreferences>();
            if (await userPreferences.HasAccessToken())
            {
                if (userPreferences.IsPartOfOneHome)
                    Application.Current.MainPage = new NavigationPage(new UserIsPartOfHomeFlyoutPage());
                else
                    Application.Current.MainPage = new NavigationPage(new UserWithoutPartOfHomePage());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage((Page)ViewFactory.CreatePage<GetStartedViewModel, GetStartedPage>());
                if (await userPreferences.AlreadySignedIn() && await CrossFingerprint.Current.IsAvailableAsync())
                {
                    await Application.Current.MainPage.Navigation.PushAsync((Page)ViewFactory.CreatePage<LoginViewModel, LoginPage>(async (viewModel, page) =>
                    {
                        await viewModel.Initialize();
                    }));
                }
            }
        }
    }
}