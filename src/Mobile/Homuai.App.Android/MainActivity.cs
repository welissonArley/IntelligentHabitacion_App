using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Com.OneSignal;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;
using TinyIoC;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace Homuai.App.Droid
{
    [Activity(Label = "Homuai", Icon = "@mipmap/ic_launcher", RoundIcon = "@mipmap/ic_launcher_round", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ConfigureDI();

            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            OneSignal.Current.StartInit(Services.Communication.Notifications.OneSignalKey).EndInit();
            OneSignal.Current.RegisterForPushNotifications();
            CrossFingerprint.SetCurrentActivityResolver(() => CrossCurrentActivity.Current.Activity);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Messier16.Forms.Android.Controls.Messier16Controls.InitAll();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            var ignore = typeof(FFImageLoading.Svg.Forms.SvgCachedImage);

            LoadApplication(new App());
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
    }
}