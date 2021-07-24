using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.RateTask
{
    public interface IRateTaskUseCase
    {
        Task<ResponseOutput> Execute(long taskCompletedId, RequestRateTaskJson request);
    }
}
