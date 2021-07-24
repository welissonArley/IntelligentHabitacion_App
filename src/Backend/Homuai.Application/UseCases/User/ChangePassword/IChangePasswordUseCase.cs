using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task<ResponseOutput> Execute(RequestChangePasswordJson changePasswordJson);
    }
}
