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
    public class RequestEmergencyContact1ViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }
        public ICommand WhyINeedFillThisInformationCommand { get; }

        public RegisterUserModel Model { get; set; }

        public RequestEmergencyContact1ViewModel()
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
                ValidateEmergencyContact(Model.EmergencyContact1.Name, Model.EmergencyContact1.PhoneNumber, Model.EmergencyContact1.Relationship);

                await Navigation.PushAsync<RequestEmergencyContact2ViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private void ValidateEmergencyContact(string name, string phoneNumber, string relationship)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NameEmptyException();

            if (string.IsNullOrWhiteSpace(relationship))
                throw new RelationshipToEmptyException();

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new PhoneNumberEmptyException();
        }
    }
}
