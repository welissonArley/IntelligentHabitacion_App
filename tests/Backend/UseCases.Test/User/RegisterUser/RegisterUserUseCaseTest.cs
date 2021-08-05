using FluentAssertions;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.User.RegisterUser;
using Homuai.Communication.Response;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.User.RegisterUser
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var user = RequestRegisterUser.Instance().Build();

            var useCase = CreateUseCase();

            var validationResult = await useCase.Execute(user);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<ResponseUserRegisteredJson>();

            var responseJson = validationResult.ResponseJson.As<ResponseUserRegisteredJson>();
            responseJson.ProfileColorLightMode.Should().NotBeNullOrEmpty().And.StartWith("#");
            responseJson.ProfileColorDarkMode.Should().NotBeNullOrEmpty().And.StartWith("#");
            responseJson.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Validade_Empty_PhoneNumbersAndEmergencyContacts()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Phonenumbers.Clear();
            user.EmergencyContacts.Clear();

            var useCase = CreateUseCase();

            Func<Task> act = async () => { await useCase.Execute(user); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 2 &&
                    e.ErrorMensages.Contains(ResourceTextException.PHONENUMBER_EMPTY)
                    && e.ErrorMensages.Contains(ResourceTextException.EMERGENCYCONTACT_EMPTY));
        }

        private RegisterUserUseCase CreateUseCase()
        {
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            var mapper = MapperBuilder.Build();
            var homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();
            var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Instance().Build();

            return new RegisterUserUseCase(mapper, unitOfWork, homuaiUseCase, userWriteOnlyRepository, userReadOnlyRepository, passwordEncripter);
        }
    }
}
