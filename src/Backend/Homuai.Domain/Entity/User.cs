using Homuai.Domain.ValueObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homuai.Domain.Entity
{
    [Table("User")]
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileColorLightMode { get; set; }
        public string ProfileColorDarkMode { get; set; }
        public string PushNotificationId { get; set; }
        public ICollection<Phonenumber> Phonenumbers { get; set; }
        public ICollection<EmergencyContact> EmergencyContacts { get; set; }

        public long? HomeAssociationId { get; set; }
        public HomeAssociation HomeAssociation { get; set; }

        public bool IsAdministrator()
        {
            return HomeAssociation != null && HomeAssociation.Home.AdministratorId == Id;
        }
        public bool IsPartOfHome()
        {
            return HomeAssociationId.HasValue;
        }
    }
}
