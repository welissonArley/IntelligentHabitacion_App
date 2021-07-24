using System.Collections.Generic;

namespace Homuai.Communication.Request
{
    public class RequestUpdateHomeJson
    {
        public RequestUpdateHomeJson()
        {
            Rooms = new List<string>();
        }

        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalAddressInfo { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string NetworksName { get; set; }
        public string NetworksPassword { get; set; }
        public IList<string> Rooms { get; set; }
    }
}
