using Xamarin.Forms;

namespace Homuai.App.CustomControl
{
    public class SkeletonView : BoxView
    {
        public SkeletonView()
        {
            var smoothAnimation = new Animation();

            smoothAnimation.WithConcurrent((f) => Opacity = f, 0.3, 1, Easing.SinIn);
            smoothAnimation.WithConcurrent((f) => Opacity = f, 1, 0.3, Easing.SinIn);

            this.Animate("FadeInOut", smoothAnimation, 16, 1000, Easing.SinIn, null, () => true);

            Color = (Color)Application.Current.Resources["SkeletonColor"];
            CornerRadius = 2;
        }
    }
}
