using FluentAssertions;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.Home.HomeInformation;
using Homuai.Communication.Response;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.Home.HomeInformation
{
    public class HomeInformationUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess_Brazil()
        {
            var user = UserBuilder.Instance().HasHomeAssociation_Brazil();

            var useCase = CreateUseCase(user);

            var validationResult = await useCase.Execute();

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();

            var responseJson = validationResult.ResponseJson.As<ResponseHomeInformationsJson>();
            responseJson.ZipCode.Should().NotBeNullOrEmpty().And.Equals(user.HomeAssociation.Home.ZipCode);
            responseJson.Country.Should().Equals(user.HomeAssociation.Home.Country);
        }

        [Fact]
        public async Task Validade_Sucess_AnotherCountry()
        {
            var user = UserBuilder.Instance().HasHomeAssociation_OthersCountries();

            var useCase = CreateUseCase(user);

            var validationResult = await useCase.Execute();

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();

            var responseJson = validationResult.ResponseJson.As<ResponseHomeInformationsJson>();
            responseJson.ZipCode.Should().NotBeNullOrEmpty().And.Equals(user.HomeAssociation.Home.ZipCode);
            responseJson.Country.Should().Equals(user.HomeAssociation.Home.Country);
        }

        private HomeInformationUseCase CreateUseCase(Homuai.Domain.Entity.User user)
        {
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var mapper = MapperBuilder.Build();
            var homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();

            return new HomeInformationUseCase(loggedUser, mapper, unitOfWork, homuaiUseCase);
        }
    }
}
