using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickInformationModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public QuickInformationModal(string message)
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = false;
            MessageLabel.Text = message;
        }
    }
}