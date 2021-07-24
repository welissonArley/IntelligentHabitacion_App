using System;
using System.Collections.Generic;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class CleaningScheduleCalendarModel : ObservableObject
    {
        public DateTime Date { get; set; }
        public IList<CleaningScheduleCalendarDayInfoModel> CleanedDays { get; set; }
    }
}
