using FluentAssertions;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.User.ChangePassword;
using Homuai.Communication.Request;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.User.ChangePassword
{
    public class ChangePasswordUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            const string NewPassword = "@NewPassword123";
            const string CurrentPassword = "@PasswordTest123";

            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            user.Password = passwordEncripter.Encrypt(CurrentPassword);

            var useCase = CreateUseCase(user);

            var validationResult = await useCase.Execute(new RequestChangePasswordJson
            {
                CurrentPassword = CurrentPassword,
                NewPassword = NewPassword
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            user.Password.Should().Equals(passwordEncripter.Encrypt(NewPassword));
        }

        [Fact]
        public async Task Validade_Invalid_CurrentPassword()
        {
            const string NewPassword = "@NewPassword123";
            const string CurrentPassword = "@PasswordTest123";

            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            user.Password = passwordEncripter.Encrypt(CurrentPassword);

            var useCase = CreateUseCase(user);

            Func<Task> act = async () =>
            {
                await useCase.Execute(new RequestChangePasswordJson
                {
                    CurrentPassword = "@WrongCurrentPassword",
                    NewPassword = NewPassword
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 &&
                    e.ErrorMensages.Contains(ResourceTextException.CURRENT_PASSWORD_INVALID));
        }

        [Fact]
        public async Task Validade_Invalid_NewPassword()
        {
            const string CurrentPassword = "@PasswordTest123";

            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            user.Password = passwordEncripter.Encrypt(CurrentPassword);

            var useCase = CreateUseCase(user);

            Func<Task> act = async () =>
            {
                await useCase.Execute(new RequestChangePasswordJson
                {
                    CurrentPassword = CurrentPassword,
                    NewPassword = "@"
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 &&
                    e.ErrorMensages.Contains(ResourceTextException.INVALID_PASSWORD));
        }

        private ChangePasswordUseCase CreateUseCase(Homuai.Domain.Entity.User user)
        {
            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            var userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(user).Build();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();

            return new ChangePasswordUseCase(loggedUser, userUpdateOnlyRepository, unitOfWork, passwordEncripter, homuaiUseCase);
        }
    }
}
