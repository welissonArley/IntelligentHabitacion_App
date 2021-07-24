using Homuai.App.Model;
using Homuai.App.UseCases.CleaningSchedule.DetailsAllRate;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;

namespace Homuai.App.ViewModel.CleaningSchedule
{
    public class DetailsAllRateViewModel : BaseViewModel
    {
        private readonly Lazy<IDetailsAllRateUseCase> useCase;
        private IDetailsAllRateUseCase _useCase => useCase.Value;

        public ObservableCollection<RateTaskModel> Model { get; set; }

        public DetailsAllRateViewModel(Lazy<IDetailsAllRateUseCase> useCase)
        {
            this.useCase = useCase;

            CurrentState = LayoutState.Loading;
        }

        public async Task Initialize(string taskId)
        {
            var list = await _useCase.Execute(taskId);
            Model = new ObservableCollection<RateTaskModel>(list);

            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("Model"));
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
