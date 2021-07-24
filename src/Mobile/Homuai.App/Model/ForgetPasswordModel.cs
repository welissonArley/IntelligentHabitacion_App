using XLabs.Data;

namespace Homuai.App.Model
{
    public class ForgetPasswordModel : ObservableObject
    {
        public string Email { get; set; }
        public string CodeReceived { get; set; }
        public string NewPassword { get; set; }
    }
}
