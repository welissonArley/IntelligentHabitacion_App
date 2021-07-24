using Homuai.App.Model;
using System;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Friends.ChangeDateFriendJoinHome
{
    public interface IChangeDateFriendJoinHomeUseCase
    {
        Task<FriendModel> Execute(string friendId, DateTime date);
    }
}
