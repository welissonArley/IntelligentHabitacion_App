using Bogus;
using Homuai.Domain.Entity;

namespace Useful.ToTests.Builders.Entity
{
    public class EmergencyContactBuilder
    {
        private static EmergencyContactBuilder _instance;

        public static EmergencyContactBuilder Instance()
        {
            _instance = new EmergencyContactBuilder();
            return _instance;
        }

        public EmergencyContact Build()
        {
            return new Faker<EmergencyContact>()
                .RuleFor(u => u.Name, (f) => f.Person.UserName)
                .RuleFor(u => u.Relationship, (f) => f.Random.Words())
                .RuleFor(u => u.Phonenumber, (f) => f.Person.Phone);
        }
    }
}
