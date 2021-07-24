using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.ChangeDateFriendJoinHome
{
    public interface IChangeDateFriendJoinHomeUseCase
    {
        Task<ResponseOutput> Execute(long myFriendId, RequestDateJson request);
    }
}
