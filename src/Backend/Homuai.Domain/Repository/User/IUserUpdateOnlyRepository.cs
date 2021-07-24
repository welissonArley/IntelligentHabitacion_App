using System.Threading.Tasks;

namespace Homuai.Domain.Repository.User
{
    public interface IUserUpdateOnlyRepository
    {
        Task<Entity.User> GetById_Update(long id);
        Task<Entity.User> GetByEmail_Update(string email);
        void Update(Entity.User user);
    }
}
