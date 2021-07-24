using Homuai.Domain.ValueObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homuai.Domain.Entity
{
    [Table("Home")]
    public class Home : EntityBase
    {
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalAddressInfo { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public CountryAvaliable Country { get; set; }
        public short DeadlinePaymentRent { get; set; }
        public string NetworksName { get; set; }
        public string NetworksPassword { get; set; }
        public long AdministratorId { get; set; }
        public IList<Room> Rooms { get; set; }
    }
}
