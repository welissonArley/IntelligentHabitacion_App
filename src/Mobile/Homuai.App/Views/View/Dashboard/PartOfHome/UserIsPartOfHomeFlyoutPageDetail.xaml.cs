using Homuai.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.View.Dashboard.PartOfHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserIsPartOfHomeFlyoutPageDetail : ContentPage
    {
        public UserIsPartOfHomeFlyoutPageDetail()
        {
            InitializeComponent();

            SetGridDefinitions();

            HeaderOrderHasArrived.ButtonClickedCommand = new Command(UserGotOrder);
        }

        protected override void OnAppearing()
        {
            RefreshHeader();
            base.OnAppearing();
        }

        private void SetGridDefinitions()
        {
            var cardHeight = ((HomuaiDevice.Width() / 2) - 35) * 1.27;

            GridCards.RowDefinitions.Clear();
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
            GridCards.RowDefinitions.Add(new RowDefinition { Height = cardHeight });
        }

        private void UserGotOrder()
        {
            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();
            userPreferences.UserHasOrder(false);
            HeaderOrderHasArrived.IsVisible = false;
            HeaderGirlReading.IsVisible = true;
        }

        public void RefreshHeader()
        {
            HeaderGirlReading.FillInformations();

            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();
            if (userPreferences.HasOrder)
            {
                HeaderOrderHasArrived.IsVisible = true;
                HeaderGirlReading.IsVisible = false;
            }
            else
            {
                HeaderOrderHasArrived.IsVisible = false;
                HeaderGirlReading.IsVisible = true;
            }
        }
    }
}