using Homuai.Application.Services.Token;
using Homuai.Domain.Repository.Token;
using Homuai.Domain.Repository.User;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace Homuai.Api.Filter.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationUserAttribute : AuthenticationBaseAttribute, IAsyncAuthorizationFilter
    {
        private readonly ITokenReadOnlyRepository _tokenRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenRepository"></param>
        /// <param name="tokenController"></param>
        public AuthenticationUserAttribute(IUserReadOnlyRepository userRepository, ITokenReadOnlyRepository tokenRepository, TokenController tokenController) : base(userRepository, tokenController)
        {
            _tokenRepository = tokenRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var user = await GetUser(context);
                if (user == null)
                    UserDoesNotHaveAccess(context);
                else
                {
                    var token = await _tokenRepository.GetByUserId(user.Id);
                    var tokenRequest = TokenOnRequest(context);
                    if (!token.Value.Equals(tokenRequest))
                        UserDoesNotHaveAccess(context);
                }
            }
            catch (SecurityTokenExpiredException)
            {
                TokenExpired(context);
            }
            catch
            {
                UserDoesNotHaveAccess(context);
            }
        }
    }
}
