using System.Collections.ObjectModel;
using Xamarin.Forms;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class FriendCreateFirstScheduleModel : ObservableObject
    {
        public FriendCreateFirstScheduleModel()
        {
            Tasks = new ObservableCollection<RoomModel>();
        }

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
        public ObservableCollection<RoomModel> Tasks { get; set; }
    }
}
