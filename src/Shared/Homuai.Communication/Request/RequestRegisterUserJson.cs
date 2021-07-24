using System.Collections.Generic;

namespace Homuai.Communication.Request
{
    public class RequestRegisterUserJson
    {
        public RequestRegisterUserJson()
        {
            EmergencyContacts = new List<RequestEmergencyContactJson>();
            Phonenumbers = new List<string>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Phonenumbers { get; set; }
        public List<RequestEmergencyContactJson> EmergencyContacts { get; set; }
        public string Password { get; set; }
        public string PushNotificationId { get; set; }
    }
}
