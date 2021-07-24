using System.Threading.Tasks;

namespace Homuai.Domain.Repository.Token
{
    public interface ITokenWriteOnlyRepository
    {
        Task Add(Entity.Token token);
    }
}
