using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Homuai.App.CustomControl;
using Homuai.App.Droid.CustomControl;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryForSearchByName), typeof(EntryForSearchByNameRenderer))]
namespace Homuai.App.Droid.CustomControl
{
    public class EntryForSearchByNameRenderer : EntryRenderer
    {
        public EntryForSearchByNameRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var color = GetLineColor();
                var cursor = GetCursor();

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    Control.Background.SetColorFilter(new BlendModeColorFilter(color, BlendMode.SrcAtop));
                    Control.SetTextCursorDrawable(cursor);
                }
                else
                {
                    Control.BackgroundTintList = ColorStateList.ValueOf(color);

                    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                    JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, cursor);
                }
            }
        }

        private Android.Graphics.Color GetLineColor()
        {
            var color = (Xamarin.Forms.Color)Application.Current.Resources["DarkModeSecondaryColor"];
            string hex = color.ToHex();

            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Android.Graphics.Color.ParseColor(hex) : Android.Graphics.Color.ParseColor("#F1F1F1");
        }

        private int GetCursor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Resource.Drawable.CursorEntryWhite : Resource.Drawable.CursorEntryBlack;
        }
    }
}