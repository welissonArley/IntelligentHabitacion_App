using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Refit;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homuai.App.Services.Communication.Friend
{
    [Headers("Content-Type: application/json")]
    public interface IFriendService
    {
        [Get("")]
        Task<ApiResponse<List<ResponseFriendJson>>> GetHouseFriends([Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Post("/notify-order-received/{friendId}")]
        Task<ApiResponse<string>> NotifyFriendOrderHasArrived(string friendId, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/change-date-join-home/{friendId}")]
        Task<ApiResponse<ResponseFriendJson>> ChangeDateJoinHome(string friendId, [Body] RequestDateJson request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Get("/code-remove-friend")]
        Task<ApiResponse<string>> RequestCodeToRemoveFriend([Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Delete("/{friendId}")]
        Task<ApiResponse<string>> RemoveFriend(string friendId, [Body] RequestAdminActionJson request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
