using System.Collections.Generic;

namespace Homuai.Communication.Response
{
    public class ResponseHistoryRoomOfTheDayJson
    {
        public string Room { get; set; }
        public IList<ResponseHistoryCleanDayJson> History { get; set; }
    }
}
