using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Login.ForgotPassword
{
    public interface IResetPasswordUseCase
    {
        Task Execute(ForgetPasswordModel model);
    }
}
