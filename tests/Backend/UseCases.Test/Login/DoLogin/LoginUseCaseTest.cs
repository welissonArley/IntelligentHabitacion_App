using FluentAssertions;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.Login.DoLogin;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Homuai.Domain.Repository.User;
using Homuai.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.Login.DoLogin
{
    public class LoginUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess_WithTrueResponse()
        {
            const string CurrentPassword = "@PasswordTest123";

            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            user.Password = passwordEncripter.Encrypt(CurrentPassword);

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmailPassword(user).Build();
            var useCase = CreateUseCase(userReadOnlyRepository);

            var validationResult = await useCase.Execute(new RequestLoginJson
            {
                User = user.Email,
                Password = CurrentPassword
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<ResponseLoginJson>();

            var responseJson = validationResult.ResponseJson.As<ResponseLoginJson>();
            responseJson.ProfileColorLightMode.Should().NotBeNullOrEmpty().And.StartWith("#");
            responseJson.ProfileColorDarkMode.Should().NotBeNullOrEmpty().And.StartWith("#");
            responseJson.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Validade_Empty_PhoneNumbersAndEmergencyContacts()
        {
            const string CurrentPassword = "@PasswordTest123";

            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            user.Password = passwordEncripter.Encrypt(CurrentPassword);

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmailPassword(user).Build();
            var useCase = CreateUseCase(userReadOnlyRepository);

            Func<Task> act = async () =>
            {
                await useCase.Execute(new RequestLoginJson
                {
                    User = "invalidUser@gmail.com",
                    Password = CurrentPassword
                });
            };

            await act.Should().ThrowAsync<InvalidLoginException>();
        }

        private LoginUseCase CreateUseCase(IUserReadOnlyRepository userReadOnlyRepository)
        {
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var mapper = MapperBuilder.Build();
            var homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();

            return new LoginUseCase(userReadOnlyRepository, passwordEncripter, homuaiUseCase, mapper, unitOfWork);
        }
    }
}
