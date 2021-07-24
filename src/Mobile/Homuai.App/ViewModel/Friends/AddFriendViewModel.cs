using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.Views.Modal;
using Homuai.Communication.Response;
using Homuai.Exception;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Homuai.App.ViewModel.Friends
{
    public class AddFriendViewModel : BaseViewModel
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private WebSocketAddFriendConnection _webSocketAddFriendConnection;
        public string CustomState { get; set; }

        private ResponseFriendJson newFriendToAddJson { get; set; }
        public AcceptNewFriendModel Model { get; set; }
        public string Time { get; set; }
        public string QrCode { get; set; }
        public string ProfileColor { get; set; }
        public ICommand CallbackOnFriendAdded { get; set; }

        public ICommand SelectEntryDateTapped { get; }
        public ICommand CancelOperationTapped { get; set; }
        public ICommand ApproveOperationTapped { get; }

        public AddFriendViewModel(Lazy<UserPreferences> userPreferences)
        {
            CurrentState = LayoutState.Loading;
            this.userPreferences = userPreferences;

            CancelOperationTapped = new Command(async () =>
            {
                await OnCancelOperation();
            });
            SelectEntryDateTapped = new Command(async () =>
            {
                await ClickSelectDueDate();
            });
            ApproveOperationTapped = new Command(async () =>
            {
                await OnApproveOperation();
            });
        }

        private async Task OnApproveOperation()
        {
            var navigation = Resolver.Resolve<INavigation>();
            if (Model.MonthlyRent <= 0)
                await navigation.PushPopupAsync(new ErrorModal(ResourceTextException.MONTHLYRENT_INVALID));
            else
            {
                SendingData();
                await _webSocketAddFriendConnection.ApproveFriendCandidate(new Command(async () =>
                {
                    await navigation.PushPopupAsync(new OperationSuccessfullyExecutedModal(ResourceText.TITLE_ACCEPTED));
                    newFriendToAddJson.JoinedOn = Model.EntryDate;
                    CallbackOnFriendAdded?.Execute(newFriendToAddJson);
                    await Task.Delay(1100);
                    await DisconnectFromSocket();
                    await navigation.PopAllPopupAsync();
                    await Navigation.PopAsync();
                }), new Communication.Request.RequestApproveAddFriendJson
                {
                    JoinedOn = Model.EntryDate,
                    MonthlyRent = Model.MonthlyRent
                });
            }
        }

        private void OnCodeWasRead(ResponseFriendJson newFriendToAddJson)
        {
            this.newFriendToAddJson = newFriendToAddJson;

            Model = new AcceptNewFriendModel
            {
                Name = newFriendToAddJson.Name,
                ProfileColorDarkMode = newFriendToAddJson.ProfileColorDarkMode,
                ProfileColorLightMode = newFriendToAddJson.ProfileColorLightMode,
                EntryDate = DateTime.Today.Date,
                MonthlyRent = 0.00m
            };

            CurrentState = LayoutState.Custom;
            CustomState = "AcceptNewFriend";
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
            OnPropertyChanged(new PropertyChangedEventArgs("CustomState"));
        }
        private async Task OnCancelOperation()
        {
            if (CurrentState == LayoutState.Custom)
                await _webSocketAddFriendConnection.DeclinedFriendCandidate();

            await DisconnectFromSocket();
            await Navigation.PopAsync();
        }
        private void OnCodeReceived(string code)
        {
            QrCode = code;
            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("QrCode"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
        private async Task HandleException(string message)
        {
            await Navigation.PopAsync();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new ErrorModal(message));
        }
        private async Task OnChangedTime(int timer)
        {
            if (timer > 0)
            {
                Time = DateTime.Today.AddSeconds(timer).ToString("mm:ss");
                OnPropertyChanged(new PropertyChangedEventArgs("Time"));
            }
            else
            {
                await DisconnectFromSocket();
                await HandleException(ResourceText.TITLE_TIME_EXPIRED_TRY_AGAIN);
            }
        }
        private async Task ClickSelectDueDate()
        {
            await ShowLoading();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new Calendar(Model.EntryDate, OnDateSelected, maximumDate: DateTime.Today));
            HideLoading();
        }
        private Task OnDateSelected(DateTime date)
        {
            Model.EntryDate = date;
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            return Task.CompletedTask;
        }

        public async Task DisconnectFromSocket()
        {
            if (_webSocketAddFriendConnection != null)
                await _webSocketAddFriendConnection.StopConnection().ConfigureAwait(false);

            _webSocketAddFriendConnection = null;
        }

        public async Task Initialize(ICommand callBackFriendAdded)
        {
            CallbackOnFriendAdded = callBackFriendAdded;

            ProfileColor = _userPreferences.ProfileColor();
            OnPropertyChanged(new PropertyChangedEventArgs("ProfileColor"));

            var callbackCodeIsReceived = new Command((code) =>
            {
                OnCodeReceived(code.ToString());
            });
            var callbackCodeWasRead = new Command((newFriendToAddJson) =>
            {
                OnCodeWasRead((ResponseFriendJson)newFriendToAddJson);
            });
            var callbackWhenAnErrorOccurs = new Command(async (message) =>
            {
                await HandleException(message.ToString());
            });
            var callbackTimeChanged = new Command(async (time) =>
            {
                await OnChangedTime((int)time);
            });

            _webSocketAddFriendConnection = new WebSocketAddFriendConnection();
            _webSocketAddFriendConnection.SetCallbacks(callbackWhenAnErrorOccurs, callbackTimeChanged);

            await _webSocketAddFriendConnection.GetQrCodeToAddFriend(callbackCodeIsReceived, callbackCodeWasRead, await _userPreferences.GetToken());
        }
    }
}
