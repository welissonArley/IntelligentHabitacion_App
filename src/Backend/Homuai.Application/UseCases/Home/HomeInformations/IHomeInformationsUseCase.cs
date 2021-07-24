using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Home.HomeInformations
{
    public interface IHomeInformationsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
