using CoreAnimation;
using CoreGraphics;
using Homuai.App.CustomControl;
using Homuai.App.iOS.CustomControl;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryForSearchByName), typeof(EntryForSearchByNameRenderer))]
namespace Homuai.App.iOS.CustomControl
{
    public class EntryForSearchByNameRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);
			if (Control == null || e.NewElement == null) return;

			CALayer _line = new CALayer
			{
				BorderColor = ColorToEntryLine(),
				BackgroundColor = ColorToEntryLine(),
				Frame = new CGRect(0, Frame.Height / 2, UIScreen.MainScreen.Bounds.Width - 40, 1f)
			};
			Control.BorderStyle = UITextBorderStyle.None;
			Control.Layer.AddSublayer(_line);
			Control.TintColor = GetCursor();
		}

		private CGColor ColorToEntryLine()
		{
			return UIColor.Clear.CGColor;
		}

		private UIColor GetCursor()
		{
			return Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark ? UIColor.White : UIColor.Black;
		}
	}
}