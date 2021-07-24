using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.HistoryOfTheDay
{
    public interface IHistoryOfTheDayUseCase
    {
        Task<ResponseOutput> Execute(RequestHistoryOfTheDayJson request);
    }
}
