using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.RemoveFriend
{
    public interface IRemoveFriendUseCase
    {
        Task<ResponseOutput> Execute(long friendId, RequestAdminActionJson request);
    }
}
