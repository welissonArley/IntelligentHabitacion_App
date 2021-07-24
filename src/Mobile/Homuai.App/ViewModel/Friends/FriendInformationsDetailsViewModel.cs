using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.UseCases.Friends.ChangeDateFriendJoinHome;
using Homuai.App.UseCases.Friends.NotifyOrderReceived;
using Homuai.App.ValueObjects.Enum;
using Homuai.App.Views.Modal;
using Homuai.App.Views.Modal.MenuOptions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Homuai.App.ViewModel.Friends
{
    public class FriendInformationsDetailsViewModel : BaseViewModel
    {
        private readonly Lazy<INotifyOrderReceivedUseCase> notifyOrderReceivedUseCase;
        private readonly Lazy<IChangeDateFriendJoinHomeUseCase> changeDateFriendJoinHomeUseCase;
        private INotifyOrderReceivedUseCase _notifyOrderReceivedUseCase => notifyOrderReceivedUseCase.Value;
        private IChangeDateFriendJoinHomeUseCase _changeDateFriendJoinHomeUseCase => changeDateFriendJoinHomeUseCase.Value;

        public ICommand MakePhonecallCommand { get; }

        public ICommand FloatActionCommand { get; }

        public ICommand NotifyFriendOrderHasArrivedCommand { get; }
        private ICommand ChangeDateJoinOnCommand { get; }
        private ICommand RemoveFriendFromHomeCommand { get; }

        public FriendModel Model { get; set; }
        public ICommand RefreshCallback { get; set; }
        public ICommand DeleteFriendCallback { get; set; }

        public FriendInformationsDetailsViewModel(Lazy<INotifyOrderReceivedUseCase> notifyOrderReceivedUseCase,
            Lazy<IChangeDateFriendJoinHomeUseCase> changeDateFriendJoinHomeUseCase)
        {
            this.notifyOrderReceivedUseCase = notifyOrderReceivedUseCase;
            this.changeDateFriendJoinHomeUseCase = changeDateFriendJoinHomeUseCase;

            MakePhonecallCommand = new Command(async (value) =>
            {
                await MakeCall(value.ToString());
            });
            FloatActionCommand = new Command(async () =>
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new FloatActionAdminFriendInformationDetailModal(NotifyFriendOrderHasArrivedCommand, ChangeDateJoinOnCommand, RemoveFriendFromHomeCommand));
            });

            ChangeDateJoinOnCommand = new Command(async () =>
            {
                await ChangeDateOption();
            });
            RemoveFriendFromHomeCommand = new Command(async () =>
            {
                await RemoveFriendFromHome();
            });
            NotifyFriendOrderHasArrivedCommand = new Command(async () =>
            {
                await NotifyFriendOrderHasArrived();
            });
        }

        private async Task MakeCall(string number)
        {
            await ShowLoading();
            PhoneCall.MakeCall(number);
            HideLoading();
        }

        private async Task ChangeDateOption()
        {
            await ShowLoading();
            var navigation = Resolver.Resolve<INavigation>();
            await navigation.PushPopupAsync(new Calendar(Model.JoinedOn, OnDateSelected, maximumDate: DateTime.Today));
            HideLoading();
        }
        private async Task RemoveFriendFromHome()
        {
            try
            {
                await Navigation.PushAsync<RemoveFriendFromHomeViewModel>((viewModel, _) =>
                {
                    viewModel.Initialize(Model, DeleteFriendCallback);
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task OnDateSelected(DateTime date)
        {
            try
            {
                SendingData();
                var friend = await _changeDateFriendJoinHomeUseCase.Execute(Model.Id, date);
                Model.DescriptionDateJoined = friend.DescriptionDateJoined;
                Model.JoinedOn = friend.JoinedOn;
                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                RefreshCallback?.Execute(null);
                await Sucess();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task NotifyFriendOrderHasArrived()
        {
            try
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new ConfirmAction(ResourceText.TITLE_NOTIFY_ORDER_ARRIVED, string.Format(ResourceText.DESCRIPTION_NOTIFY_ORDER_ARRIVED, Model.Name), ModalConfirmActionType.Green,
                    new Command(async () =>
                    {
                        SendingData();
                        await _notifyOrderReceivedUseCase.Execute(Model.Id);
                        await Sucess();
                    })));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        public void Initialize(FriendModel model, ICommand refreshCallback, ICommand deleteFriendCallback)
        {
            Model = model;
            RefreshCallback = refreshCallback;
            DeleteFriendCallback = new Command(async (args) =>
            {
                deleteFriendCallback.Execute(args);
                await Navigation.RemoveAsync(this);
            });
        }
    }
}
