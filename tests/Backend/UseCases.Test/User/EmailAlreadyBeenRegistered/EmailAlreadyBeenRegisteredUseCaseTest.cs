using FluentAssertions;
using Homuai.Application.UseCases.User.EmailAlreadyBeenRegistered;
using Homuai.Communication.Boolean;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.User.EmailAlreadyBeenRegistered
{
    public class EmailAlreadyBeenRegisteredUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess_WithTrueResponse()
        {
            var email = "user@email.com";

            var userRepository = UserReadOnlyRepositoryBuilder.Instance().ExistActiveUserWithEmail(email).Build();

            var useCase = new EmailAlreadyBeenRegisteredUseCase(userRepository);

            var validationResult = await useCase.Execute(email);

            validationResult.Should().BeOfType<BooleanJson>();
            validationResult.Value.Should().BeTrue();
        }

        [Fact]
        public async Task Validade_Sucess_WithFalseResponse()
        {
            var email = "user@email.com";

            var userRepository = UserReadOnlyRepositoryBuilder.Instance().Build();

            var useCase = new EmailAlreadyBeenRegisteredUseCase(userRepository);

            var validationResult = await useCase.Execute(email);

            validationResult.Should().BeOfType<BooleanJson>();
            validationResult.Value.Should().BeFalse();
        }
    }
}
