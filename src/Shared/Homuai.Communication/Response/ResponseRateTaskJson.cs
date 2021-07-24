using System;

namespace Homuai.Communication.Response
{
    public class ResponseRateTaskJson
    {
        public string Room { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Feedback { get; set; }
    }
}
