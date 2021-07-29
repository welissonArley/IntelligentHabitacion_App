namespace Homuai.Domain.ValueObjects
{
    public class EmailContent
    {
        public string Subject { get; set; }
        public string SendToEmail { get; set; }
        public string HtmlContent { get; set; }
        public string PlainTextContent { get; set; }
    }
}
