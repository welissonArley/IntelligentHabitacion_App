using System.Collections.Generic;
using System.Collections.ObjectModel;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class CreateScheduleCleaningHouseModel : ObservableObject
    {
        public CreateScheduleCleaningHouseModel()
        {
            Rooms = new List<RoomModel>();
            Friends = new ObservableCollection<FriendCreateFirstScheduleModel>();
        }

        public IList<RoomModel> Rooms { get; set; }
        public ObservableCollection<FriendCreateFirstScheduleModel> Friends { get; set; }
    }
}
