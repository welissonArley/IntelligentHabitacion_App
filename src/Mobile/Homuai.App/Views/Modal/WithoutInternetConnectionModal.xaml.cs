using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WithoutInternetConnectionModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public WithoutInternetConnectionModal()
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;
        }
    }
}