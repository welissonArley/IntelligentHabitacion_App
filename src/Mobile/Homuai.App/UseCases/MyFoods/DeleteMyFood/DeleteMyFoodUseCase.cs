using Homuai.App.Services;
using Homuai.App.Services.Communication.MyFoods;
using Refit;
using System;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.MyFoods.DeleteMyFood
{
    public class DeleteMyFoodUseCase : UseCaseBase, IDeleteMyFoodUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IMyFoodsService _restService;

        public DeleteMyFoodUseCase(Lazy<UserPreferences> userPreferences) : base("MyFood")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IMyFoodsService>(BaseAddress());
        }

        public async Task Execute(string productId)
        {
            var response = await _restService.Delete(productId, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }
    }
}
