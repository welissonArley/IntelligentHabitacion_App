using Homuai.Communication.Boolean;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.EmailAlreadyBeenRegistered
{
    public interface IEmailAlreadyBeenRegisteredUseCase
    {
        Task<BooleanJson> Execute(string email);
    }
}
