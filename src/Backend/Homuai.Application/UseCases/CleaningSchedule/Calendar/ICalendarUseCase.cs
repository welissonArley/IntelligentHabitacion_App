using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.Calendar
{
    public interface ICalendarUseCase
    {
        Task<ResponseOutput> Execute(RequestCalendarCleaningScheduleJson request);
    }
}
