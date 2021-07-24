using Homuai.App.Model;
using Homuai.App.UseCases.User.EmailAlreadyBeenRegistered;
using Homuai.App.Views.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Homuai.App.ViewModel.User.Register
{
    public class RequestEmailViewModel : BaseViewModel
    {
        private readonly Lazy<IEmailAlreadyBeenRegisteredUseCase> useCase;
        private IEmailAlreadyBeenRegisteredUseCase _useCase => useCase.Value;

        public ICommand NextCommand { get; }
        public ICommand WhyINeedFillThisInformationCommand { get; }

        public RegisterUserModel Model { get; set; }

        public RequestEmailViewModel(Lazy<IEmailAlreadyBeenRegisteredUseCase> useCase)
        {
            this.useCase = useCase;

            NextCommand = new Command(async () => await OnNext());
            WhyINeedFillThisInformationCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new LgpdModal(ResourceText.DESCRIPTION_WHY_WE_NEED_YOUR_EMAIL));
            });
        }

        private async Task OnNext()
        {
            try
            {
                SendingData();

                await _useCase.Execute(Model.Email);

                await Navigation.PushAsync<RequestNameViewModel>((viewModel, page) => viewModel.Model = Model);

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
