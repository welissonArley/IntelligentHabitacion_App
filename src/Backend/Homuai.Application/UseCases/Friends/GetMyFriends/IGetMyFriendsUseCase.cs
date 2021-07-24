using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.GetMyFriends
{
    public interface IGetMyFriendsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
