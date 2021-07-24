using Homuai.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homuai.Domain.Entity
{
    [Table("EmergencyContact")]
    public class EmergencyContact : EntityBase
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Phonenumber { get; set; }
        public long UserId { get; set; }
    }
}
