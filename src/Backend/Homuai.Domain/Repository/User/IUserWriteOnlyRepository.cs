using System.Threading.Tasks;

namespace Homuai.Domain.Repository.User
{
    public interface IUserWriteOnlyRepository
    {
        Task Add(Entity.User user);
    }
}
