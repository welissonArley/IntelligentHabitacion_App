using Homuai.App.CustomControl;
using Homuai.App.iOS.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LabelJustifyText), typeof(LabelJustifyTextRenderer))]
namespace Homuai.App.iOS.CustomControl
{
    public class LabelJustifyTextRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TextAlignment = UIKit.UITextAlignment.Justified;
            }
        }
    }
}