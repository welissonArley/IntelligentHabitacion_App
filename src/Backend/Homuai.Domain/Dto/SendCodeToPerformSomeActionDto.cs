namespace Homuai.Domain.Dto
{
    public class SendCodeToPerformSomeActionDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
