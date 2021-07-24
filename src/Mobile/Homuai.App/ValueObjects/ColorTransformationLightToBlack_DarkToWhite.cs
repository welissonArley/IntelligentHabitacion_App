using Xamarin.Forms;

namespace Homuai.App.ValueObjects
{
    public class ColorTransformationLightToBlack_DarkToWhite : FFImageLoading.Transformations.TintTransformation
    {
        public ColorTransformationLightToBlack_DarkToWhite()
        {
            HexColor = Application.Current.RequestedTheme == OSAppTheme.Light ? "#000000" : "#FFFFFF";
            EnableSolidColor = true;
        }
    }
}
