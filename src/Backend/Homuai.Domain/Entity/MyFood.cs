using Homuai.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homuai.Domain.Entity
{
    [Table("MyFood")]
    public class MyFood : EntityBase
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Manufacturer { get; set; }
        public Enums.Type Type { get; set; }
        public DateTime? DueDate { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
