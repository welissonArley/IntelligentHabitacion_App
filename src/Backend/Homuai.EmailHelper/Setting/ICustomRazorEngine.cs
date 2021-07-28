using System.Threading.Tasks;

namespace Homuai.EmailHelper.Setting
{
    public interface ICustomRazorEngine
    {
        Task<string> RazorViewToHtmlAsync<TModel>(string viewName, TModel model);
    }
}
