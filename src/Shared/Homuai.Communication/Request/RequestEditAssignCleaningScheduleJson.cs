using System.Collections.Generic;

namespace Homuai.Communication.Request
{
    public class RequestEditAssignCleaningScheduleJson
    {
        public string Room { get; set; }
        public IList<string> UserIds { get; set; }
    }
}
