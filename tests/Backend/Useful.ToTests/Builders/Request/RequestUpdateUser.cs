using Bogus;
using Homuai.Communication.Request;
using System.Collections.Generic;

namespace Useful.ToTests.Builders.Request
{
    public class RequestUpdateUser
    {
        private static RequestUpdateUser _instance;

        public static RequestUpdateUser Instance()
        {
            _instance = new RequestUpdateUser();
            return _instance;
        }

        public RequestUpdateUserJson Build()
        {
            return new Faker<RequestUpdateUserJson>()
                .RuleFor(u => u.Name, (f) => f.Person.UserName)
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Phonenumbers, (f) => new List<string>
                {
                    f.Person.Phone, f.Phone.PhoneNumber()
                })
                .RuleFor(u => u.EmergencyContacts, () => new List<RequestEmergencyContactJson>
                {
                    RequestEmergencyContact.Instance().Build(),
                    RequestEmergencyContact.Instance().Build()
                });
        }
    }
}
