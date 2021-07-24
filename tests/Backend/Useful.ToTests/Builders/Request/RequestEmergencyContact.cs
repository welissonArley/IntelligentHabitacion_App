using Bogus;
using Homuai.Communication.Request;

namespace Useful.ToTests.Builders.Request
{
    public class RequestEmergencyContact
    {
        private static RequestEmergencyContact _instance;

        public static RequestEmergencyContact Instance()
        {
            _instance = new RequestEmergencyContact();
            return _instance;
        }

        public RequestEmergencyContactJson Build()
        {
            return new Faker<RequestEmergencyContactJson>()
                .RuleFor(u => u.Name, (f) => f.Person.UserName)
                .RuleFor(u => u.Relationship, (f) => f.Random.Words())
                .RuleFor(u => u.Phonenumber, (f) => f.Person.Phone);
        }
    }
}
