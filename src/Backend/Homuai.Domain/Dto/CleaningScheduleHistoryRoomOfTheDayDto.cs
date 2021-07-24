using System.Collections.Generic;

namespace Homuai.Domain.Dto
{
    public class CleaningScheduleHistoryRoomOfTheDayDto
    {
        public CleaningScheduleHistoryRoomOfTheDayDto()
        {
            History = new List<CleaningScheduleHistoryCleanDayDto>();
        }

        public string Room { get; set; }
        public List<CleaningScheduleHistoryCleanDayDto> History { get; set; }
    }
}
