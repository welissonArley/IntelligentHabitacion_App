using Homuai.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Homuai.Domain.Services
{
    public interface ISendEmail
    {
        Task Send(EmailContent content);
        Task SendMessageSupport(EmailContent content);
    }
}
