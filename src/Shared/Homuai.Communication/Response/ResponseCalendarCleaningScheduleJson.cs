using System;
using System.Collections.Generic;

namespace Homuai.Communication.Response
{
    public class ResponseCalendarCleaningScheduleJson
    {
        public DateTime Date { get; set; }
        public IList<ResponseCleaningScheduleCalendarDayInfoJson> CleanedDays { get; set; }
    }
}
