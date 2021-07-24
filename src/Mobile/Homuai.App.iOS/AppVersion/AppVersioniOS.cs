using Foundation;
using Homuai.App.iOS.AppVersion;
using Homuai.App.Services.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersioniOS))]
namespace Homuai.App.iOS.AppVersion
{
    public class AppVersioniOS : IAppVersion
    {
        public string GetVersionNumber()
        {
            var info = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"];

            return $"{info.Description}.0";
        }
    }
}