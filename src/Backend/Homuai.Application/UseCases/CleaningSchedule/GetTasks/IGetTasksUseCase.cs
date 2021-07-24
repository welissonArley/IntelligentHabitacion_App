using System;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.GetTasks
{
    public interface IGetTasksUseCase
    {
        Task<ResponseOutput> Execute(DateTime date);
    }
}
