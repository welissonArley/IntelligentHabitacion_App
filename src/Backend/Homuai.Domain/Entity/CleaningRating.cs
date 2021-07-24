using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homuai.Domain.Entity
{
    [Table("CleaningRating")]
    public class CleaningRating
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Rating { get; set; }
        public string FeedBack { get; set; }
        [ForeignKey("CleaningTaskCompletedId")]
        public CleaningTasksCompleted CleaningTaskCompleted { get; set; }
        public long CleaningTaskCompletedId { get; set; }
    }
}
