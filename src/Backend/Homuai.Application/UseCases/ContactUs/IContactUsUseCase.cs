using Homuai.Communication.Request;
using System.Threading.Tasks;

namespace Homuai.Application.UseCases.ContactUs
{
    public interface IContactUsUseCase
    {
        Task<ResponseOutput> Execute(RequestContactUsJson request);
    }
}
