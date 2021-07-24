using Homuai.Application.Services.Token;
using Homuai.Domain.Entity;
using Homuai.Domain.Repository.User;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Homuai.Application.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserReadOnlyRepository _repository;
        private readonly TokenController _tokenController;

        public LoggedUser(IHttpContextAccessor httpContextAccessor,
            IUserReadOnlyRepository repository,
            TokenController tokenController)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _tokenController = tokenController;
        }

        public async Task<User> User()
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            var token = authorization["Bearer ".Length..].Trim();

            var email = _tokenController.User(token);

            var user = await _repository.GetByEmail(email);

            return user;
        }
    }
}
