using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Homuai.App.Droid
{
    [Activity(Label = "Homuai", Theme = "@style/Theme.Splash", LaunchMode = LaunchMode.SingleTop, MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, NoHistory = true)]
    public class SplashScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MainActivity));
        }

        public override void OnBackPressed() { /* Prevent the back button from canceling the startup process */ }
    }
}