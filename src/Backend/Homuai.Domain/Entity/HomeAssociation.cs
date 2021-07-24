using Homuai.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homuai.Domain.Entity
{
    [Table("HomeAssociation")]
    public class HomeAssociation : EntityBase
    {
        [ForeignKey("HomeId")]
        public Home Home { get; set; }
        public long HomeId { get; set; }
        public DateTime JoinedOn { get; set; }
        public DateTime? ExitOn { get; set; }
        public decimal MonthlyRent { get; set; }
        public long UserIdentity { get; set; }
    }
}
