using Homuai.App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.CreateFirstSchedule
{
    public interface ICreateFirstScheduleUseCase
    {
        Task<ScheduleTasksCleaningHouseModel> Execute(IList<FriendCreateFirstScheduleModel> usersTasks);
    }
}
