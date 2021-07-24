using System;
using System.Collections.Generic;

namespace Homuai.Communication.Request
{
    public class RequestRegisterRoomCleaned
    {
        public RequestRegisterRoomCleaned()
        {
            TaskIds = new List<string>();
        }

        public IList<string> TaskIds { get; set; }
        public DateTime Date { get; set; }
    }
}
