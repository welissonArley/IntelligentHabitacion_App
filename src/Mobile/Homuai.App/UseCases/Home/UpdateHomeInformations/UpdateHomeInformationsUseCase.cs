using Homuai.App.Model;
using Homuai.App.Services;
using Homuai.App.Services.Communication.Home;
using Homuai.App.UseCases.Home.Strategy;
using Refit;
using System;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.Home.UpdateHomeInformations
{
    public class UpdateHomeInformationsUseCase : UseCaseBase, IUpdateHomeInformationsUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IHomeService _restService;
        private readonly ContextStrategy _contextStrategy;

        public UpdateHomeInformationsUseCase(Lazy<UserPreferences> userPreferences) : base("Home")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IHomeService>(BaseAddress());
            _contextStrategy = new ContextStrategy();
        }

        public async Task Execute(HomeModel home)
        {
            var strategy = _contextStrategy.GetStrategy(home.City.Country);

            strategy.Validate(home);

            var request = strategy.Mapper_Update(home);

            var response = await _restService.UpdateHome(request, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));

            _userPreferences.UserIsAdministrator(true);
        }
    }
}
