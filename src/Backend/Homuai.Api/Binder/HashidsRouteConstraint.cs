using HashidsNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Globalization;

namespace Homuai.Api.Binder
{
    /// <summary>
    /// 
    /// </summary>
    public class HashidsRouteConstraint : IRouteConstraint
    {
        private readonly IHashids hashids;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashids"></param>
        public HashidsRouteConstraint(IHashids hashids)
        {
            this.hashids = hashids ?? throw new ArgumentNullException(nameof(hashids));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="route"></param>
        /// <param name="routeKey"></param>
        /// <param name="values"></param>
        /// <param name="routeDirection"></param>
        /// <returns></returns>
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var value))
            {
                var hashid = Convert.ToString(value, CultureInfo.InvariantCulture);
                var decode = hashids.Decode(hashid);

                return decode.Length > 0;
            }

            return false;
        }
    }
}
