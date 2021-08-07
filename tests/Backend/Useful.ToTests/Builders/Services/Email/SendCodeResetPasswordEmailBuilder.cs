using Homuai.Domain.Services.SendEmail;
using Moq;

namespace Useful.ToTests.Builders.Services.Email
{
    public class SendCodeResetPasswordEmailBuilder
    {
        private static SendCodeResetPasswordEmailBuilder _instance;
        private readonly Mock<ISendCodeResetPasswordEmail> _repository;

        private SendCodeResetPasswordEmailBuilder()
        {
            if (_repository == null)
                _repository = new Mock<ISendCodeResetPasswordEmail>();
        }

        public static SendCodeResetPasswordEmailBuilder Instance()
        {
            _instance = new SendCodeResetPasswordEmailBuilder();
            return _instance;
        }

        public ISendCodeResetPasswordEmail Build()
        {
            return _repository.Object;
        }
    }
}
