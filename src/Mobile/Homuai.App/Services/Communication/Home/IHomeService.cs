using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Refit;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homuai.App.Services.Communication.Home
{
    [Headers("Content-Type: application/json")]
    public interface IHomeService
    {
        [Post("")]
        Task<ApiResponse<string>> CreateHome([Body] RequestRegisterHomeJson registerHome, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Get("")]
        Task<ApiResponse<ResponseHomeInformationsJson>> GetHomesInformations([Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("")]
        Task<ApiResponse<string>> UpdateHome([Body] RequestUpdateHomeJson registerHome, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
