using System.Threading.Tasks;

namespace Homuai.App.UseCases.Login.ForgotPassword
{
    public interface IRequestCodeResetPasswordUseCase
    {
        Task Execute(string email);
    }
}
