using Homuai.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Domain.Repository.CleaningSchedule
{
    public interface ICleaningScheduleReadOnlyRepository
    {
        Task<bool> HomeHasCleaningScheduleCreated(long homeId);
        Task<List<Entity.CleaningSchedule>> GetTasksWithMoreThan8daysWithoutClompleted();
        Task<List<Entity.CleaningSchedule>> GetTasksForTheMonth(DateTime month, long homeId);
        Task<bool> TaskCleanedOnDate(long taskId, DateTime date);
        Task<bool> ThereAreaTaskToUserRateThisMonth(long userId, string room);
        Task<Entity.CleaningSchedule> GetTaskById(long id);
        Task<IList<CleaningScheduleCalendarDayInfoDto>> GetCalendarTasksForMonth(DateTime month, long homeId, string room, long userId);
        Task<IList<CleaningScheduleHistoryRoomOfTheDayDto>> GetHistoryOfTheDay(DateTime date, long homeId, string room, long userId);
        Task<IList<Entity.CleaningSchedule>> GetScheduleRoomForCurrentMonth(long homeId, string room);
        Task<Entity.CleaningTasksCompleted> GetTaskCompletedById(long id);
        Task<bool> UserAlreadyRatedTheTask(long userId, long taskCompletedId);
    }
}
