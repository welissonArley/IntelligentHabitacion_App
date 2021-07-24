using XLabs.Data;

namespace Homuai.App.Model
{
    public class EmergencyContactModel : ObservableObject
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Relationship { get; set; }
    }
}
