using Homuai.Communication.Enums;

namespace Homuai.Communication.Response
{
    public class ResponseTasksJson
    {
        public ResponseTasksJson()
        {
            CreateSchedule = new ResponseCreateScheduleCleaningHouseJson();
            Schedule = new ResponseScheduleTasksCleaningHouseJson();
        }

        public NeedAction Action { get; set; }
        public string Message { get; set; }
        public ResponseCreateScheduleCleaningHouseJson CreateSchedule { get; set; }
        public ResponseScheduleTasksCleaningHouseJson Schedule { get; set; }
    }
}
