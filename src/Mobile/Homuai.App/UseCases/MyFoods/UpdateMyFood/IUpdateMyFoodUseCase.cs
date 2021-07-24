using Homuai.App.Model;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.MyFoods.UpdateMyFood
{
    public interface IUpdateMyFoodUseCase
    {
        Task Execute(FoodModel model);
    }
}
