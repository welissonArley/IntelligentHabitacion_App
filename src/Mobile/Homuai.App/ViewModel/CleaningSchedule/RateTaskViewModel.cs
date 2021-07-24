using Homuai.App.Model;
using Homuai.App.UseCases.CleaningSchedule.RateTask;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.CleaningSchedule
{
    public class RateTaskViewModel : BaseViewModel
    {
        private readonly Lazy<IRateTaskUseCase> useCase;
        private IRateTaskUseCase _useCase => useCase.Value;

        public ICommand CallbackOnConcludeCommand { get; set; }
        public ICommand OnConcludeCommand { get; }
        public RateTaskModel Model { get; set; }

        public RateTaskViewModel(Lazy<IRateTaskUseCase> useCase)
        {
            this.useCase = useCase;

            OnConcludeCommand = new Command(async () => await OnConclude());
        }

        private async Task OnConclude()
        {
            try
            {
                SendingData();

                var averageRating = await _useCase.Execute(Model);

                await Sucess();

                CallbackOnConcludeCommand.Execute(averageRating);

                await Navigation.PopAsync();
            }
            catch (System.Exception exeption)
            {
                await Exception(exeption);
            }
        }

        public void Initialize(DetailsTaskCleanedOnDayModel task, string room, ICommand callbackOnConcludeCommand)
        {
            CallbackOnConcludeCommand = callbackOnConcludeCommand;
            Model = new RateTaskModel
            {
                TaskId = task.Id,
                Feedback = "",
                RatingStars = 0,
                Name = task.User,
                Room = room,
                Date = task.CleanedAt
            };

            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
        }
    }
}
