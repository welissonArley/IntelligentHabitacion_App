using Homuai.App.ValueObjects.Enum;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.Modal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmAction : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly ICommand _callbackConfirm;
        private readonly ICommand _callbackCancel;

        public ConfirmAction(string title, string text, ModalConfirmActionType type, ICommand callbackConfirm, ICommand callbackCancel = null)
        {
            InitializeComponent();

            _callbackConfirm = callbackConfirm;
            _callbackCancel = callbackCancel;

            LabelTitle.Text = title;
            LabelText.Text = text;

            switch (type)
            {
                case ModalConfirmActionType.Green:
                    {
                        LabelTitle.TextColor = (Color)Application.Current.Resources["GreenDefault"];
                        ButtonOk.BackgroundColor = (Color)Application.Current.Resources["YellowDefault"];
                        ImageIcon.Source = ImageSource.FromFile("IconCheck");
                    }
                    break;
                case ModalConfirmActionType.Red:
                    {
                        LabelTitle.TextColor = (Color)Application.Current.Resources["RedDefault"];
                        ButtonOk.BackgroundColor = (Color)Application.Current.Resources["RedDefault"];
                    }
                    break;
            }
        }

        private async void Button_Cancel(object sender, EventArgs e)
        {
            _callbackCancel?.Execute(null);
            await Navigation.PopPopupAsync();
        }
        private async void Button_Ok(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
            _callbackConfirm.Execute(null);
        }
    }
}