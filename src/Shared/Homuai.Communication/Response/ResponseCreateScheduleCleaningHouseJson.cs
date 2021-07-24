using System.Collections.Generic;

namespace Homuai.Communication.Response
{
    public class ResponseCreateScheduleCleaningHouseJson
    {
        public ResponseCreateScheduleCleaningHouseJson()
        {
            Rooms = new List<ResponseRoomJson>();
            Friends = new List<ResponseUserSimplifiedJson>();
        }

        public IList<ResponseRoomJson> Rooms { get; set; }
        public IList<ResponseUserSimplifiedJson> Friends { get; set; }
    }
}
