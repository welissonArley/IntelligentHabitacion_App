using Bogus;
using Homuai.Domain.Entity;
using System;
using System.Collections.Generic;
using Useful.ToTests.Builders.Encripter;

namespace Useful.ToTests.Builders.Entity
{
    public class UserBuilder
    {
        private static UserBuilder _instance;

        public static UserBuilder Instance()
        {
            _instance = new UserBuilder();
            return _instance;
        }

        public User WithoutHomeAssociation()
        {
            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();

            return new Faker<User>()
                .RuleFor(u => u.Name, (f) => f.Person.UserName)
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.PushNotificationId, () => Guid.NewGuid().ToString())
                .RuleFor(u => u.Password, (f) => passwordEncripter.Encrypt(f.Internet.Password(10)))
                .RuleFor(u => u.ProfileColorLightMode, (f) => f.Internet.Color())
                .RuleFor(u => u.ProfileColorDarkMode, (f) => f.Internet.Color())
                .RuleFor(u => u.Phonenumbers, (f) => new List<Phonenumber>
                {
                    new Phonenumber
                    {
                        Number = f.Person.Phone
                    },
                    new Phonenumber
                    {
                        Number = f.Phone.PhoneNumber()
                    }
                })
                .RuleFor(u => u.EmergencyContacts, () => new List<EmergencyContact>
                {
                    EmergencyContactBuilder.Instance().Build(),
                    EmergencyContactBuilder.Instance().Build()
                });
        }
    }
}
