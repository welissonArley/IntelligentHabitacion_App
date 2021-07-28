using Homuai.Domain.Dto;
using System.Threading.Tasks;

namespace Homuai.Domain.Services.SendEmail
{
    public interface ISendResetPasswordEmail
    {
        Task Send(ResetPasswordDto dto);
    }
}
