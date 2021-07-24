using Homuai.App.Model;
using Homuai.Exception.Exceptions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.User.Register
{
    public class RequestNameViewModel : BaseViewModel
    {
        public ICommand NextCommand { get; }

        public RegisterUserModel Model { get; set; }

        public RequestNameViewModel()
        {
            NextCommand = new Command(async () => await OnNext());
        }

        private async Task OnNext()
        {
            try
            {
                ValidateName(Model.Name);

                await Navigation.PushAsync<RequestPhoneNumberViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NameEmptyException();
        }
    }
}
