using Homuai.Domain.Repository.Code;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class CodeWriteOnlyRepositoryBuilder
    {
        private static CodeWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<ICodeWriteOnlyRepository> _repository;

        private CodeWriteOnlyRepositoryBuilder()
        {
            if (_repository == null)
                _repository = new Mock<ICodeWriteOnlyRepository>();
        }

        public static CodeWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new CodeWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public ICodeWriteOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
