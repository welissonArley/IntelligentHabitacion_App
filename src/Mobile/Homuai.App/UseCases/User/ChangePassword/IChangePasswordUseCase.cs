using System.Threading.Tasks;

namespace Homuai.App.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(string currentPassword, string newPassword);
    }
}
