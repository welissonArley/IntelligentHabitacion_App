using System.Threading.Tasks;

namespace Homuai.Domain.Repository.Token
{
    public interface ITokenReadOnlyRepository
    {
        Task<Entity.Token> GetByUserId(long userId);
    }
}
