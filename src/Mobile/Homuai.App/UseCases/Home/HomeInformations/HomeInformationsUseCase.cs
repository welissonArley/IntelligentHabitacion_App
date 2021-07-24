using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.Services.Communication.Home;
using Homuai.App.ValueObjects;
using Homuai.App.ValueObjects.Enum;
using Homuai.Communication.Response;
using Refit;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Home.HomeInformations
{
    public class HomeInformationsUseCase : UseCaseBase, IHomeInformationsUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IHomeService _restService;

        public HomeInformationsUseCase(Lazy<UserPreferences> userPreferences) : base("Home")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IHomeService>(BaseAddress());
        }

        public async Task<HomeModel> Execute()
        {
            var response = await _restService.GetHomesInformations(await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            return Mapper(response.Content);
        }

        private HomeModel Mapper(ResponseHomeInformationsJson response)
        {
            return new HomeModel
            {
                AdditionalAddressInfo = response.AdditionalAddressInfo,
                Address = response.Address,
                City = new CityModel
                {
                    Country = new CoutriesAvaliables().Get().First(c => c.Id == (CountryEnum)response.Country),
                    Name = response.City,
                    StateProvinceName = response.StateProvince
                },
                Neighborhood = response.Neighborhood,
                Number = response.Number,
                ZipCode = response.ZipCode,
                Rooms = new ObservableCollection<RoomModel>(response.Rooms.Select(c => new RoomModel
                {
                    Id = c.Id,
                    Room = c.Name
                })),
                NetWork = new WifiNetworkModel
                {
                    Name = response.NetWork.Name,
                    Password = response.NetWork.Password
                }
            };
        }
    }
}
