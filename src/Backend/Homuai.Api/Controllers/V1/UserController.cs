using Homuai.Api.Filter.Authentication;
using Homuai.Application.UseCases.User.ChangePassword;
using Homuai.Application.UseCases.User.EmailAlreadyBeenRegistered;
using Homuai.Application.UseCases.User.RegisterUser;
using Homuai.Application.UseCases.User.UpdateUserInformations;
using Homuai.Application.UseCases.User.UserInformations;
using Homuai.Communication.Boolean;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Homuai.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : BaseController
    {
        /// <summary>
        /// This function verify if the user's informations is correct and save the informations on database
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="registerUserJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseUserRegisteredJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson registerUserJson)
        {
            var response = await useCase.Execute(registerUserJson);

            WriteAutenticationHeader(response);
            return Created(string.Empty, response.ResponseJson);
        }

        /// <summary>
        /// This function verify if the e-mail address has already been registered.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("email-already-been-registered/{email}")]
        [ProducesResponseType(typeof(BooleanJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> EmailAlreadyBeenRegistered(
            [FromServices] IEmailAlreadyBeenRegisteredUseCase useCase,
            [FromRoute] string email)
        {
            var response = await useCase.Execute(email);
            return Ok(response);
        }

        /// <summary>
        /// This function will update the logged user's personal informations
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="updateUserJson"></param>
        /// <returns></returns>
        [HttpPut]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateUserInformationsUseCase useCase,
            [FromBody] RequestUpdateUserJson updateUserJson)
        {
            var response = await useCase.Execute(updateUserJson);
            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will update the password
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="changePasswordJson"></param>
        /// <returns></returns>
        [HttpPut]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        [Route("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordJson changePasswordJson)
        {
            var response = await useCase.Execute(changePasswordJson);

            WriteAutenticationHeader(response);

            return Ok();
        }

        /// <summary>
        /// This function will return the user's informations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        [ProducesResponseType(typeof(ResponseUserInformationsJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Informations([FromServices] IUserInformationsUseCase useCase)
        {
            var response = await useCase.Execute();
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }
    }
}
