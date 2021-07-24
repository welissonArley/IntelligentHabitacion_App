namespace Homuai.Communication.Request
{
    public class RequestResetYourPasswordJson
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
