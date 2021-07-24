using Bogus;
using Homuai.Communication.Enums;
using Homuai.Communication.Request;

namespace Useful.ToTests.Builders.Request
{
    public class RequestRegisterHome
    {
        private static RequestRegisterHome _instance;

        public static RequestRegisterHome Instance()
        {
            _instance = new RequestRegisterHome();
            return _instance;
        }

        public RequestRegisterHomeJson OthersCountries()
        {
            return new Faker<RequestRegisterHomeJson>()
                .RuleFor(u => u.ZipCode, (f) => f.Address.ZipCode())
                .RuleFor(u => u.Address, (f) => f.Address.StreetAddress())
                .RuleFor(u => u.Number, (f) => f.Address.BuildingNumber())
                .RuleFor(u => u.City, (f) => f.Address.City())
                .RuleFor(u => u.Country, (f) => f.PickRandomWithout(Country.BRAZIL));
        }

        public RequestRegisterHomeJson Brazil()
        {
            return new Faker<RequestRegisterHomeJson>()
                .RuleFor(u => u.ZipCode, (f) => f.Address.ZipCode("##.###-###"))
                .RuleFor(u => u.Address, (f) => f.Address.StreetAddress())
                .RuleFor(u => u.Number, (f) => f.Address.BuildingNumber())
                .RuleFor(u => u.City, (f) => f.Address.City())
                .RuleFor(u => u.Country, () => Country.BRAZIL)
                .RuleFor(u => u.StateProvince, (f) => f.Address.State())
                .RuleFor(u => u.Neighborhood, (f) => f.Address.Direction());
        }
    }
}
