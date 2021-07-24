using System;

namespace Homuai.Domain.ValueObjects
{
    public class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
    }
}
