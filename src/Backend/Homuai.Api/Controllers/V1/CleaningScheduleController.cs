using Homuai.Api.Binder;
using Homuai.Api.Filter.Authentication;
using Homuai.Application.UseCases.CleaningSchedule.Calendar;
using Homuai.Application.UseCases.CleaningSchedule.CreateFirstSchedule;
using Homuai.Application.UseCases.CleaningSchedule.DetailsAllRate;
using Homuai.Application.UseCases.CleaningSchedule.EditTaskAssign;
using Homuai.Application.UseCases.CleaningSchedule.GetTasks;
using Homuai.Application.UseCases.CleaningSchedule.HistoryOfTheDay;
using Homuai.Application.UseCases.CleaningSchedule.RateTask;
using Homuai.Application.UseCases.CleaningSchedule.RegisterRoomCleaned;
using Homuai.Application.UseCases.CleaningSchedule.Reminder;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cleaning-schedule")]
    public class CleaningScheduleController : BaseController
    {
        /// <summary>
        /// This function will return an object with the user's tasks for the date.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("tasks")]
        [ProducesResponseType(typeof(ResponseTasksJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> MyTasks([FromServices] IGetTasksUseCase useCase,
            [FromBody] RequestDateJson request)
        {
            var response = await useCase.Execute(request.Date);
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will create only the first cleaning schedule
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseScheduleTasksCleaningHouseJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> CreateFirstCleaningSchedule([FromServices] ICreateFirstScheduleUseCase useCase,
            [FromBody] List<RequestUpdateCleaningScheduleJson> request)
        {
            var response = await useCase.Execute(request);
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will save one register to confirm that the user cleaned the room received as parameter
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("room-cleaned")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> RegisterRoomCleaned(
            [FromServices] IRegisterRoomCleanedUseCase useCase,
            [FromBody] RequestRegisterRoomCleaned request)
        {
            var response = await useCase.Execute(request);
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will send a PushNotification to the users received as parameter to remember clean room
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("reminder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> Reminder(
            [FromServices] IReminderUseCase useCase,
            [FromBody] IList<string> request)
        {
            var response = await useCase.Execute(request);
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function return the calendar with the days that an room was cleaned.
        /// If the room name on request is empty, so this function will return the calendar considering all rooms cleaned
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("calendar")]
        [ProducesResponseType(typeof(ResponseCalendarCleaningScheduleJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> Calendar(
            [FromServices] ICalendarUseCase useCase,
            [FromBody] RequestCalendarCleaningScheduleJson request)
        {
            var response = await useCase.Execute(request);
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function return the all tasks cleaned in the day.
        /// If the room name on request is empty, so this function will return the calendar considering all rooms cleaned
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("history/day")]
        [ProducesResponseType(typeof(IList<ResponseHistoryRoomOfTheDayJson>), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> HistoryOfTheDay(
            [FromServices] IHistoryOfTheDayUseCase useCase,
            [FromBody] RequestHistoryOfTheDayJson request)
        {
            var response = await useCase.Execute(request);
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will change the assign for a task on the current month
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("task-assign")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> EditTaskAssign(
            [FromServices] IEditTaskAssignUseCase useCase,
            [FromBody] RequestEditAssignCleaningScheduleJson request)
        {
            var response = await useCase.Execute(request);
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will rate one task
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("rate/task/{id:hashids}")]
        [ProducesResponseType(typeof(ResponseAverageRatingJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> RateTask(
            [FromServices] IRateTaskUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id,
            [FromBody] RequestRateTaskJson request)
        {
            var response = await useCase.Execute(id, request);
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will return all rate to the task received in the route
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("rate/details/{id:hashids}")]
        [ProducesResponseType(typeof(IList<ResponseRateTaskJson>), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> RateDetails(
            [FromServices] IDetailsAllRateUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
        {
            var response = await useCase.Execute(id);
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }
    }
}
