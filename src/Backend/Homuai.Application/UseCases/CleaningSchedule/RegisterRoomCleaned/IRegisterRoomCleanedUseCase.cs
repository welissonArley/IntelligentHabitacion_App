using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.CleaningSchedule.RegisterRoomCleaned
{
    public interface IRegisterRoomCleanedUseCase
    {
        Task<ResponseOutput> Execute(RequestRegisterRoomCleaned request);
    }
}
