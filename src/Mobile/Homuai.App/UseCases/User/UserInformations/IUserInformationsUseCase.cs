using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.User.UserInformations
{
    public interface IUserInformationsUseCase
    {
        Task<UserInformationsModel> Execute();
    }
}
