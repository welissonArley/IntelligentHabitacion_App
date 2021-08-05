using Homuai.Domain.Repository.Code;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class CodeReadOnlyRepositoryBuilder
    {
        private static CodeReadOnlyRepositoryBuilder _instance;
        private readonly Mock<ICodeReadOnlyRepository> _repository;

        private CodeReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<ICodeReadOnlyRepository>();
            }
        }

        public static CodeReadOnlyRepositoryBuilder Instance()
        {
            _instance = new CodeReadOnlyRepositoryBuilder();
            return _instance;
        }

        public CodeReadOnlyRepositoryBuilder GetByUserId(Homuai.Domain.Entity.Code code)
        {
            _repository.Setup(x => x.GetByUserId(code.UserId)).ReturnsAsync(code);
            return this;
        }

        public ICodeReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
