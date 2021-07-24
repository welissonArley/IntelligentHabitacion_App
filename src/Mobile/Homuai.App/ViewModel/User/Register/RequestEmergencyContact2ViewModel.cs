using Homuai.App.Model;
using Homuai.App.Views.Modal;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Homuai.App.ViewModel.User.Register
{
    public class RequestEmergencyContact2ViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }
        public ICommand WhyINeedFillThisInformationCommand { get; }

        public RegisterUserModel Model { get; set; }

        public RequestEmergencyContact2ViewModel()
        {
            NextCommand = new Command(async () => await OnNext());
            WhyINeedFillThisInformationCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new LgpdModal(ResourceText.DESCRIPTION_WHY_WE_NEED_YOUR_EMERGENCY_CONTACT));
            });
        }

        private async Task OnNext()
        {
            try
            {
                await Navigation.PushAsync<RequestPasswordViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
