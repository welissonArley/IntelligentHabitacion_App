using Homuai.App.Model;
using Homuai.App.UseCases.Login.ForgotPassword;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.Login.ForgotPassword
{
    public class RequestEmailViewModel : BaseViewModel
    {
        private readonly Lazy<IRequestCodeResetPasswordUseCase> useCase;
        private IRequestCodeResetPasswordUseCase _useCase => useCase.Value;

        public ICommand RequestCodeCommand { get; }

        public ForgetPasswordModel Model { get; set; }

        public RequestEmailViewModel(Lazy<IRequestCodeResetPasswordUseCase> useCase)
        {
            this.useCase = useCase;
            RequestCodeCommand = new Command(async () => await OnRequestCode());
        }

        private async Task OnRequestCode()
        {
            try
            {
                SendingData();

                await _useCase.Execute(Model.Email);

                await Navigation.PushAsync<ResetPasswordViewModel>((viewModel, page) => viewModel.Model = Model);
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
