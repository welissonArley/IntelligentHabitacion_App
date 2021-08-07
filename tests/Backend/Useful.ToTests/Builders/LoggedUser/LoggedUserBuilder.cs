using Homuai.Application.Services.LoggedUser;
using Moq;

namespace Useful.ToTests.Builders.LoggedUser
{
    public class LoggedUserBuilder
    {
        private static LoggedUserBuilder _instance;
        private readonly Mock<ILoggedUser> _service;

        private LoggedUserBuilder()
        {
            if(_service == null)
                _service = new Mock<ILoggedUser>();
        }

        public static LoggedUserBuilder Instance()
        {
            _instance = new LoggedUserBuilder();
            return _instance;
        }

        public LoggedUserBuilder User(Homuai.Domain.Entity.User user)
        {
            _service.Setup(c => c.User()).ReturnsAsync(user);
            return this;
        }

        public ILoggedUser Build()
        {
            return _service.Object;
        }
    }
}
