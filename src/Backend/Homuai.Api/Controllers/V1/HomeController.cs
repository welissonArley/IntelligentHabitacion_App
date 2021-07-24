using Homuai.Api.Filter.Authentication;
using Homuai.Application.UseCases.Home.HomeInformations;
using Homuai.Application.UseCases.Home.RegisterHome;
using Homuai.Application.UseCases.Home.UpdateHomeInformations;
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
    public class HomeController : BaseController
    {
        /// <summary>
        /// This function verify if the homes's information is correct and save the informations on database
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="registerHomeJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterHomeUseCase useCase,
            [FromBody] RequestRegisterHomeJson registerHomeJson)
        {
            var response = await useCase.Execute(registerHomeJson);
            WriteAutenticationHeader(response);

            return Created(string.Empty, string.Empty);
        }

        /// <summary>
        /// This function will return the home's information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseHomeInformationsJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
        public async Task<IActionResult> Informations([FromServices] IHomeInformationsUseCase useCase)
        {
            var response = await useCase.Execute();
            WriteAutenticationHeader(response);

            return Ok(response.ResponseJson);
        }

        /// <summary>
        /// This function will update the Home's information
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="updateHomeJson"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateHomeInformationsUseCase useCase,
            RequestUpdateHomeJson updateHomeJson)
        {
            var response = await useCase.Execute(updateHomeJson);
            WriteAutenticationHeader(response);

            return Ok();
        }
    }
}
