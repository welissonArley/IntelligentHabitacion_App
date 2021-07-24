using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Home.RegisterHome
{
    public interface IRegisterHomeUseCase
    {
        Task Execute(HomeModel home);
    }
}
