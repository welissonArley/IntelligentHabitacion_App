using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Refit;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homuai.App.Services.Communication.CleaningSchedule
{
    [Headers("Content-Type: application/json")]
    public interface ICleaningScheduleService
    {
        [Post("/tasks")]
        Task<ApiResponse<ResponseTasksJson>> GetTasks([Body] RequestDateJson request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Post("")]
        Task<ApiResponse<ResponseScheduleTasksCleaningHouseJson>> CreateFirstCleaningSchedule([Body] IList<RequestUpdateCleaningScheduleJson> request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/room-cleaned")]
        Task<ApiResponse<string>> RegisterRoomCleaned([Body] RequestRegisterRoomCleaned request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/reminder")]
        Task<ApiResponse<string>> ReminderUsers([Body] IList<string> request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/calendar")]
        Task<ApiResponse<ResponseCalendarCleaningScheduleJson>> Calendar([Body] RequestCalendarCleaningScheduleJson request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/history/day")]
        Task<ApiResponse<IList<ResponseHistoryRoomOfTheDayJson>>> HistoryOfTheDay([Body] RequestHistoryOfTheDayJson request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Put("/task-assign")]
        Task<ApiResponse<string>> EditTaskAssign([Body] RequestEditAssignCleaningScheduleJson request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Post("/rate/task/{taskCompletedId}")]
        Task<ApiResponse<ResponseAverageRatingJson>> RateTask(string taskCompletedId, [Body] RequestRateTaskJson request, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
        [Get("/rate/details/{taskCompletedId}")]
        Task<ApiResponse<IList<ResponseRateTaskJson>>> RateDetails(string taskCompletedId, [Authorize("Bearer")] string token, [Header("Accept-Language")] StringWithQualityHeaderValue language);
    }
}
