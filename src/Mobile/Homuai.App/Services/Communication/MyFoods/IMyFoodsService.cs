using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Refit;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homuai.App.Services.Communication.MyFoods
{
    [Headers("Content-Type: application/json")]
    public interface IMyFoodsService
    {
        [Get("")]
        Task<ApiResponse<List<ResponseMyFoodJson>>> GetMyFoods([Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Post("")]
        Task<ApiResponse<string>> AddMyFood([Body] RequestProductJson myFood, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/change-quantity/{myFoodId}")]
        Task<ApiResponse<string>> ChangeQuantityMyFood(string myFoodId, [Body] RequestChangeQuantityMyFoodJson myFood, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/{myFoodId}")]
        Task<ApiResponse<string>> EditMyFood(string myFoodId, [Body] RequestProductJson myFood, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Delete("/{myFoodId}")]
        Task<ApiResponse<string>> Delete(string myFoodId, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
