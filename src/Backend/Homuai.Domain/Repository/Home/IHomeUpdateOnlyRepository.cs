using System.Threading.Tasks;

namespace Homuai.Domain.Repository.Home
{
    public interface IHomeUpdateOnlyRepository
    {
        Task<Entity.Home> GetById_Update(long homeId);
        void Update(Entity.Home home);
    }
}
