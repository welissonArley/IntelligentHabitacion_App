using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Refit;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homuai.App.Services.Communication.Login
{
    [Headers("Content-Type: application/json")]
    public interface ILoginRestService
    {
        [Post("")]
        Task<ApiResponse<ResponseLoginJson>> DoLogin([Body] RequestLoginJson loggin, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Get("/request-code-reset-password/{email}")]
        Task RequestCodeResetPassword(string email, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/reset-password")]
        Task ChangePasswordForgotPassword([Body] RequestResetYourPasswordJson loggin, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
