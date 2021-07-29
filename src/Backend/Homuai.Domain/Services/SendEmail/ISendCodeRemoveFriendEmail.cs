using Homuai.Domain.Dto;
using System.Threading.Tasks;

namespace Homuai.Domain.Services.SendEmail
{
    public interface ISendCodeRemoveFriendEmail
    {
        Task Send(SendCodeToPerformSomeActionDto dto);
    }
}
