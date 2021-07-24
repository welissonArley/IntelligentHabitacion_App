using Homuai.Domain.Entity;
using System;
using System.Collections.ObjectModel;

namespace WebApi.Test.Builder.Entities
{
    public class UserBuilder
    {
        private static UserBuilder _instance;

        public static UserBuilder Instance()
        {
            _instance = new UserBuilder();
            return _instance;
        }

        public User WithoutHome()
        {
            return new User
            {
                Id = 1,
                Email = "user1@email.com.br",
                Password = "",
                Name = "User 1",
                ProfileColorDarkMode = "#FFFFFF",
                ProfileColorLightMode = "#000000",
                PushNotificationId = Guid.NewGuid().ToString(),
                EmergencyContacts = new Collection<EmergencyContact>
                {
                    new EmergencyContact
                    {
                        Id = 1,
                        Name = "Contact 1",
                        Relationship = "Mother",
                        Phonenumber = "+55 38 1000-0000"
                    },
                    new EmergencyContact
                    {
                        Id = 2,
                        Name = "Contact 2",
                        Relationship = "Sister",
                        Phonenumber = "+55 38 2000-0000"
                    }
                },
                Phonenumbers = new Collection<Phonenumber>
                {
                    new Phonenumber
                    {
                        Id = 1,
                        Number = "+55 38 0000-0000"
                    },
                    new Phonenumber
                    {
                        Id = 2,
                        Number = "+55 38 0000-0001"
                    }
                }
            };
        }
    }
}
