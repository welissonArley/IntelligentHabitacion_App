using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Login.ForgotPassword
{
    public interface IRequestCodeResetPasswordUseCase
    {
        Task Execute(string email);
    }
}
