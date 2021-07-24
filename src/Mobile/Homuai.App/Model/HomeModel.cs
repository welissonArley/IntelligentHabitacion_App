using Homuai.App.ValueObjects.Enum;
using System.Collections.ObjectModel;
using System.ComponentModel;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class HomeModel : ObservableObject
    {
        public HomeModel()
        {
            City = new CityModel();
            NetWork = new WifiNetworkModel();
        }
        public string ZipCode { get; set; }
        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Address"));
            }
        }
        public string Number { get; set; }
        public string AdditionalAddressInfo { get; set; }
        private string _neighborhood;
        public string Neighborhood
        {
            get
            {
                return _neighborhood;
            }
            set
            {
                _neighborhood = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Neighborhood"));
            }
        }
        public short? DeadlinePaymentRent { get; set; }
        public WifiNetworkModel NetWork { get; set; }
        public CityModel City { get; set; }
        public ObservableCollection<RoomModel> Rooms { get; set; }

        public bool IsBrazil()
        {
            return City.Country.Id == CountryEnum.BRAZIL;
        }
    }
}
