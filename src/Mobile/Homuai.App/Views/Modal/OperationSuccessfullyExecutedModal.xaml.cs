using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OperationSuccessfullyExecutedModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public OperationSuccessfullyExecutedModal(string message)
        {
            InitializeComponent();

            LabelText.Text = message;
            CloseWhenBackgroundIsClicked = false;
        }
    }
}