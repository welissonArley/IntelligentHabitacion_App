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

[assembly: ExportRenderer(typeof(AppEntry), typeof(AppEntryRenderer))]
namespace Homuai.App.Droid.CustomControl
{
    public class AppEntryRenderer : EntryRenderer
    {
        public AppEntryRenderer(Context context) : base(context) { }

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
            var color = Application.Current.RequestedTheme == OSAppTheme.Dark ? "#62615E" : "#E5E5E5";

            return Android.Graphics.Color.ParseColor(color);
        }

        private int GetCursor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Resource.Drawable.CursorEntryWhite : Resource.Drawable.CursorEntryBlack;
        }
    }
}