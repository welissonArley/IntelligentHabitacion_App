using Homuai.Application.UseCases.Login.DoLogin;
using Homuai.Application.UseCases.Login.ForgotPassword;
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
    public class LoginController : BaseController
    {
        /// <summary>
        /// Function to do Login on API
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="loginJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(
            [FromServices] ILoginUseCase useCase,
            [FromBody] RequestLoginJson loginJson)
        {
            var response = await useCase.Execute(loginJson);
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// Use this function to get a code to reset your password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("request-code-reset-password/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RequestCodeResetPassword(
            [FromServices] IRequestCodeResetPasswordUseCase useCase,
            [FromRoute] string email)
        {
            await useCase.Execute(email);

            return Ok();
        }

        /// <summary>
        /// Use this function to reset the password
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="resetYourPasswordJson"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetYourPassword(
            [FromServices] IResetPasswordUseCase useCase,
            [FromBody] RequestResetYourPasswordJson resetYourPasswordJson)
        {
            await useCase.Execute(resetYourPasswordJson);

            return Ok();
        }
    }
}