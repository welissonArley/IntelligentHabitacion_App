using System;

namespace Homuai.Domain.Dto
{
    public class CleaningScheduleHistoryCleanDayDto
    {
        public long Id { get; set; }
        public string User { get; set; }
        public int AverageRate { get; set; }
        public bool CanRate { get; set; }
        public DateTime CleanedAt { get; set; }
    }
}
