using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ErrorModal(string message)
        {
            InitializeComponent();
            ErrorMensage.Text = message;
        }

        private void Button_Clicked_Ok(object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}