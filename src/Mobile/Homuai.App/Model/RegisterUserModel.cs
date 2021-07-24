using XLabs.Data;

namespace Homuai.App.Model
{
    public class RegisterUserModel : ObservableObject
    {
        public RegisterUserModel()
        {
            EmergencyContact1 = new EmergencyContactModel();
            EmergencyContact2 = new EmergencyContactModel();
        }

        public string Name { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public EmergencyContactModel EmergencyContact1 { get; set; }
        public EmergencyContactModel EmergencyContact2 { get; set; }
        public string Password { get; set; }
    }
}
