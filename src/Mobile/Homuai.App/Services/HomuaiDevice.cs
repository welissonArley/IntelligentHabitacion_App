using XLabs.Ioc;

namespace Homuai.App.Services
{
    public static class HomuaiDevice
    {
        public static double Width()
        {
            return Resolver.Resolve<UserPreferences>().Width;
        }
    }
}
