using Homuai.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homuai.Domain.Entity
{
    public class Room : EntityBase
    {
        public string Name { get; set; }
        [ForeignKey("HomeId")]
        public Home Home { get; set; }
        public long HomeId { get; set; }
    }
}
