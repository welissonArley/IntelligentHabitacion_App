using Homuai.Domain.Services.SendEmail;
using Moq;

namespace Useful.ToTests.Builders.Services.Email
{
    public class SendContactUsEmailBuilder
    {
        private static SendContactUsEmailBuilder _instance;
        private readonly Mock<ISendContactUsEmail> _repository;

        private SendContactUsEmailBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<ISendContactUsEmail>();
            }
        }

        public static SendContactUsEmailBuilder Instance()
        {
            _instance = new SendContactUsEmailBuilder();
            return _instance;
        }

        public ISendContactUsEmail Build()
        {
            return _repository.Object;
        }
    }
}
