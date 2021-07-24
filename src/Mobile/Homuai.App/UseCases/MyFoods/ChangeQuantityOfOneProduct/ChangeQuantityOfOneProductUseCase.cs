using Homuai.App.Services;
using Homuai.App.Services.Communication.MyFoods;
using Homuai.Communication.Request;
using Refit;
using System;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.MyFoods.ChangeQuantityOfOneProduct
{
    public class ChangeQuantityOfOneProductUseCase : UseCaseBase, IChangeQuantityOfOneProductUseCase
    {
        private readonly Lazy<UserPreferences> userPreferences;
        private UserPreferences _userPreferences => userPreferences.Value;
        private readonly IMyFoodsService _restService;

        public ChangeQuantityOfOneProductUseCase(Lazy<UserPreferences> userPreferences) : base("MyFood")
        {
            this.userPreferences = userPreferences;
            _restService = RestService.For<IMyFoodsService>(BaseAddress());
        }

        public async Task Execute(string productId, decimal amount)
        {
            var response = await _restService.ChangeQuantityMyFood(productId, new RequestChangeQuantityMyFoodJson { Amount = amount }, await _userPreferences.GetToken(), GetLanguage());

            ResponseValidate(response);

            await _userPreferences.ChangeToken(GetTokenOnHeaderRequest(response.Headers));
        }
    }
}
