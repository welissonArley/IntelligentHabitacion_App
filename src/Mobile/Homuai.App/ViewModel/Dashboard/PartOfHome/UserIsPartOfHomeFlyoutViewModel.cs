using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.ViewModel.AboutThisProject;
using Homuai.App.ViewModel.ContactUs;
using Homuai.App.ViewModel.Friends;
using Homuai.App.ViewModel.Login.DoLogin;
using Homuai.App.ViewModel.MyFoods;
using Homuai.App.Views.View.Login.DoLogin;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.Dashboard.PartOfHome
{
    public class UserIsPartOfHomeFlyoutViewModel : BaseViewModel
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;

        public ICommand LoggoutCommand { get; }
        public ICommand AddNewItemCommand { get; }
        public ICommand AddNewFriendCommand { get; }
        public ICommand AboutThisProjectCommand { get; }
        public ICommand ContactUsCommand { get; }

        public UserIsPartOfHomeFlyoutViewModel(Lazy<UserPreferences> userPreferences)
        {
            this.userPreferences = userPreferences;

            LoggoutCommand = new Command(async () => { await ClickLogoutAccount(); });
            AddNewItemCommand = new Command(async () => { await OnAddNewItem(); });
            AddNewFriendCommand = new Command(async () => { await AddFriends(); });
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
        private async Task OnAddNewItem()
        {
            try
            {
                await Navigation.PushAsync<AddEditMyFoodsViewModel>((viewModel, _) =>
                {
                    viewModel.Title = ResourceText.TITLE_NEW_ITEM;
                    viewModel.Model = new FoodModel { Quantity = 1.00m };
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task AddFriends()
        {
            try
            {
                await Navigation.PushAsync<AddFriendViewModel>(async (viewModel, _) =>
                {
                    await viewModel.Initialize(null);
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
