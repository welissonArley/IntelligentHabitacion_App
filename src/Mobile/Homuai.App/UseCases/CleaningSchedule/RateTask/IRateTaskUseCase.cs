using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.RateTask
{
    public interface IRateTaskUseCase
    {
        Task<int> Execute(RateTaskModel model);
    }
}
