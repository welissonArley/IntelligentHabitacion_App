using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingContentView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LoadingContentView()
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;
        }
    }
}