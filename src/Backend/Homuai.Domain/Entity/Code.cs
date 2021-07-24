using Homuai.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homuai.Domain.Entity
{
    [Table("Code")]
    public class Code : EntityBase
    {
        public string Value { get; set; }
        public Enums.CodeType Type { get; set; }
        public long UserId { get; set; }
    }
}
