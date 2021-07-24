using System.Threading.Tasks;

namespace Homuai.Domain.Repository
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
