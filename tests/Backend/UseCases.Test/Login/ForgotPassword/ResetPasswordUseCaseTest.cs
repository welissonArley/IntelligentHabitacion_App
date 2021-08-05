using FluentAssertions;
using Homuai.Application.UseCases.Login.ForgotPassword;
using Homuai.Communication.Request;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Login.ForgotPassword
{
    public class ResetPasswordUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var code = CodeBuilder.Instance().Build(user.Id);

            var useCase = CreateUseCase(user, code);

            Func<Task> act = async () =>
            {
                await useCase.Execute(new RequestResetYourPasswordJson
                {
                    Code = code.Value,
                    Email = user.Email,
                    Password = "@NewPassword123"
                });
            };

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Validade_Invalid_Code()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var code = CodeBuilder.Instance().Build(user.Id);

            var useCase = CreateUseCase(user, code);

            Func<Task> act = async () =>
            {
                await useCase.Execute(new RequestResetYourPasswordJson
                {
                    Code = "123456",
                    Email = user.Email,
                    Password = "@NewPassword123"
                });
            };

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(e => e.ErrorMensages.Count == 1 &&
                    e.ErrorMensages.Contains(ResourceTextException.CODE_INVALID));
        }

        private ResetPasswordUseCase CreateUseCase(Homuai.Domain.Entity.User user, Homuai.Domain.Entity.Code code)
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(user).Build();
            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var codeWriteOnlyRepository = CodeWriteOnlyRepositoryBuilder.Instance().Build();
            var userUpdateReadOnlyRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetByEmail(user).Build();
            var codeReadOnlyRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(code).Build();

            return new ResetPasswordUseCase(passwordEncripter, userUpdateReadOnlyRepository, codeReadOnlyRepository, userReadOnlyRepository, codeWriteOnlyRepository, unitOfWork);
        }
    }
}
