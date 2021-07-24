using Homuai.App.Model;
using Homuai.App.Views.Modal;
using Homuai.Exception.Exceptions;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Homuai.App.ViewModel.User.Register
{
    public class RequestPhoneNumberViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }
        public ICommand WhyINeedFillThisInformationCommand { get; }

        public RegisterUserModel Model { get; set; }

        public RequestPhoneNumberViewModel()
        {
            NextCommand = new Command(async () => await OnNext());
            WhyINeedFillThisInformationCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new LgpdModal(ResourceText.DESCRIPTION_WHY_WE_NEED_YOUR_TELEPHONE_NUMBER));
            });
        }

        private async Task OnNext()
        {
            try
            {
                ValidatePhoneNumber();

                await Navigation.PushAsync<RequestEmergencyContact1ViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private void ValidatePhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(Model.PhoneNumber1))
                throw new PhoneNumberEmptyException();
        }
    }
}
