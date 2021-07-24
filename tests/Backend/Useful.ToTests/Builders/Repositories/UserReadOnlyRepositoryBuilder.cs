using Homuai.Domain.Repository.User;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private static UserReadOnlyRepositoryBuilder _instance;
        private readonly Mock<IUserReadOnlyRepository> _repository;

        private UserReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IUserReadOnlyRepository>();
        }

        public static UserReadOnlyRepositoryBuilder Instance()
        {
            _instance = new UserReadOnlyRepositoryBuilder();
            return _instance;
        }

        public UserReadOnlyRepositoryBuilder ExistActiveUserWithEmail(string email)
        {
            _repository.Setup(x => x.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
            return this;
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
