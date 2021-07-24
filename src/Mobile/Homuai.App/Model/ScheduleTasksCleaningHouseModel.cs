using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class ScheduleTasksCleaningHouseModel : ObservableObject
    {
        public ScheduleTasksCleaningHouseModel()
        {
            Tasks = new ObservableCollection<TaskModel>();
            AvaliableUsersToAssign = new ObservableCollection<UserSimplifiedModel>();
        }

        public string ProfileColor
        {
            get
            {
                return Application.Current.RequestedTheme == OSAppTheme.Dark ? ProfileColorDarkMode : ProfileColorLightMode;
            }
        }
        public string ProfileColorLightMode { get; set; }
        public string ProfileColorDarkMode { get; set; }

        public string Name { get; set; }
        public int AmountOfTasks { get; set; }

        public DateTime Date { get; set; }
        public ObservableCollection<TaskModel> Tasks { get; set; }
        public ObservableCollection<UserSimplifiedModel> AvaliableUsersToAssign { get; set; }
    }
}
