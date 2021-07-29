using Homuai.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Homuai.EmailHelper.Services.SendEmail
{
    public interface ISendEmail
    {
        Task Send(EmailContent content);
        Task SendMessageSupport(EmailContent content);
    }
}
