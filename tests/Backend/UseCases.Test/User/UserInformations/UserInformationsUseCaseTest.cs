using FluentAssertions;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.User.UserInformations;
using Homuai.Communication.Response;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.User.UserInformations
{
    public class UserInformationsUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();

            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var mapper = MapperBuilder.Build();
            var homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();

            var useCase = new UserInformationsUseCase(loggedUser, mapper, unitOfWork, homuaiUseCase);

            var validationResult = await useCase.Execute();

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<ResponseUserInformationsJson>();

            var responseJson = validationResult.ResponseJson.As<ResponseUserInformationsJson>();
            responseJson.Email.Should().NotBeNullOrEmpty().And.Equals(user.Email);
            responseJson.Name.Should().NotBeNullOrEmpty().And.Equals(user.Name);
        }
    }
}
