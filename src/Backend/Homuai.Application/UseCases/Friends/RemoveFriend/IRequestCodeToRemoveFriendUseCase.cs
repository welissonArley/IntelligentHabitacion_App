using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.RemoveFriend
{
    public interface IRequestCodeToRemoveFriendUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
