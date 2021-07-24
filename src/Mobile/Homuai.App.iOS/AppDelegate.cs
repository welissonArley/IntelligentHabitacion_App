using Com.OneSignal;
using Foundation;
using TinyIoC;
using UIKit;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace Homuai.App.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            ConfigureDI();

            Rg.Plugins.Popup.Popup.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            OneSignal.Current.StartInit(Services.Communication.Notifications.OneSignalKey).EndInit();
            Messier16.Forms.iOS.Controls.Messier16Controls.InitAll();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            var ignore = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);

            LoadApplication(new App());

            Plugin.InputKit.Platforms.iOS.Config.Init();
            OneSignal.Current.RegisterForPushNotifications();

            return base.FinishedLaunching(app, options);
        }

        private void ConfigureDI()
        {
            if (!Resolver.IsSet)
            {
                var container = new TinyContainer(new TinyIoCContainer());

                container.AddDependeces();

                container.Register<IDependencyContainer>(container);

                Resolver.SetResolver(container.GetResolver());
            }
        }
    }
}
