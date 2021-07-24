using Homuai.App.Model;
using Homuai.Communication.Request;
using Homuai.Exception;
using Homuai.Exception.Exceptions;
using System.Linq;
using System.Text.RegularExpressions;

namespace Homuai.App.UseCases.Home.Strategy
{
    public class BrazilHomeRegisterStrategy : HomeRegisterStrategy
    {
        public override RequestRegisterHomeJson Mapper_Register(HomeModel model)
        {
            return new RequestRegisterHomeJson
            {
                ZipCode = model.ZipCode,
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                Address = model.Address,
                Neighborhood = model.Neighborhood,
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
                NetworksName = model.NetWork.Name,
                NetworksPassword = model.NetWork.Password,
                ZipCode = model.ZipCode,
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                Address = model.Address,
                Neighborhood = model.Neighborhood,
                Number = model.Number,
                City = model.City.Name,
                StateProvince = model.City.StateProvinceName,
                Rooms = model.Rooms.Select(c => c.Room).ToList()
            };
        }

        public override void Validate(HomeModel model)
        {
            ValidateBase(model);
            ValidateStateProvince(model.City.StateProvinceName);
            ValidadeNeighborhood(model.Neighborhood);
            ValidateZipCode(model.ZipCode);
        }

        private void ValidateStateProvince(string stateProvince)
        {
            if (string.IsNullOrWhiteSpace(stateProvince))
                throw new StateProvinceEmptyException();
        }
        private void ValidadeNeighborhood(string neighborhood)
        {
            if (string.IsNullOrWhiteSpace(neighborhood))
                throw new NeighborhoodEmptyException();
        }
        public void ValidateZipCode(string zipCode)
        {
            Regex regex = new Regex(RegexExpressions.CEP);
            if (!regex.Match(zipCode).Success)
                throw new ZipCodeInvalidException(ResourceTextException.ZIPCODE_INVALID_BRAZIL);
        }
    }
}
