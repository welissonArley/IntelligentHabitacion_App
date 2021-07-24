using System.Threading.Tasks;

namespace Homuai.Domain.Repository.Home
{
    public interface IHomeWriteOnlyRepository
    {
        Task Add(Entity.User administrator, Entity.Home home);
    }
}
