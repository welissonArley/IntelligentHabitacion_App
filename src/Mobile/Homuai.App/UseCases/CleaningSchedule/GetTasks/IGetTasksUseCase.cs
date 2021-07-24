using Homuai.App.Model;
using System;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.GetTasks
{
    public interface IGetTasksUseCase
    {
        Task<ScheduleCleaningHouseModel> Execute(DateTime date);
    }
}
