using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.User.UpdateUserInformations
{
    public interface IUpdateUserInformationsUseCase
    {
        Task<ResponseOutput> Execute(RequestUpdateUserJson updateUserJson);
    }
}
