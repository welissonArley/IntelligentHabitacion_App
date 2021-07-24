using Homuai.App.Services;
using Homuai.App.ViewModel.AboutThisProject;
using Homuai.App.ViewModel.ContactUs;
using Homuai.App.ViewModel.Home.Register;
using Homuai.App.ViewModel.Login.DoLogin;
using Homuai.App.Views.View.Login.DoLogin;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.Dashboard.NotPartOfHome
{
    public class UserWithoutPartOfHomeFlyoutViewModel : BaseViewModel
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;

        public ICommand LoggoutCommand { get; }
        public ICommand CreateHomeCommand { get; }
        public ICommand AboutThisProjectCommand { get; }
        public ICommand ContactUsCommand { get; }

        public UserWithoutPartOfHomeFlyoutViewModel(Lazy<UserPreferences> userPreferences)
        {
            this.userPreferences = userPreferences;

            LoggoutCommand = new Command(async () => { await ClickLogoutAccount(); });
            CreateHomeCommand = new Command(async () => { await OnCreateHome(); });
            ContactUsCommand = new Command(async () => { await ContactUs(); });
            AboutThisProjectCommand = new Command(async () => { await AboutThisProject(); });
        }

        private async Task ClickLogoutAccount()
        {
            try
            {
                _userPreferences.Logout();
                Application.Current.MainPage = new NavigationPage((Page)XLabs.Forms.Mvvm.ViewFactory.CreatePage<LoginViewModel, LoginPage>(async (viewModel, _) =>
                {
                    await viewModel.Initialize();
                }));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnCreateHome()
        {
            try
            {
                await Navigation.PushAsync<SelectCountryViewModel>((viewModel, _) =>
                {
                    viewModel.Initialize();
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task ContactUs()
        {
            try
            {
                await Navigation.PushAsync<ContactUsViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task AboutThisProject()
        {
            try
            {
                await Navigation.PushAsync<ProjectInformationViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
