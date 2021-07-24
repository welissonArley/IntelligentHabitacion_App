using System.Threading.Tasks;

namespace Homuai.App.UseCases.ContactUs
{
    public interface IContactUsUseCase
    {
        Task Execute(string message);
    }
}
