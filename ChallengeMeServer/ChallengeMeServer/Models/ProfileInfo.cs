using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Models
{
    public class ProfileInfo
    {
        // TODO: daemateba velebi
        public ProfileInfo (profile_info profileInfo) {
            Name = profileInfo.Name;
            LastName = profileInfo.LastName;
            BirthDate = profileInfo.BirthDate;
            Gender = profileInfo.Gender;
            ProfilePicture = profileInfo.ProfilePicture;
        }
        public string ProfilePicture { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<bool> Gender { get; set; }
    }
}