using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Home.UpdateHomeInformations
{
    public interface IUpdateHomeInformationsUseCase
    {
        Task Execute(HomeModel home);
    }
}
