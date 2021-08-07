using FluentAssertions;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.Home.UpdateHomeInformation;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.Home.UpdateHomeInformation
{
    public class UpdateHomeInformationUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess_Brazil()
        {
            var user = UserBuilder.Instance().HasHomeAssociation_Brazil();

            var jsonUpdateHome = RequestUpdateHome.Instance().Build();

            var useCase = CreateUseCase(user);

            var validationResult = await useCase.Execute(jsonUpdateHome);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validade_Sucess_AnotherCountry()
        {
            var user = UserBuilder.Instance().HasHomeAssociation_OthersCountries();

            var jsonUpdateHome = RequestUpdateHome.Instance().Build();

            var useCase = CreateUseCase(user);

            var validationResult = await useCase.Execute(jsonUpdateHome);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validade_Invalid_Empty_Zipcode()
        {
            var user = UserBuilder.Instance().HasHomeAssociation_OthersCountries();

            var jsonUpdateHome = RequestUpdateHome.Instance().Build();
            jsonUpdateHome.ZipCode = "";

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(jsonUpdateHome); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 &&
                    e.ErrorMensages.Contains(ResourceTextException.ZIPCODE_EMPTY));
        }

        private UpdateHomeInformationUseCase CreateUseCase(Homuai.Domain.Entity.User user)
        {
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var mapper = MapperBuilder.Build();
            var homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();
            var repository = HomeUpdateOnlyRepositoryBuilder.Instance().GetById(user.HomeAssociation.Home).Build();

            return new UpdateHomeInformationUseCase(loggedUser, mapper, unitOfWork, homuaiUseCase, repository);
        }
    }
}
