using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.DetailsAllRate
{
    public interface IDetailsAllRateUseCase
    {
        Task<ResponseOutput> Execute(long completedTaskId);
    }
}
