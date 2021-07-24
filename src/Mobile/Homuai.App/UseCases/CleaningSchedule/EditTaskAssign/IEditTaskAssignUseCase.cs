using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.EditTaskAssign
{
    public interface IEditTaskAssignUseCase
    {
        Task Execute(List<string> userIds, string room);
    }
}
