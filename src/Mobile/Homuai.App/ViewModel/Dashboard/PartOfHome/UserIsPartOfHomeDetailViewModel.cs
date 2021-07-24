using Homuai.App.ViewModel.CleaningSchedule;
using Homuai.App.ViewModel.Friends;
using Homuai.App.ViewModel.Home.Informations;
using Homuai.App.ViewModel.MyFoods;
using Homuai.App.ViewModel.User.Update;
using Homuai.App.Views.View.CleaningSchedule;
using Homuai.App.Views.View.Friends;
using Homuai.App.Views.View.Home.Informations;
using Homuai.App.Views.View.MyFoods;
using Homuai.App.Views.View.User.Update;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace Homuai.App.ViewModel.Dashboard.PartOfHome
{
    public class UserIsPartOfHomeDetailViewModel
    {
        private readonly INavigation Navigation;

        public ICommand CardMyInformationTapped { get; }
        public ICommand CardHomesInformationsTapped { get; }
        public ICommand CardMyFriendsTapped { get; }
        public ICommand CardMyFoodsTapped { get; }
        public ICommand CardCleanHouseTapped { get; }

        public UserIsPartOfHomeDetailViewModel(INavigation navigation)
        {
            Navigation = navigation;

            CardMyInformationTapped = new Command(async () => await ClickOnCardMyInformations());
            CardHomesInformationsTapped = new Command(async () => await ClickOnCardHomesInformations());
            CardMyFriendsTapped = new Command(async () => await ClickOnCardMyFriends());
            CardMyFoodsTapped = new Command(async () => await ClickOnCardMyFoods());
            CardCleanHouseTapped = new Command(async () => await ClickOnCardCleanHouse());
        }

        private async Task ClickOnCardMyInformations()
        {
            var page = (Page)ViewFactory.CreatePage<UserInformationViewModel, UserInformationPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
        private async Task ClickOnCardHomesInformations()
        {
            var page = (Page)ViewFactory.CreatePage<HomeInformationViewModel, HomeInformationPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
        private async Task ClickOnCardMyFriends()
        {
            var page = (Page)ViewFactory.CreatePage<MyFriendsViewModel, MyFriendsPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
        private async Task ClickOnCardMyFoods()
        {
            var page = (Page)ViewFactory.CreatePage<MyFoodsViewModel, MyFoodsPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
        private async Task ClickOnCardCleanHouse()
        {
            var page = (Page)ViewFactory.CreatePage<TasksViewModel, TasksPage>(async (viewModel, _) =>
            {
                await viewModel.Initialize();
            });

            await Navigation.PushAsync(page);
        }
    }
}
