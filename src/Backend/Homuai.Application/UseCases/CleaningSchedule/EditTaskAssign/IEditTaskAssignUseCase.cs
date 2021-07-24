using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.EditTaskAssign
{
    public interface IEditTaskAssignUseCase
    {
        Task<ResponseOutput> Execute(RequestEditAssignCleaningScheduleJson request);
    }
}
