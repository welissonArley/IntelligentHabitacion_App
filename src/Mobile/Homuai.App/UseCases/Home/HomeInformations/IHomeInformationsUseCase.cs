using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Home.HomeInformations
{
    public interface IHomeInformationsUseCase
    {
        Task<HomeModel> Execute();
    }
}
