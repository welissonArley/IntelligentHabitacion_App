using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LgpdModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LgpdModal(string message)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
            Message.Text = message;
        }

        private void OnButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}