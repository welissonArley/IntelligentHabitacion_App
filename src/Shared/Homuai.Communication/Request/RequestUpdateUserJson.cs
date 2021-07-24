using System.Collections.Generic;

namespace Homuai.Communication.Request
{
    public class RequestUpdateUserJson
    {
        public RequestUpdateUserJson()
        {
            EmergencyContacts = new List<RequestEmergencyContactJson>();
            Phonenumbers = new List<string>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Phonenumbers { get; set; }
        public List<RequestEmergencyContactJson> EmergencyContacts { get; set; }
    }
}
