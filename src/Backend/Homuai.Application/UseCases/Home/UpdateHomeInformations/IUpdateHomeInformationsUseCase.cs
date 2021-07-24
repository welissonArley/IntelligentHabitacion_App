using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Home.UpdateHomeInformations
{
    public interface IUpdateHomeInformationsUseCase
    {
        Task<ResponseOutput> Execute(RequestUpdateHomeJson updateHomeJson);
    }
}
