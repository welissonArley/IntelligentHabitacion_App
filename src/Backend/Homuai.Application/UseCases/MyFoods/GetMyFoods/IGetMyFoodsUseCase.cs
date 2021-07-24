using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.GetMyFoods
{
    public interface IGetMyFoodsUseCase
    {
        Task<ResponseOutput> Execute();
    }
}
