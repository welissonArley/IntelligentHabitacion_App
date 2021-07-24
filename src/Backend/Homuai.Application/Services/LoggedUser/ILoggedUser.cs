using Homuai.Domain.Entity;
using System.Threading.Tasks;

namespace Homuai.Application.Services.LoggedUser
{
    public interface ILoggedUser
    {
        Task<User> User();
    }
}
