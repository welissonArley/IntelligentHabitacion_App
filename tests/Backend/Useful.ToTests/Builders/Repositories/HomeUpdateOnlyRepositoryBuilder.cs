using Homuai.Domain.Repository.Home;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class HomeUpdateOnlyRepositoryBuilder
    {
        private static HomeUpdateOnlyRepositoryBuilder _instance;
        private readonly Mock<IHomeUpdateOnlyRepository> _repository;

        private HomeUpdateOnlyRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<IHomeUpdateOnlyRepository>();
        }

        public static HomeUpdateOnlyRepositoryBuilder Instance()
        {
            _instance = new HomeUpdateOnlyRepositoryBuilder();
            return _instance;
        }

        public HomeUpdateOnlyRepositoryBuilder GetById(Homuai.Domain.Entity.Home home)
        {
            _repository.Setup(c => c.GetById_Update(home.Id)).ReturnsAsync(home);
            return this;
        }

        public IHomeUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
