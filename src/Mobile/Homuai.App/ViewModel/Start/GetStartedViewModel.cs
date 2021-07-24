using Homuai.App.ViewModel.Login.DoLogin;
using Homuai.App.ViewModel.User.Register;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.Start
{
    public class GetStartedViewModel : BaseViewModel
    {
        public ICommand LoginCommand { protected set; get; }
        public ICommand RegisterCommand { protected set; get; }

        public GetStartedViewModel()
        {
            LoginCommand = new Command(async () => await OnLogin());
            RegisterCommand = new Command(async () => await OnRegister());
        }

        private async Task OnRegister()
        {
            try
            {
                await Navigation.PushAsync<RequestEmailViewModel>((viewModel, page) => viewModel.Model = new Model.RegisterUserModel());
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnLogin()
        {
            try
            {
                await Navigation.PushAsync<LoginViewModel>(async (viewModel, _) =>
                {
                    await viewModel.Initialize();
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
