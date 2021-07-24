using System.Collections.ObjectModel;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class TaskModel : ObservableObject
    {
        public TaskModel()
        {
            Assign = new ObservableCollection<UserSimplifiedModel>();
        }
        public string IdTaskToRegisterRoomCleaning { get; set; }
        public string Room { get; set; }
        public bool CanRate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanRegisterRoomCleanedToday { get; set; }
        public ObservableCollection<UserSimplifiedModel> Assign { get; set; }
    }
}
