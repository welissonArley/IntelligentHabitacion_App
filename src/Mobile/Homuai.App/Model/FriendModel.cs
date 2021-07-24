using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XLabs.Data;

namespace Homuai.App.Model
{
    public class FriendModel : ObservableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> Phonenumbers { get; set; }
        public IList<EmergencyContactModel> EmergencyContacts { get; set; }
        public string ProfileColor
        {
            get
            {
                return Application.Current.RequestedTheme == OSAppTheme.Dark ? ProfileColorDarkMode : ProfileColorLightMode;
            }
        }
        public DateTime JoinedOn { get; set; }
        public string DescriptionDateJoined { get; set; }

        public string ProfileColorLightMode { get; set; }
        public string ProfileColorDarkMode { get; set; }
    }
}
