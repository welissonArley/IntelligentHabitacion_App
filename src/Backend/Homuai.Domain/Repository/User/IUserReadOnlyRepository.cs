using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homuai.Domain.Repository.User
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> ExistActiveUserWithEmail(string email);
        Task<Entity.User> GetByEmail(string email);
        Task<Entity.User> GetByEmailPassword(string email, string password);
        Task<IList<Entity.User>> GetByHome(long homeId);
        Task<Entity.User> GetById(long userId);
    }
}
