using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.MyFoods.RegisterMyFood
{
    public interface IRegisterMyFoodUseCase
    {
        Task<FoodModel> Execute(FoodModel model);
    }
}
