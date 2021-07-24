using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.UseCases.Home.HomeInformations;
using Homuai.App.UseCases.Home.RegisterHome.Brazil;
using Homuai.App.UseCases.Home.UpdateHomeInformations;
using Homuai.App.ValueObjects.Enum;
using Plugin.Clipboard;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.Home.Informations
{
    public class HomeInformationViewModel : BaseViewModel
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private readonly Lazy<IRequestCEPUseCase> cepUseCase;
        private readonly Lazy<IHomeInformationsUseCase> informationsUseCase;
        private readonly Lazy<IUpdateHomeInformationsUseCase> updateUseCase;
        private IHomeInformationsUseCase _informationsUseCase => informationsUseCase.Value;
        private IRequestCEPUseCase _cepUseCase => cepUseCase.Value;
        private IUpdateHomeInformationsUseCase _updateUseCase => updateUseCase.Value;
        private UserPreferences _userPreferences => userPreferences.Value;

        public string _currentZipCode;
        public bool IsAdministrator { get; set; }

        public HomeModel Model { get; set; }

        public ICommand CopyWifiPasswordTapped { get; }
        public ICommand UpdateInformationsTapped { get; }
        public ICommand AddRoomTapped { get; }
        public ICommand RemoveRoomTapped { get; }

        public EventHandler<FocusEventArgs> ZipCodeChangedUnfocused { get; set; }

        public HomeInformationViewModel(Lazy<UserPreferences> userPreferences, Lazy<IRequestCEPUseCase> cepUseCase,
            Lazy<IHomeInformationsUseCase> informationsUseCase, Lazy<IUpdateHomeInformationsUseCase> updateUseCase)
        {
            this.userPreferences = userPreferences;
            this.cepUseCase = cepUseCase;
            this.informationsUseCase = informationsUseCase;
            this.updateUseCase = updateUseCase;

            CurrentState = LayoutState.Loading;

            CopyWifiPasswordTapped = new Command(async () => await ClickCopyWifiPassword());
            AddRoomTapped = new Command(async () => await ClickAddRoom());
            RemoveRoomTapped = new Command((room) => ClickRemoveRoom(room.ToString()));
            UpdateInformationsTapped = new Command(async () => await ClickUpdateInformations());
            ZipCodeChangedUnfocused += async (sender, e) =>
            {
                await VerifyZipCode();
            };
        }

        private async Task ClickCopyWifiPassword()
        {
            if (!string.IsNullOrWhiteSpace(Model.NetWork.Password))
            {
                CrossClipboard.Current.SetText(Model.NetWork.Password);
                await ShowQuickInformation(ResourceText.INFORMATION_PASSWORD_COPIED_SUCCESSFULLY);
            }
        }

        private async Task ClickAddRoom()
        {
            try
            {
                await Navigation.PushAsync<InsertRoomViewModel>((viewModel, _) =>
                {
                    viewModel.RoomsSaved = Model.Rooms.Select(c => c.Room).ToList();
                    viewModel.CallbackSelectRoomCommand = new Command((room) =>
                    {
                        Model.Rooms = new ObservableCollection<RoomModel>(Model.Rooms)
                        {
                            new RoomModel
                            {
                                Room = room.ToString()
                            }
                        };

                        OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                    });
                });
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        private void ClickRemoveRoom(string room)
        {
            var model = Model.Rooms.First(c => c.Room.Equals(room));
            Model.Rooms.Remove(model);

            Model.Rooms = new ObservableCollection<RoomModel>(Model.Rooms);

            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
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

        private async Task VerifyZipCode()
        {
            try
            {
                if (Model.City.Country.Id != CountryEnum.BRAZIL || _currentZipCode.Equals(Model.ZipCode))
                    return;

                await ShowLoading();

                var result = await _cepUseCase.Execute(Model.ZipCode);

                _currentZipCode = Model.ZipCode;
                Model.Neighborhood = result.Neighborhood;
                Model.Address = result.Address;
                Model.City.Name = result.City.Name;
                Model.City.StateProvinceName = result.City.StateProvinceName;

                HideLoading();
            }
            catch (System.Exception exeption)
            {
                HideLoading();
                await Exception(exeption);
            }
        }

        public async Task Initialize()
        {
            try
            {
                Model = await _informationsUseCase.Execute();
                IsAdministrator = _userPreferences.IsAdministrator;
                CurrentState = LayoutState.None;
                OnPropertyChanged(new PropertyChangedEventArgs("IsAdministrator"));
                OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }
    }
}
