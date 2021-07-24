using Homuai.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Homuai.Domain.Entity
{
    [Table("CleaningTasksCompleted")]
    public class CleaningTasksCompleted : EntityBase
    {
        [NotMapped]
        public int AverageRating { get => Ratings != null && Ratings.Any() ? (int)Math.Round(Ratings.Average(c => c.Rating)) : -1; }
        [ForeignKey("CleaningScheduleId")]
        public long CleaningScheduleId { get; set; }
        public CleaningSchedule CleaningSchedule { get; set; }
        public IList<CleaningRating> Ratings { get; set; }
    }
}
