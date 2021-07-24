using Homuai.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Templates.Header
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderWithGirlReading : ContentView
    {
        public HeaderWithGirlReading()
        {
            InitializeComponent();

            var deviceWidth = HomuaiDevice.Width();

            ImageGirlReading.WidthRequest = deviceWidth;
            ImageGirlReading.HeightRequest = deviceWidth * 0.49;

            FillInformations();
        }

        public void FillInformations()
        {
            var userPreferences = XLabs.Ioc.Resolver.Resolve<UserPreferences>();

            LabelUserName.Text = userPreferences.Name;
            ImageKingCrown.IsVisible = userPreferences.IsAdministrator;
        }
    }
}