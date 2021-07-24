using Homuai.App.Model;
using Homuai.App.ValueObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Homuai.App.ViewModel.Home.Register
{
    public class SelectCountryViewModel : BaseViewModel
    {
        private IList<CountryModel> _listCountry { get; set; }
        public ObservableCollection<CountryModel> CountryList { get; set; }

        public ICommand SearchTextChangedCommand { get; }
        public ICommand ItemSelectedCommand { get; }

        public SelectCountryViewModel()
        {
            CurrentState = LayoutState.Loading;

            SearchTextChangedCommand = new Command((value) =>
            {
                OnSearchTextChanged((string)value);
            });

            ItemSelectedCommand = new Command(async (value) =>
            {
                await Navigation.PushAsync<RegisterHomeViewModel>((viewModel, _) => viewModel.Model = new HomeModel
                {
                    City = new CityModel
                    {
                        Country = (CountryModel)value
                    }
                });
            });
        }

        private void OnSearchTextChanged(string value)
        {
            CountryList = new ObservableCollection<CountryModel>(_listCountry.Where(c => c.Name.ToUpper().Contains(value.ToUpper())).ToList());

            OnPropertyChanged(new PropertyChangedEventArgs("CountryList"));
        }

        public void Initialize()
        {
            _listCountry = new CoutriesAvaliables().Get();
            CountryList = new ObservableCollection<CountryModel>(_listCountry);
            OnPropertyChanged(new PropertyChangedEventArgs("CountryList"));
            CurrentState = LayoutState.None;
            OnPropertyChanged(new PropertyChangedEventArgs("CurrentState"));
        }
    }
}
