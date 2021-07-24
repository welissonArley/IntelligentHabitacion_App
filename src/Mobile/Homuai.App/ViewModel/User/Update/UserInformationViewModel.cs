using Homuai.App.Model;
using Homuai.App.UseCases.User.UpdateUserInformations;
using Homuai.App.UseCases.User.UserInformations;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.User.Update
{
    public class UserInformationViewModel : BaseViewModel
    {
        private Lazy<IUserInformationsUseCase> getInformationsUseCase;
        private IUserInformationsUseCase _useCase => getInformationsUseCase.Value;
        private Lazy<IUpdateUserInformationsUseCase> updateUseCase;
        private IUpdateUserInformationsUseCase _updateUseCase => updateUseCase.Value;

        public ICommand DeleteAccountTapped { get; }
        public ICommand ChangePasswordTapped { get; }
        public ICommand UpdateInformationsTapped { get; }

        public UserInformationsModel Model { get; set; }

        public UserInformationViewModel(Lazy<IUserInformationsUseCase> getInformationsUseCase,
            Lazy<IUpdateUserInformationsUseCase> updateUseCase)
        {
            CurrentState = LayoutState.Loading;

            this.getInformationsUseCase = getInformationsUseCase;
            this.updateUseCase = updateUseCase;

            ChangePasswordTapped = new Command(async () => await ClickChangePasswordAccount());
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());
        }

        private async Task ClickChangePasswordAccount()
        {
            try
            {
                await Navigation.PushAsync<ChangePasswordViewModel>();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task ClickUpdateInformations()
        {
            try
            {
                SendingData();

                await _updateUseCase.Execute(Model);

                await Sucess();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        public async Task Initialize()
        {
            try
            {
                Model = await _useCase.Execute();
                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                CurrentState = LayoutState.None;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
