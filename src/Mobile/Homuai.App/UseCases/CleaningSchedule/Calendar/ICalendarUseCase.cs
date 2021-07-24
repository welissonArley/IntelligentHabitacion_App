using Homuai.App.Model;
using System;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.Calendar
{
    public interface ICalendarUseCase
    {
        Task<CleaningScheduleCalendarModel> Execute(DateTime month, string room = null);
    }
}
