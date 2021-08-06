using Bogus;
using Homuai.Domain.Entity;
using Homuai.Domain.ValueObjects;
using System.Collections.Generic;

namespace Useful.ToTests.Builders.Entity
{
    public class HomeBuilder
    {
        private static HomeBuilder _instance;

        public static HomeBuilder Instance()
        {
            _instance = new HomeBuilder();
            return _instance;
        }

        public Home Brazil(User userAdmin)
        {
            return new Faker<Home>()
                .RuleFor(u => u.Id, (f) => f.Random.Long(min: 1, max: 200))
                .RuleFor(u => u.ZipCode, (f) => f.Address.ZipCode("##.###-###"))
                .RuleFor(u => u.Address, (f) => f.Address.StreetAddress())
                .RuleFor(u => u.Number, (f) => f.Address.BuildingNumber())
                .RuleFor(u => u.City, (f) => f.Address.City())
                .RuleFor(u => u.Country, () => CountryAvaliable.BRAZIL)
                .RuleFor(u => u.StateProvince, (f) => f.Address.State())
                .RuleFor(u => u.Neighborhood, (f) => f.Address.Direction())
                .RuleFor(u => u.NetworksName, (f) => f.Internet.UserName())
                .RuleFor(u => u.NetworksPassword, (f) => f.Internet.Password())
                .RuleFor(u => u.Rooms, (f) => new List<Room>
                {
                    new Room
                    {
                        Name = f.Lorem.Word()
                    },
                    new Room
                    {
                        Name = f.Lorem.Word()
                    }
                })
                .RuleFor(u => u.AdministratorId, () => userAdmin.Id);
        }

        public Home OthersCountries(User userAdmin)
        {
            return new Faker<Home>()
                .RuleFor(u => u.Id, (f) => f.Random.Long(min: 1, max: 200))
                .RuleFor(u => u.ZipCode, (f) => f.Address.ZipCode())
                .RuleFor(u => u.Address, (f) => f.Address.StreetAddress())
                .RuleFor(u => u.Number, (f) => f.Address.BuildingNumber())
                .RuleFor(u => u.City, (f) => f.Address.City())
                .RuleFor(u => u.Country, (f) => f.PickRandomWithout(CountryAvaliable.BRAZIL))
                .RuleFor(u => u.Neighborhood, (f) => f.Address.Direction())
                .RuleFor(u => u.NetworksName, (f) => f.Internet.UserName())
                .RuleFor(u => u.NetworksPassword, (f) => f.Internet.Password())
                .RuleFor(u => u.Rooms, (f) => new List<Room>
                {
                    new Room
                    {
                        Name = f.Lorem.Word()
                    },
                    new Room
                    {
                        Name = f.Lorem.Word()
                    }
                })
                .RuleFor(u => u.AdministratorId, () => userAdmin.Id);
        }
    }
}
