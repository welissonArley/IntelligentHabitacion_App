using System.Threading.Tasks;

namespace Homuai.App.UseCases.Friends.NotifyOrderReceived
{
    public interface INotifyOrderReceivedUseCase
    {
        Task Execute(string friendId);
    }
}
