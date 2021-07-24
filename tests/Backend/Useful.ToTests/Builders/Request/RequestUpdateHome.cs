using Bogus;
using Homuai.Communication.Request;
using System.Collections.Generic;

namespace Useful.ToTests.Builders.Request
{
    public class RequestUpdateHome
    {
        private static RequestUpdateHome _instance;

        public static RequestUpdateHome Instance()
        {
            _instance = new RequestUpdateHome();
            return _instance;
        }

        public RequestUpdateHomeJson Build()
        {
            return new Faker<RequestUpdateHomeJson>()
                .RuleFor(u => u.ZipCode, (f) => f.Address.ZipCode())
                .RuleFor(u => u.Address, (f) => f.Address.StreetAddress())
                .RuleFor(u => u.Number, (f) => f.Address.BuildingNumber())
                .RuleFor(u => u.City, (f) => f.Address.City())
                .RuleFor(u => u.Rooms, (f) => new List<string>
                {
                    "Bathroom", "Living room"
                });
        }
    }
}
