using FluentAssertions;
using Homuai.Application.UseCases.Login.ForgotPassword;
using Homuai.Domain.Repository.User;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Services.Email;
using Xunit;

namespace UseCases.Test.Login.ForgotPassword
{
    public class RequestCodeResetPasswordUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess_WithTrueResponse()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();

            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(user).Build();
            var useCase = CreateUseCase(userReadOnlyRepository);

            Func<Task> act = async () => { await useCase.Execute(user.Email); };

            await act.Should().NotThrowAsync();
        }

        private RequestCodeResetPasswordUseCase CreateUseCase(IUserReadOnlyRepository userReadOnlyRepository)
        {
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var codeWriteOnlyRepository = CodeWriteOnlyRepositoryBuilder.Instance().Build();
            var emailSender = SendCodeResetPasswordEmailBuilder.Instance().Build();

            return new RequestCodeResetPasswordUseCase(userReadOnlyRepository, codeWriteOnlyRepository, emailSender, unitOfWork);
        }
    }
}
