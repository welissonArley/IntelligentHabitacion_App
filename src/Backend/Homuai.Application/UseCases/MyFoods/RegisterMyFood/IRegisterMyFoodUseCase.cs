using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.MyFoods.RegisterMyFood
{
    public interface IRegisterMyFoodUseCase
    {
        Task<ResponseOutput> Execute(RequestProductJson requestMyFood);
    }
}
