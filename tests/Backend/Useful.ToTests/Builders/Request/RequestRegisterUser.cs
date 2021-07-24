using Bogus;
using Homuai.Communication.Request;
using System;
using System.Collections.Generic;

namespace Useful.ToTests.Builders.Request
{
    public class RequestRegisterUser
    {
        private static RequestRegisterUser _instance;

        public static RequestRegisterUser Instance()
        {
            _instance = new RequestRegisterUser();
            return _instance;
        }

        public RequestRegisterUserJson Build()
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(u => u.Name, (f) => f.Person.UserName)
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.PushNotificationId, () => Guid.NewGuid().ToString())
                .RuleFor(u => u.Password, (f) => f.Internet.Password(10))
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
