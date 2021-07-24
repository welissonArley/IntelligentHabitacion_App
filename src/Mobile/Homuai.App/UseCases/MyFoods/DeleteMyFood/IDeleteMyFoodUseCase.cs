using System.Threading.Tasks;

namespace Homuai.App.UseCases.MyFoods.DeleteMyFood
{
    public interface IDeleteMyFoodUseCase
    {
        Task Execute(string productId);
    }
}
