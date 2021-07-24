using Homuai.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Homuai.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        protected void WriteAutenticationHeader(ResponseOutput response)
        {
            Response.Headers.Add("Tvih", response.Token);
        }
    }
}
