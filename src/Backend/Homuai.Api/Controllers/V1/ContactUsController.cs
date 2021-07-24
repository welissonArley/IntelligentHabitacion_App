using Homuai.Api.Filter.Authentication;
using Homuai.Application.UseCases.ContactUs;
using Homuai.Communication.Request;
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
    [ServiceFilter(typeof(AuthenticationUserAttribute))]
    public class ContactUsController : BaseController
    {
        /// <summary>
        /// This function send an email with the user's message to the support
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SendMessageToUs([FromServices] IContactUsUseCase useCase,
            [FromBody] RequestContactUsJson request)
        {
            var response = await useCase.Execute(request);
            WriteAutenticationHeader(response);

            return Ok();
        }
    }
}
