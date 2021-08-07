using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Home.UpdateHomeInformation
{
    public interface IUpdateHomeInformationUseCase
    {
        Task<ResponseOutput> Execute(RequestUpdateHomeJson updateHomeJson);
    }
}
