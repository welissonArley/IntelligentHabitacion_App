using System;
using System.Collections.Generic;

namespace Homuai.Communication.Response
{
    public class ResponseFriendJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<ResponsePhonenumberJson> Phonenumbers { get; set; }
        public List<ResponseEmergencyContactJson> EmergencyContacts { get; set; }
        public string ProfileColorLightMode { get; set; }
        public string ProfileColorDarkMode { get; set; }
        public DateTime JoinedOn { get; set; }
        public string DescriptionDateJoined { get; set; }
    }
}
