using System.Threading.Tasks;

namespace Homuai.Domain.Repository.MyFoods
{
    public interface IMyFoodsUpdateOnlyRepository
    {
        Task<Entity.MyFood> GetById_Update(long myFoodId, long userId);
        void Update(Entity.MyFood myFood);
    }
}
