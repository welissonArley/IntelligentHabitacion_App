using Homuai.Communication.Enums;
using System.Collections.Generic;

namespace Homuai.Communication.Response
{
    public class ResponseHomeInformationsJson
    {
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalAddressInfo { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public Country Country { get; set; }
        public ResponseWifiNetworkJson NetWork { get; set; }
        public IList<ResponseRoomJson> Rooms { get; set; }
    }
}
