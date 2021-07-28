using Homuai.Domain.Services.SendEmail;
using Homuai.EmailHelper.Services.SendEmail.EmailsType;
using Homuai.EmailHelper.Setting;
using Microsoft.Extensions.DependencyInjection;

namespace Homuai.EmailHelper
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddEmailHelper(this IServiceCollection services)
        {
            return services
                    .AddScoped<ICustomRazorEngine, CustomRazorEngine>()
                    .AddScoped<ISendResetPasswordEmail, SendResetPasswordHelper>();
        }
    }
}
