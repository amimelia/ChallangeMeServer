using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChallengeMeServer.Clients;

namespace ChallengeMeServer.Models
{
    public class UserInfo
    {
        public UserInfo(TimeLineInfo timeLineInfo,ProfileInfo profileInfo)
        {
            TimeLine = timeLineInfo;
            Profile = profileInfo;
        }
        public TimeLineInfo TimeLine { get; set; }

        public ProfileInfo Profile { get; set; }
    }
}