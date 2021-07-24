using Homuai.Domain.Repository.User;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class UserUpdateOnlyRepositoryBuilder
    {
        private static UserUpdateOnlyRepositoryBuilder _instance;
        private readonly Mock<IUserUpdateOnlyRepository> _repository;

        private UserUpdateOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IUserUpdateOnlyRepository>();
            }
        }

        public static UserUpdateOnlyRepositoryBuilder Instance()
        {
            _instance = new UserUpdateOnlyRepositoryBuilder();
            return _instance;
        }

        public UserUpdateOnlyRepositoryBuilder GetById(Homuai.Domain.Entity.User user)
        {
            _repository.Setup(c => c.GetById_Update(user.Id)).ReturnsAsync(user);
            return _instance;
        }

        public IUserUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
