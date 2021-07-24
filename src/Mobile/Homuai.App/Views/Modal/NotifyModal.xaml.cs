using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotifyModal : Rg.Plugins.Popup.Pages.PopupPage
    {
        public NotifyModal(string title, string message)
        {
            InitializeComponent();
            LabelTitle.Text = title;
            LabelMessage.Text = message;
            CloseWhenBackgroundIsClicked = false;
        }

        private void OnButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}