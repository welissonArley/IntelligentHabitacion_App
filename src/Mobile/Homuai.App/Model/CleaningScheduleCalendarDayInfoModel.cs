using XLabs.Data;

namespace Homuai.App.Model
{
    public class CleaningScheduleCalendarDayInfoModel : ObservableObject
    {
        public int Day { get; set; }
        public int AmountCleanedRecords { get; set; }
        public int AmountcleanedRecordsToRate { get; set; }
    }
}
