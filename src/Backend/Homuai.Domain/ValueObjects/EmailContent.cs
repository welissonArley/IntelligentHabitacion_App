namespace Homuai.Domain.ValueObjects
{
    public class EmailContent
    {
        public string Subject { get; set; }
        public string PlainText { get; set; }
        public string HtmlText { get; set; }
        public string SendToEmail { get; set; }
    }
}
