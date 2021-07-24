using Xamarin.Forms;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class UserSimplifiedModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileColor
        {
            get
            {
                return Application.Current.RequestedTheme == OSAppTheme.Dark ? ProfileColorDarkMode : ProfileColorLightMode;
            }
        }

        public string ProfileColorLightMode { get; set; }
        public string ProfileColorDarkMode { get; set; }
    }
}
