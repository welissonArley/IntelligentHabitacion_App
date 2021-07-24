using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Home.RegisterHome.Brazil
{
    public interface IRequestCEPUseCase
    {
        Task<HomeModel> Execute(string cep);
    }
}
