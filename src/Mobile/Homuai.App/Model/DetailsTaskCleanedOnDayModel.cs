using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Homuai.App.Model
{
    public class DetailsTaskCleanedOnDayModel
    {
        public string Id { get; set; }
        public string User { get; set; }
        public int AverageRate { get; set; }
        public bool CanRate { get; set; }
        public DateTime CleanedAt { get; set; }
    }

    public class DetailsTaskCleanedOnDayModelGroup : ObservableCollection<DetailsTaskCleanedOnDayModel>
    {
        public string Room { get; private set; }

        public DetailsTaskCleanedOnDayModelGroup(string room, IList<DetailsTaskCleanedOnDayModel> list) : base(list)
        {
            Room = room;
        }
    }
}
