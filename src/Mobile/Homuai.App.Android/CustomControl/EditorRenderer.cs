using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Homuai.App.CustomControl;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AppEditor), typeof(Homuai.App.Droid.CustomControl.EditorRenderer))]
namespace Homuai.App.Droid.CustomControl
{
    public class EditorRenderer : Xamarin.Forms.Platform.Android.EditorRenderer
    {
        public EditorRenderer(Android.Content.Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
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

            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Android.Graphics.Color.ParseColor(color.ToHex()) : Android.Graphics.Color.White;
        }

        private int GetCursor()
        {
            return Application.Current.RequestedTheme == OSAppTheme.Dark ? Resource.Drawable.CursorEntryWhite : Resource.Drawable.CursorEntryBlack;
        }
    }
}