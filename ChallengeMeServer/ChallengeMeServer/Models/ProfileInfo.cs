using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Models
{
    public class ProfileInfo
    {
        // TODO: daemateba velebi
        public string ProfilePicture { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<bool> Gender { get; set; }
    }
}