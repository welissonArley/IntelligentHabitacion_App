using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.View.Dashboard.NotPartOfHome
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserWithoutPartOfHomePageFlyout : ContentPage
    {
        public ListView ListView;

        public UserWithoutPartOfHomePageFlyout()
        {
            InitializeComponent();
        }
    }
}