using System.ComponentModel;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class CityModel : ObservableObject
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }
        private string _stateProvinceName;
        public string StateProvinceName
        {
            get
            {
                return _stateProvinceName;
            }
            set
            {
                _stateProvinceName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StateProvinceName"));
            }
        }
        public CountryModel Country { get; set; }
    }
}
