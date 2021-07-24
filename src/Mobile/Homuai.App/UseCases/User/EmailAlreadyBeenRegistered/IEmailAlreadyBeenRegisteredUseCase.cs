using System.Threading.Tasks;

namespace Homuai.App.UseCases.User.EmailAlreadyBeenRegistered
{
    public interface IEmailAlreadyBeenRegisteredUseCase
    {
        Task Execute(string email);
    }
}
