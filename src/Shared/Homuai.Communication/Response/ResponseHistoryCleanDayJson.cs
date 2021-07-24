using System;

namespace Homuai.Communication.Response
{
    public class ResponseHistoryCleanDayJson
    {
        public string Id { get; set; }
        public string User { get; set; }
        public int AverageRate { get; set; }
        public bool CanRate { get; set; }
        public DateTime CleanedAt { get; set; }
    }
}
