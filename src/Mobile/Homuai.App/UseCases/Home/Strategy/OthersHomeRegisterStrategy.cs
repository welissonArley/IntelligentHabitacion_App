using Homuai.App.Model;
using Homuai.Communication.Request;
using System.Linq;

namespace Homuai.App.UseCases.Home.Strategy
{
    public class OthersHomeRegisterStrategy : HomeRegisterStrategy
    {
        public override RequestRegisterHomeJson Mapper_Register(HomeModel model)
        {
            return new RequestRegisterHomeJson
            {
                ZipCode = model.ZipCode,
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                Address = model.Address,
                Number = model.Number,
                City = model.City.Name,
                Country = (Communication.Enums.Country)model.City.Country.Id,
                StateProvince = model.City.StateProvinceName
            };
        }

        public override RequestUpdateHomeJson Mapper_Update(HomeModel model)
        {
            return new RequestUpdateHomeJson
            {
                ZipCode = model.ZipCode,
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                Address = model.Address,
                Number = model.Number,
                City = model.City.Name,
                StateProvince = model.City.StateProvinceName,
                NetworksName = model.NetWork.Name,
                NetworksPassword = model.NetWork.Password,
                Rooms = model.Rooms.Select(c => c.Room).ToList()
            };
        }

        public override void Validate(HomeModel model)
        {
            ValidateBase(model);
        }
    }
}
