using Homuai.Api.Binder;
using Homuai.Api.Filter.Authentication;
using Homuai.Application.UseCases.Friends.ChangeDateFriendJoinHome;
using Homuai.Application.UseCases.Friends.GetMyFriends;
using Homuai.Application.UseCases.Friends.NotifyOrderReceived;
using Homuai.Application.UseCases.Friends.RemoveFriend;
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
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FriendController : BaseController
    {
        /// <summary>
        /// This function will return the list of friends
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseFriendJson>), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> Friends([FromServices] IGetMyFriendsUseCase useCase)
        {
            var response = await useCase.Execute();
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will change the date when the friend joined at home
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("change-date-join-home/{id:hashids}")]
        [ProducesResponseType(typeof(ResponseFriendJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> ChangeDateJoinHome(
            [FromServices] IChangeDateFriendJoinHomeUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id,
            [FromBody] RequestDateJson request)
        {
            var response = await useCase.Execute(id, request);
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will notify one friend that one order has arrived
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("notify-order-received/{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> NotifyOrderReceived(
            [FromServices] INotifyOrderReceivedUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
        {
            var response = await useCase.Execute(id);
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will send a email to admin with a code to remove a friend
        /// </summary>
        /// <param name="useCase"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("code-remove-friend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> RequestCodeToRemoveFriend(
            [FromServices] IRequestCodeToRemoveFriendUseCase useCase)
        {
            var response = await useCase.Execute();
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will remove a friend from home
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> RemoveFriend(
            [FromServices] IRemoveFriendUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id,
           [FromBody] RequestAdminActionJson request)
        {
            var response = await useCase.Execute(id, request);
            WriteAutenticationHeader(response);

            return Ok();
        }
    }
}