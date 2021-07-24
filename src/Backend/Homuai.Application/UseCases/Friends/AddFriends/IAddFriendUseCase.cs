using Homuai.Communication.Request;
using Homuai.Communication.Response;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.AddFriends
{
    public interface IAddFriendUseCase
    {
        Task<ResponseCodeToAddFriendJson> GetCodeToAddFriend(string userToken);
        Task<ResponseCodeWasReadJson> CodeWasRead(string userToken, string code);
        Task ApproveFriend(string adminId, string friendId, RequestApproveAddFriendJson requestApprove);
    }
}
