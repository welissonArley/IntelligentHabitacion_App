using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.DeleteMyFood
{
    public interface IDeleteMyFoodUseCase
    {
        Task<ResponseOutput> Execute(long myFoodId);
    }
}
