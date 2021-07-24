using System.Collections.Generic;

namespace Homuai.Communication.Request
{
    public class RequestUpdateCleaningScheduleJson
    {
        public RequestUpdateCleaningScheduleJson()
        {
            Rooms = new List<string>();
        }

        public string UserId { get; set; }
        public IList<string> Rooms { get; set; }
    }
}
