using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OperationErrorModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public OperationErrorModal(string message)
        {
            InitializeComponent();

            LabelText.Text = message;
            CloseWhenBackgroundIsClicked = false;
        }
    }
}