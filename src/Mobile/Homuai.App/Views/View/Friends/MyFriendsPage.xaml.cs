using Homuai.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.View.Friends
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFriendsPage : ContentPage
    {
        public MyFriendsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();
            if (!userPreferences.IsAdministrator)
                ButtonAddFriend.IsVisible = false;

            base.OnAppearing();
        }
    }
}