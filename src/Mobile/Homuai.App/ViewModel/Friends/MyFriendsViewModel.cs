using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.UseCases.Friends.GetMyFriends;
using Homuai.App.Views.Modal;
using Homuai.App.Views.Templates.Information;
using Homuai.Communication.Response;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Homuai.App.ViewModel.Friends
{
    public class MyFriendsViewModel : BaseViewModel
    {
        private readonly Lazy<IGetMyFriendsUseCase> getMyFriendsUseCase;
        private IGetMyFriendsUseCase _getMyFriendsUseCase => getMyFriendsUseCase.Value;

        private MyFriendsComponent componentToEdit { get; set; }

        public ICommand SearchTextChangedCommand { protected set; get; }
        public ICommand MakePhonecallCommand { protected set; get; }
        public ICommand DetailFriendCommand { protected set; get; }
        public ICommand AddFriendCommand { protected set; get; }

        private ObservableCollection<FriendModel> _friendsList { get; set; }
        public ObservableCollection<FriendModel> FriendsList { get; set; }

        public MyFriendsViewModel(Lazy<IGetMyFriendsUseCase> getMyFriendsUseCase)
        {
            this.getMyFriendsUseCase = getMyFriendsUseCase;

            CurrentState = LayoutState.Loading;

            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });
            MakePhonecallCommand = new Command(async (value) =>
            {
                await MakePhonecall((FriendModel)value);
            });
            DetailFriendCommand = new Command(async (value) =>
            {
                componentToEdit = (MyFriendsComponent)value;
                await OnDetailFriend(componentToEdit.Friend);
            });
            AddFriendCommand = new Command(async () =>
            {
                await OnAddFriend();
            });
        }

        private void OnSearchTextChanged(string value)
        {
            FriendsList = new ObservableCollection<FriendModel>(_friendsList.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
        }
        private async Task MakePhonecall(FriendModel friend)
        {
            if (friend.Phonenumbers.Count == 1)
                await MakeCall(friend.Phonenumbers.First());
            else
            {
                var navigation = Resolver.Resolve<INavigation>();
                await navigation.PushPopupAsync(new ChoosePhonenumberModal(friend.Name, friend.Phonenumbers.ElementAt(0), friend.Phonenumbers.ElementAt(1), friend.ProfileColor, MakeCall));
            }
        }
        private async Task OnDetailFriend(FriendModel friend)
        {
            try
            {
                await Navigation.PushAsync<FriendInformationsDetailsViewModel>((viewModel, _) =>
                {
                    viewModel.Initialize(friend, new Command(RefreshList), new Command((friendId) =>
                    {
                        FriendRemoved(friendId.ToString());
                    }));
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
        private async Task OnAddFriend()
        {
            try
            {
                await Navigation.PushAsync<AddFriendViewModel>(async (viewModel, _) =>
                {
                    await viewModel.Initialize(new Command((friendModel) =>
                    {
                        CallbackFriendAdded((ResponseFriendJson)friendModel);
                    }));
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private async Task MakeCall(string number)
        {
            await ShowLoading();
            PhoneCall.MakeCall(number);
            HideLoading();
        }

        private void RefreshList()
        {
            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
            componentToEdit.Refresh();
        }

        private void FriendRemoved(string id)
        {
            var friend = FriendsList.First(c => c.Id.Equals(id));
            FriendsList.Remove(friend);
            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
            OnPropertyChanged(new PropertyChangedEventArgs("FriendsListIsEmpty"));
        }

        private void CallbackFriendAdded(ResponseFriendJson json)
        {
            var model = new FriendModel
            {
                Id = json.Id,
                JoinedOn = json.JoinedOn,
                Name = json.Name,
                ProfileColorLightMode = json.ProfileColorLightMode,
                ProfileColorDarkMode = json.ProfileColorDarkMode,
                Phonenumbers = json.Phonenumbers.Select(w => w.Number).ToList(),
                EmergencyContacts = json.EmergencyContacts.Select(w => new EmergencyContactModel
                {
                    Name = w.Name,
                    Relationship = w.Relationship,
                    PhoneNumber = w.Phonenumber
                }).ToList()
            };
            _friendsList.Add(model);
            FriendsList.Add(model);
            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }

        public async Task Initialize()
        {
            try
            {
                _friendsList = new ObservableCollection<FriendModel>(await _getMyFriendsUseCase.Execute());
                FriendsList = new ObservableCollection<FriendModel>(_friendsList);
                OnPropertyChanged(new PropertyChangedEventArgs("FriendsList"));
                CurrentState = _friendsList.Any() ? LayoutState.None : LayoutState.Empty;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
