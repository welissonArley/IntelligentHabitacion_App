using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.RegisterUser
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseOutput> Execute(RequestRegisterUserJson registerUserJson);
    }
}
