using FluentAssertions;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.Home.RegisterHome;
using Homuai.Exception;
using Homuai.Exception.Exceptions;
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

namespace UseCases.Test.Home.RegisterHome
{
    public class RegisterHomeUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess_Brazil()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();

            var home = RequestRegisterHome.Instance().Brazil();

            var useCase = CreateUseCase(user);

            var validationResult = await useCase.Execute(home);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validade_Sucess_AnotherCountry()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();

            var home = RequestRegisterHome.Instance().OthersCountries();

            var useCase = CreateUseCase(user);

            var validationResult = await useCase.Execute(home);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Validade_Empty_ZipCode()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();

            var home = RequestRegisterHome.Instance().OthersCountries();
            home.ZipCode = "";

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(home); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 && e.ErrorMensages.Contains(ResourceTextException.ZIPCODE_EMPTY));
        }

        [Fact]
        public async Task Validade_Invalid_User_Is_Part_Home_Brazil()
        {
            var user = UserBuilder.Instance().HasHomeAssociation_Brazil();

            var home = RequestRegisterHome.Instance().Brazil();

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(home); };

            await act.Should().ThrowAsync<UserIsPartOfAHomeException>();
        }

        [Fact]
        public async Task Validade_Invalid_User_Is_Part_Home_OthersCountries()
        {
            var user = UserBuilder.Instance().HasHomeAssociation_OthersCountries();

            var home = RequestRegisterHome.Instance().OthersCountries();

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(home); };

            await act.Should().ThrowAsync<UserIsPartOfAHomeException>();
        }

        private RegisterHomeUseCase CreateUseCase(Homuai.Domain.Entity.User user)
        {
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var mapper = MapperBuilder.Build();
            var homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();
            var writeWriteOnlyRepository = HomeWriteOnlyRepositoryBuilder.Instance().Build();

            return new RegisterHomeUseCase(writeWriteOnlyRepository, unitOfWork, loggedUser, mapper, homuaiUseCase);
        }
    }
}
