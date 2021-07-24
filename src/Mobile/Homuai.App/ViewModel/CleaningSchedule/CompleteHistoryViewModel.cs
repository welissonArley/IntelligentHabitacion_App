using Homuai.App.Model;
using Homuai.App.UseCases.CleaningSchedule.Calendar;
using Homuai.App.UseCases.CleaningSchedule.HistoryOfTheDay;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.CleaningSchedule
{
    public class CompleteHistoryViewModel : BaseViewModel
    {
        public LayoutState CurrentStateCalendar { get; set; }
        public LayoutState CurrentStateHistoric { get; set; }

        public CleaningScheduleCalendarModel Model { get; set; }
        public ObservableCollection<DetailsTaskCleanedOnDayModelGroup> DetailsDayModel { get; set; }

        private readonly Lazy<IHistoryOfTheDayUseCase> historyOfTheDayUseCase;
        private readonly Lazy<ICalendarUseCase> useCase;
        private ICalendarUseCase _useCase => useCase.Value;
        private IHistoryOfTheDayUseCase _historyOfTheDayUseCase => historyOfTheDayUseCase.Value;

        public ICommand OnDateChangedCommand { get; }
        public ICommand OnDaySelectedCommand { get; }
        public ICommand OnRateTaskTappedCommand { get; }
        public ICommand OnSeeDetailsRateTappedCommand { get; }

        public CompleteHistoryViewModel(Lazy<ICalendarUseCase> useCase, Lazy<IHistoryOfTheDayUseCase> historyOfTheDayUseCase)
        {
            CurrentStateHistoric = LayoutState.Loading;
            CurrentStateCalendar = LayoutState.Loading;

            this.useCase = useCase;
            this.historyOfTheDayUseCase = historyOfTheDayUseCase;

            OnDateChangedCommand = new Command(async (dateReturned) =>
            {
                await DateChanged((DateTime)dateReturned);
            });
            OnDaySelectedCommand = new Command(async (dateReturned) =>
            {
                await FillTaskDayDetailsModel((DateTime)dateReturned);
            });
            OnRateTaskTappedCommand = new Command(async (taskToRate) =>
            {
                await RateTask((DetailsTaskCleanedOnDayModel)taskToRate);
            });
            OnSeeDetailsRateTappedCommand = new Command(async (taskToRate) =>
            {
                var taskToRateModel = (DetailsTaskCleanedOnDayModel)taskToRate;

                await Navigation.PushAsync<DetailsAllRateViewModel>(async (viewModel, _) =>
                {
                    await viewModel.Initialize(taskToRateModel.Id);
                });
            });
        }

        private async Task DateChanged(DateTime date)
        {
            CurrentStateHistoric = LayoutState.Loading;
            CurrentStateCalendar = LayoutState.Loading;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));

            await FillCalendarModel(date);
            await FillTaskDayDetailsModel(date);
        }
        private async Task RateTask(DetailsTaskCleanedOnDayModel taskToRateModel)
        {
            var groupModel = this.DetailsDayModel.First(c => c.Any(w => w.Id.Equals(taskToRateModel.Id)));

            await Navigation.PushAsync<RateTaskViewModel>((viewModel, _) =>
            {
                viewModel.Initialize(taskToRateModel, groupModel.Room, new Command((newAverageRate) =>
                {
                    CurrentStateCalendar = LayoutState.Loading;
                    CurrentStateHistoric = LayoutState.Loading;
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));

                    taskToRateModel.CanRate = false;
                    taskToRateModel.AverageRate = (int)newAverageRate;
                    Model.Date = taskToRateModel.CleanedAt;
                    Model.CleanedDays.First(c => c.Day == taskToRateModel.CleanedAt.Day).AmountcleanedRecordsToRate--;

                    CurrentStateHistoric = LayoutState.None;
                    CurrentStateCalendar = LayoutState.None;
                    OnPropertyChanged(new PropertyChangedEventArgs("DetailsDayModel"));
                    OnPropertyChanged(new PropertyChangedEventArgs("Model"));
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
                    OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));
                }));
            });
        }

        private async Task FillCalendarModel(DateTime date)
        {
            if (CurrentStateCalendar != LayoutState.Loading)
            {
                CurrentStateCalendar = LayoutState.Loading;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));
            }

            Model = await _useCase.Execute(date);

            CurrentStateCalendar = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateCalendar"));
        }
        private async Task FillTaskDayDetailsModel(DateTime date)
        {
            if (CurrentStateHistoric != LayoutState.Loading)
            {
                CurrentStateHistoric = LayoutState.Loading;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
            }

            var result = await _historyOfTheDayUseCase.Execute(date);
            DetailsDayModel = new ObservableCollection<DetailsTaskCleanedOnDayModelGroup>(result);

            CurrentStateHistoric = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentStateHistoric"));
        }

        public async Task Initialize()
        {
            await FillCalendarModel(DateTime.UtcNow);
            await FillTaskDayDetailsModel(DateTime.UtcNow);
        }
    }
}
