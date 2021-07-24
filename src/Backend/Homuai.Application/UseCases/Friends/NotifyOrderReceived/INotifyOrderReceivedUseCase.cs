using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Friends.NotifyOrderReceived
{
    public interface INotifyOrderReceivedUseCase
    {
        Task<ResponseOutput> Execute(long friendId);
    }
}
