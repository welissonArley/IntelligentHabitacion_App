using Homuai.Communication.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.CreateFirstSchedule
{
    public interface ICreateFirstScheduleUseCase
    {
        Task<ResponseOutput> Execute(IList<RequestUpdateCleaningScheduleJson> request);
    }
}
