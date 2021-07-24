using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Home.RegisterHome
{
    public interface IRegisterHomeUseCase
    {
        Task<ResponseOutput> Execute(RequestRegisterHomeJson registerHomeJson);
    }
}
