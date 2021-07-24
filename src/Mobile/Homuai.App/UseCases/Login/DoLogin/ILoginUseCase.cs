using System.Threading.Tasks;

namespace Homuai.App.UseCases.Login.DoLogin
{
    public interface ILoginUseCase
    {
        /// <summary>
        /// This function will return true if the user is part of one home
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> Execute(string email, string password);
    }
}
