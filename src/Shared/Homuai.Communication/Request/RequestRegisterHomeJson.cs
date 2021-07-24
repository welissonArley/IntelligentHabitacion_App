using Homuai.Communication.Enums;

namespace Homuai.Communication.Request
{
    public class RequestRegisterHomeJson
    {
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalAddressInfo { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public Country Country { get; set; }
    }
}
