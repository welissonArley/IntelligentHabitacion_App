using CoreGraphics;
using Homuai.App.iOS.CustomControl;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Frame), typeof(AppFrameRenderer))]
namespace Homuai.App.iOS.CustomControl
{
    public class AppFrameRenderer : Xamarin.Forms.Platform.iOS.FrameRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            base.LayoutSubviews();
            this.Layer.ShadowRadius = 2.0f;
            this.Layer.ShadowColor = ShadowColor();
            this.Layer.ShadowOffset = new CGSize(2, 2);
            this.Layer.ShadowOpacity = 1.0f;
            this.Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            this.Layer.MasksToBounds = false;
        }

        private CGColor ShadowColor()
        {
            return Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark ? UIColor.White.CGColor : UIColor.Black.CGColor;
        }
    }
}