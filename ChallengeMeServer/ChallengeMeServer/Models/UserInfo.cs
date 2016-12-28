using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChallengeMeServer.Clients;

namespace ChallengeMeServer.Models
{
    public class UserInfo
    {
        public UserInfo(FeedInfo feedInfo,ProfileInfo profileInfo)
        {
            NewsFeed = feedInfo;
            Profile = profileInfo;
        }
        public FeedInfo NewsFeed { get; set; }

        public ProfileInfo Profile { get; set; }
    }
}