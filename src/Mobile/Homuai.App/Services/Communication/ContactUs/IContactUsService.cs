using Homuai.Communication.Request;
using Refit;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homuai.App.Services.Communication.ContactUs
{
    [Headers("Content-Type: application/json")]
    public interface IContactUsService
    {
        [Post("")]
        Task<ApiResponse<string>> SendMessage([Body] RequestContactUsJson request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
