using Homuai.Domain.ValueObjects;

namespace Homuai.Domain.Entity
{
    public class Phonenumber : EntityBase
    {
        public string Number { get; set; }
        public long UserId { get; set; }
    }
}
