using FluentAssertions;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.User.EmailAlreadyBeenRegistered;
using Homuai.Application.UseCases.User.UpdateUserInformations;
using Homuai.Communication.Boolean;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using Moq;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.User.UpdateUserInformations
{
    public class UpdateUserInformationsUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();

            var request = RequestUpdateUser.Instance().Build();

            var useCaseEmailAlreadyBeenRegistered = CreateMockUseCaseEmailRegistered(request.Email, false);
            var useCase = CreateUseCase(user, useCaseEmailAlreadyBeenRegistered);

            var validationResult = await useCase.Execute(request);

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();

            user.Email.Should().Equals(request.Email);
            user.Name.Should().Equals(request.Name);
        }

        [Fact]
        public async Task Validade_NewEmailRegistered()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();

            var request = RequestUpdateUser.Instance().Build();

            var useCaseEmailAlreadyBeenRegistered = CreateMockUseCaseEmailRegistered(request.Email, true);
            var useCase = CreateUseCase(user, useCaseEmailAlreadyBeenRegistered);

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 &&
                    e.ErrorMensages.Contains(ResourceTextException.EMAIL_ALREADYBEENREGISTERED));
        }

        [Fact]
        public async Task Validade_EmptyName()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();

            var request = RequestUpdateUser.Instance().Build();
            request.Name = "";

            var useCaseEmailAlreadyBeenRegistered = CreateMockUseCaseEmailRegistered(request.Email, false);
            var useCase = CreateUseCase(user, useCaseEmailAlreadyBeenRegistered);

            Func<Task> act = async () => { await useCase.Execute(request); };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 &&
                    e.ErrorMensages.Contains(ResourceTextException.NAME_EMPTY));
        }

        private UpdateUserInformationsUseCase CreateUseCase(Homuai.Domain.Entity.User user, IEmailAlreadyBeenRegisteredUseCase emailAlreadyBeenRegisteredUseCase)
        {
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var mapper = MapperBuilder.Build();
            var homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            var userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(user).Build();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();

            return new UpdateUserInformationsUseCase(loggedUser, mapper, userUpdateOnlyRepository, unitOfWork, emailAlreadyBeenRegisteredUseCase, homuaiUseCase);
        }
        private IEmailAlreadyBeenRegisteredUseCase CreateMockUseCaseEmailRegistered(string email, bool value)
        {
            var emailAlreadyBeenRegisteredUseCase = new Mock<IEmailAlreadyBeenRegisteredUseCase>();
            emailAlreadyBeenRegisteredUseCase.Setup(c => c.Execute(email)).ReturnsAsync(new BooleanJson { Value = value });

            return emailAlreadyBeenRegisteredUseCase.Object;
        }
    }
}
