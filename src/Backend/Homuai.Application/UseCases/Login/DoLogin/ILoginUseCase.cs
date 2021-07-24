using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.Login.DoLogin
{
    public interface ILoginUseCase
    {
        Task<ResponseOutput> Execute(RequestLoginJson loginJson);
    }
}
