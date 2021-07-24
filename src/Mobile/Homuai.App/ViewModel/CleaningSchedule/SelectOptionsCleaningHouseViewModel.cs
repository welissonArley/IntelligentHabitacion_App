using Homuai.App.Model;
using Homuai.App.ValueObjects.Dtos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.CleaningSchedule
{
    public class SelectOptionsCleaningHouseViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Phrase { get; set; }
        public string SubTitle { get; set; }
        public ICommand ConcludeCommand { get; }
        public ICommand CallbackOnConclude { set; get; }
        public ObservableCollection<SelectOptionModel> Options { get; set; }

        public SelectOptionsCleaningHouseViewModel()
        {
            ConcludeCommand = new Command(async () =>
            {
                CallbackOnConclude.Execute(Options.Where(c => c.Assigned).ToList());
                await Navigation.PopAsync();
            });
        }

        public void Initialize(SelectOptionsObject optionsObject)
        {
            Title = optionsObject.Title;
            Phrase = optionsObject.Phrase;
            SubTitle = optionsObject.SubTitle;
            CallbackOnConclude = optionsObject.CallbackOnConclude;
            Options = new ObservableCollection<SelectOptionModel>(optionsObject.Options);

            OnPropertyChanged(new PropertyChangedEventArgs("Title"));
            OnPropertyChanged(new PropertyChangedEventArgs("Phrase"));
            OnPropertyChanged(new PropertyChangedEventArgs("SubTitle"));
            OnPropertyChanged(new PropertyChangedEventArgs("CallbackOnConclude"));
            OnPropertyChanged(new PropertyChangedEventArgs("Options"));
        }
    }
}
