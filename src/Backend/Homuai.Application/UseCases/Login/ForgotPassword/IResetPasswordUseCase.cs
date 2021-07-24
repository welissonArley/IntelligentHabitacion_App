using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Login.ForgotPassword
{
    public interface IResetPasswordUseCase
    {
        Task Execute(RequestResetYourPasswordJson resetYourPasswordJson);
    }
}
