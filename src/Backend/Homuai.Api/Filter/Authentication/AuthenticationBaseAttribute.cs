using Homuai.Application.Services.Token;
using Homuai.Communication.Error;
using Homuai.Domain.Entity;
using Homuai.Domain.Repository.User;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Homuai.Api.Filter.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationBaseAttribute : AuthorizeAttribute
    {
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly TokenController _tokenController;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenController"></param>
        public AuthenticationBaseAttribute(IUserReadOnlyRepository userRepository, TokenController tokenController)
        {
            _userRepository = userRepository;
            _tokenController = tokenController;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected void UserDoesNotHaveAccess(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new HomuaiException(ResourceTextException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected void TokenExpired(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorJson
            {
                ErrorCode = ErrorCode.TokenExpired
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected async Task<User> GetUser(AuthorizationFilterContext context)
        {
            var tokenRequest = TokenOnRequest(context);
            var email = _tokenController.User(tokenRequest);
            return await _userRepository.GetByEmail(email);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authentication))
                throw new HomuaiException("");

            return authentication["Bearer ".Length..].Trim();
        }
    }
}
