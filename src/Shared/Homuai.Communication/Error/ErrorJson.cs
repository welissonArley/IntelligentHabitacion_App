using System.Collections.Generic;

namespace Homuai.Communication.Error
{
    public enum ErrorCode
    {
        Error = 0,
        TokenExpired = 1
    }

    public class ErrorJson
    {
        public ErrorCode ErrorCode { get; set; } = ErrorCode.Error;
        public List<string> Errors { get; set; }

        public ErrorJson() { /* Use only for JsonConvert.DeserializeObject<ErrorJson> */ }
        public ErrorJson(string message)
        {
            Errors = new List<string> { message };
        }
        public ErrorJson(List<string> messages)
        {
            Errors = messages;
        }
    }
}
