using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.ChangeQuantityOfOneProduct
{
    public interface IChangeQuantityOfOneProductUseCase
    {
        Task<ResponseOutput> Execute(long id, decimal amount);
    }
}
