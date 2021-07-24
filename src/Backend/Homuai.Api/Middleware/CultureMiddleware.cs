using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Homuai.Api.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class CultureMiddleware
    {
        readonly RequestDelegate _next;

        private readonly List<string> _idioms = new List<string> { "EN", "PT", "PT-BR", "EN-US" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var culture = new CultureInfo("PT-BR");
            if (context.Request.Headers["Accept-Language"].Count > 0 && _idioms.Contains(context.Request.Headers["Accept-Language"][0].ToUpper()))
                culture = new CultureInfo(context.Request.Headers["Accept-Language"][0]);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(context);
        }
    }
}
