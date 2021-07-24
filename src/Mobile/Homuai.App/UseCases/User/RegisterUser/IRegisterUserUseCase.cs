using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.User.RegisterUser
{
    public interface IRegisterUserUseCase
    {
        Task Execute(RegisterUserModel userInformations);
    }
}
