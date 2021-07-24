using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.Reminder
{
    public interface IReminderUseCase
    {
        Task<ResponseOutput> Execute(IList<string> usersId);
    }
}
