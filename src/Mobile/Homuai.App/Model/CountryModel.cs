using Homuai.App.ValueObjects.Enum;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class CountryModel : ObservableObject
    {
        public CountryEnum Id { get; set; }
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public string Flag { get; set; }
    }
}
