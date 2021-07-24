using System.Threading.Tasks;

namespace Homuai.App.UseCases.Friends.RemoveFriend
{
    public interface IRemoveFriendUseCase
    {
        Task Execute(string friendId, string code, string password);
    }
}
