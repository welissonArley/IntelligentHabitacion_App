using Homuai.App.Model;
using Homuai.Communication.Request;
using Homuai.Exception.Exceptions;

namespace Homuai.App.UseCases.Home.Strategy
{
    public abstract class HomeRegisterStrategy
    {
        public abstract RequestRegisterHomeJson Mapper_Register(HomeModel model);
        public abstract RequestUpdateHomeJson Mapper_Update(HomeModel model);
        public abstract void Validate(HomeModel model);

        protected void ValidateBase(HomeModel model)
        {
            ValidateZipCode(model.ZipCode);
            ValidadeAdress(model.Address);
            ValidadeNumber(model.Number);
            ValidadeCity(model.City.Name);
        }

        private void ValidateZipCode(string zipcode)
        {
            if (string.IsNullOrWhiteSpace(zipcode))
                throw new ZipCodeEmptyException();
        }
        private void ValidadeNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new NumberEmptyException();
        }
        private void ValidadeAdress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new AddressEmptyException();
        }
        private void ValidadeCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new CityEmptyException();
        }
    }
}
