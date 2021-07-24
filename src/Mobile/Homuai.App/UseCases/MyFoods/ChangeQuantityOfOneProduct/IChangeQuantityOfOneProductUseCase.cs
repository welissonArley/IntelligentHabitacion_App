using System.Threading.Tasks;

namespace Homuai.App.UseCases.MyFoods.ChangeQuantityOfOneProduct
{
    public interface IChangeQuantityOfOneProductUseCase
    {
        Task Execute(string productId, decimal amount);
    }
}
