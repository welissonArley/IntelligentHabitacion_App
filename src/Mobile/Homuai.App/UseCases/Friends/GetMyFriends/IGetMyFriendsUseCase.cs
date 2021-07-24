using Homuai.App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Friends.GetMyFriends
{
    public interface IGetMyFriendsUseCase
    {
        Task<IList<FriendModel>> Execute();
    }
}
