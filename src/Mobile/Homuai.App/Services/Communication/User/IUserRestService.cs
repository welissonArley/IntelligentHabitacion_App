using Homuai.Communication.Boolean;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Refit;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homuai.App.Services.Communication.User
{
    [Headers("Content-Type: application/json")]
    public interface IUserRestService
    {
        [Get("/email-already-been-registered/{email}")]
        Task<BooleanJson> EmailAlreadyBeenRegistered(string email, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Post("")]
        Task<ApiResponse<ResponseUserRegisteredJson>> CreateUser([Body] RequestRegisterUserJson registerUser, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Get("")]
        Task<ApiResponse<ResponseUserInformationsJson>> GetUserInformations([Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("")]
        Task<ApiResponse<string>> UpdateUser([Body] RequestUpdateUserJson updateUser, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/change-password")]
        Task<ApiResponse<string>> ChangePassword([Body] RequestChangePasswordJson changePassword, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
