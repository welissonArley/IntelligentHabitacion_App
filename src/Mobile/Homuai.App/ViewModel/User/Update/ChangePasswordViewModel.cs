using Homuai.App.UseCases.User.ChangePassword;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.User.Update
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private Lazy<IChangePasswordUseCase> useCase;
        private IChangePasswordUseCase _useCase => useCase.Value;

        public ICommand ChangePasswordTapped { get; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public ChangePasswordViewModel(Lazy<IChangePasswordUseCase> useCase)
        {
            this.useCase = useCase;
            ChangePasswordTapped = new Command(async () => await ClickChangePasswordAccount());
        }

        private async Task ClickChangePasswordAccount()
        {
            try
            {
                SendingData();

                await _useCase.Execute(CurrentPassword, NewPassword);

                await Sucess();
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
