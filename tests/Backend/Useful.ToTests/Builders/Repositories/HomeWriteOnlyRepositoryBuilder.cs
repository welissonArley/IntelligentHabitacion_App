using Homuai.Domain.Repository.Home;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class HomeWriteOnlyRepositoryBuilder
    {
        private static HomeWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IHomeWriteOnlyRepository> _repository;

        private HomeWriteOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IHomeWriteOnlyRepository>();
            }
        }

        public static HomeWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new HomeWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IHomeWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
