using Xamarin.Forms;

namespace Homuai.App.ValueObjects
{
    public class ColorTransformationLightToWhite_DarkToDark : FFImageLoading.Transformations.TintTransformation
    {
        public ColorTransformationLightToWhite_DarkToDark()
        {
            HexColor = Application.Current.RequestedTheme == OSAppTheme.Light ? "#FFFFFF" : ((Color)Application.Current.Resources["DarkModePrimaryColor"]).ToHex();
            EnableSolidColor = true;
        }
    }
}
