using Homuai.App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.CleaningSchedule.DetailsAllRate
{
    public interface IDetailsAllRateUseCase
    {
        Task<IList<RateTaskModel>> Execute(string completedTaskId);
    }
}
