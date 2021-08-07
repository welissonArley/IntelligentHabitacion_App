using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Home.HomeInformation
{
    public interface IHomeInformationUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
