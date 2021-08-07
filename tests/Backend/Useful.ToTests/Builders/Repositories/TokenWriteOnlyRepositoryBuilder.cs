using Homuai.Domain.Repository.Token;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class TokenWriteOnlyRepositoryBuilder
    {
        private static TokenWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<ITokenWriteOnlyRepository> _repository;

        private TokenWriteOnlyRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<ITokenWriteOnlyRepository>();
        }

        public static TokenWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new TokenWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public ITokenWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
