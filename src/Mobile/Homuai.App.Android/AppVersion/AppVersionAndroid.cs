using Homuai.App.Droid.AppVersion;
using Homuai.App.Services.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersionAndroid))]
namespace Homuai.App.Droid.AppVersion
{
    public class AppVersionAndroid : IAppVersion
    {
        public string GetVersionNumber()
        {
            var context = Android.App.Application.Context;
            var manager = context.PackageManager;
            var info = manager.GetPackageInfo(context.PackageName, 0);

            return $"{info.VersionName}.0";
        }
    }
}