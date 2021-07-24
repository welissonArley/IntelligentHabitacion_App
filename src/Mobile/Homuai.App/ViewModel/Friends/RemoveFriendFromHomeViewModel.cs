using Homuai.App.Model;
using Homuai.App.UseCases.Friends.RemoveFriend;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.Friends
{
    public class RemoveFriendFromHomeViewModel : BaseViewModel
    {
        public string CustomState { get; set; }

        private readonly Lazy<IRemoveFriendUseCase> removeFriendUseCase;
        private readonly Lazy<IRequestCodeToRemoveFriendUseCase> requestCodeToRemoveFriendUseCase;
        private IRemoveFriendUseCase _removeFriendUseCase => removeFriendUseCase.Value;
        private IRequestCodeToRemoveFriendUseCase _requestCodeToRemoveFriendUseCase => requestCodeToRemoveFriendUseCase.Value;

        public FriendModel Model { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }

        public ICommand CancelCommand { get; }
        public ICommand RequestCodeCommand { get; }
        public ICommand ConfirmRemoveFriendCommand { get; }

        public ICommand FunctionCallbackCommand { get; set; }

        public RemoveFriendFromHomeViewModel(Lazy<IRemoveFriendUseCase> removeFriendUseCase,
            Lazy<IRequestCodeToRemoveFriendUseCase> requestCodeToRemoveFriendUseCase)
        {
            this.removeFriendUseCase = removeFriendUseCase;
            this.requestCodeToRemoveFriendUseCase = requestCodeToRemoveFriendUseCase;

            CancelCommand = new Command(async () => await OnCancel());
            RequestCodeCommand = new Command(async () => await OnRequestCode());
            ConfirmRemoveFriendCommand = new Command(async () => await ConfirmRemoveFriend());
        }
        private async Task OnRequestCode()
        {
            try
            {
                SendingData();
                await _requestCodeToRemoveFriendUseCase.Execute();
                CurrentState = Xamarin.CommunityToolkit.UI.Views.LayoutState.Custom;
                CustomState = "ConfirmAction";
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
                OnPropertyChanged(new PropertyChangedEventArgs("CustomState"));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task ConfirmRemoveFriend()
        {
            try
            {
                SendingData();
                await _removeFriendUseCase.Execute(Model.Id, Code, Password);

                FunctionCallbackCommand?.Execute(Model.Id);
                await Sucess();
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnCancel()
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        public void Initialize(FriendModel model, ICommand deleteFriendCallback)
        {
            Model = model;
            FunctionCallbackCommand = deleteFriendCallback;
        }
    }
}
