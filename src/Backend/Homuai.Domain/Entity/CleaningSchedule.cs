using Homuai.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homuai.Domain.Entity
{
    [Table("CleaningSchedule")]
    public class CleaningSchedule : EntityBase
    {
        public DateTime ScheduleStartAt { get; set; }
        public DateTime? ScheduleFinishAt { get; set; }
        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public User User { get; set; }
        public long HomeId { get; set; }
        public string Room { get; set; }
        public IList<CleaningTasksCompleted> CleaningTasksCompleteds { get; set; }
    }
}
