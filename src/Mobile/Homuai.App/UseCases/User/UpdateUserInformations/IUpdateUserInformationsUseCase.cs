using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.User.UpdateUserInformations
{
    public interface IUpdateUserInformationsUseCase
    {
        Task Execute(UserInformationsModel userInformations);
    }
}
