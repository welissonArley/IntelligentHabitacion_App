using Bogus;
using Homuai.Communication.Request;

namespace Useful.ToTests.Builders.Request
{
    public class RequestChangePassword
    {
        private static RequestChangePassword _instance;

        public static RequestChangePassword Instance()
        {
            _instance = new RequestChangePassword();
            return _instance;
        }

        public RequestChangePasswordJson Build()
        {
            return new Faker<RequestChangePasswordJson>()
                .RuleFor(u => u.CurrentPassword, (f) => f.Internet.Password(10))
                .RuleFor(u => u.NewPassword, (f) => f.Internet.Password(10));
        }
    }
}
