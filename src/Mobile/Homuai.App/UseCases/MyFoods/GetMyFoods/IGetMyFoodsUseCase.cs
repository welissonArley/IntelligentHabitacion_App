using Homuai.App.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.App.UseCases.MyFoods.GetMyFoods
{
    public interface IGetMyFoodsUseCase
    {
        Task<IList<FoodModel>> Execute();
    }
}
