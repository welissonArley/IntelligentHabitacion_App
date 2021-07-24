using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.UserInformations
{
    public interface IUserInformationsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
