using Android.Content;
using Homuai.App.Droid.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(ButtonAllCapsFalseRenderer))]
namespace Homuai.App.Droid.CustomControl
{
    public class ButtonAllCapsFalseRenderer : ButtonRenderer
    {
        public ButtonAllCapsFalseRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Control.SetAllCaps(false);
        }
    }
}