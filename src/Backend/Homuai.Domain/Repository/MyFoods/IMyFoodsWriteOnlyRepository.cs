using Homuai.Domain.Entity;
using System.Threading.Tasks;

namespace Homuai.Domain.Repository.MyFoods
{
    public interface IMyFoodsWriteOnlyRepository
    {
        Task Add(MyFood myFood);
        void Delete(MyFood myFood);
        Task ChangeAmount(long myFoodId, decimal amount);
        void DeleteAllFromTheUser(long userId);
    }
}
