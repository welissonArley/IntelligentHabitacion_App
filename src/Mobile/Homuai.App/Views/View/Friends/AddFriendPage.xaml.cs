using Homuai.App.ViewModel.Friends;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Homuai.App.Views.View.Friends
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFriendPage : ContentPage
    {
        public AddFriendPage()
        {
            InitializeComponent();

            BarcodeView.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
            BarcodeView.BarcodeOptions.Width = 170;
            BarcodeView.BarcodeOptions.Height = 170;
            BarcodeView.BarcodeOptions.Margin = 0;
        }

        protected override bool OnBackButtonPressed()
        {
            var binding = (AddFriendViewModel)BindingContext;
            var command = new Command(async () =>
            {
                await binding.DisconnectFromSocket();
            });
            command.Execute(null);
            
            return base.OnBackButtonPressed();
        }
    }
}