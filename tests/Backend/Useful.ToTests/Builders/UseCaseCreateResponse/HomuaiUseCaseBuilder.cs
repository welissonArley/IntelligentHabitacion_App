using Homuai.Application.UseCases;
using Homuai.Domain.Repository.Token;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.TokenController;

namespace Useful.ToTests.Builders.UseCaseCreateResponse
{
    public class HomuaiUseCaseBuilder
    {
        private static HomuaiUseCaseBuilder _instance;
        private readonly ITokenWriteOnlyRepository _tokenRepository;

        private HomuaiUseCaseBuilder(ITokenWriteOnlyRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public static HomuaiUseCaseBuilder Instance()
        {
            var tokenRepository = TokenWriteOnlyRepositoryBuilder.Instance().Build();

            _instance = new HomuaiUseCaseBuilder(tokenRepository);
            return _instance;
        }

        public HomuaiUseCase Build()
        {
            return new HomuaiUseCase(TokenControllerBuilder.Instance().Build(), _tokenRepository);
        }
    }
}
