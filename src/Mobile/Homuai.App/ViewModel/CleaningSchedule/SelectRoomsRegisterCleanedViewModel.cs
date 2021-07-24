using Homuai.App.Model;
using Homuai.App.UseCases.CleaningSchedule.RegisterRoomCleaned;
using Homuai.App.Views.Modal;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using XLabs.Ioc;

namespace Homuai.App.ViewModel.CleaningSchedule
{
    public class SelectRoomsRegisterCleanedViewModel : BaseViewModel
    {
        private readonly Lazy<IRegisterRoomCleanedUseCase> registerRoomCleanedUseCase;
        private IRegisterRoomCleanedUseCase _registerRoomCleanedUseCase => registerRoomCleanedUseCase.Value;

        private ICommand CallbackSucessIfSelectedDateIsToday { get; set; }
        public ObservableCollection<SelectOptionModel> Model { get; set; }
        public DateTime CleanedAt { get; set; }

        public ICommand SelectCleanedAtTapped { get; }
        public ICommand ConcludeCommand { get; }

        public SelectRoomsRegisterCleanedViewModel(Lazy<IRegisterRoomCleanedUseCase> registerRoomCleanedUseCase)
        {
            CurrentState = LayoutState.Loading;

            SelectCleanedAtTapped = new Command(async () =>
            {
                await ClickSelectDateCleanedAt();
            });
            ConcludeCommand = new Command(async () =>
            {
                await OnConclude();
            });

            this.registerRoomCleanedUseCase = registerRoomCleanedUseCase;
        }

        private async Task ClickSelectDateCleanedAt()
        {
            await ShowLoading();
            var navigation = Resolver.Resolve<INavigation>();
            var today = DateTime.UtcNow;
            var minimumDate = today.Day <= 4 ? new DateTime(today.Year, today.Month, 1) : today.AddDays(-3);
            await navigation.PushPopupAsync(new Calendar(CleanedAt, OnDateSelected, minimumDate: minimumDate, maximumDate: today));
            HideLoading();
        }
        private Task OnDateSelected(DateTime date)
        {
            CleanedAt = date;
            OnPropertyChanged(new PropertyChangedEventArgs("CleanedAt"));
            return Task.CompletedTask;
        }
        private async Task OnConclude()
        {
            try
            {
                SendingData();

                var assigns = Model.Where(c => c.Assigned).Select(c => c.Id).ToList();

                await _registerRoomCleanedUseCase.Execute(assigns, CleanedAt);

                var today = DateTime.UtcNow.Date;
                if (CleanedAt.Date == today)
                    CallbackSucessIfSelectedDateIsToday.Execute(assigns);

                await Sucess();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        public void Initialize(IList<TaskModel> tasks, ICommand callbackSucessIfSelectedDateIsToday)
        {
            CallbackSucessIfSelectedDateIsToday = callbackSucessIfSelectedDateIsToday;

            Model = new ObservableCollection<SelectOptionModel>(tasks.Select(c => new SelectOptionModel
            {
                Id = c.IdTaskToRegisterRoomCleaning,
                Assigned = false,
                Name = c.Room
            }));
            CleanedAt = DateTime.UtcNow;

            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CleanedAt"));
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
