using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.UpdateMyFood
{
    public interface IUpdateMyFoodUseCase
    {
        Task<ResponseOutput> Execute(long myFoodId, RequestProductJson editMyFood);
    }
}
