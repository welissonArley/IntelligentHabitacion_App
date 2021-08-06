using FluentAssertions;
using Homuai.Application.UseCases;
using Homuai.Application.UseCases.ContactUs;
using Homuai.Communication.Request;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Services.Email;
using Useful.ToTests.Builders.UseCaseCreateResponse;
using Xunit;

namespace UseCases.Test.ContactUs
{
    public class ContactUsUseCaseTest
    {
        [Fact]
        public async Task Validade_Sucess_WithTrueResponse()
        {
            var useCase = CreateUseCase();

            var validationResult = await useCase.Execute(new RequestContactUsJson
            {
                Message = "My Message"
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
        }

        private ContactUsUseCase CreateUseCase()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var unitOfWork = UnitOfWorkBuilder.Instance().Build();
            var homuaiUseCase = HomuaiUseCaseBuilder.Instance().Build();
            var loggedUser = LoggedUserBuilder.Instance().User(user).Build();
            var sendContactUsEmail = SendContactUsEmailBuilder.Instance().Build();

            return new ContactUsUseCase(loggedUser, sendContactUsEmail, homuaiUseCase, unitOfWork);
        }
    }
}
