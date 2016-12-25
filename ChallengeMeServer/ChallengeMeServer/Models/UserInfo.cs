using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChallengeMeServer.Clients;

namespace ChallengeMeServer.Models
{
    public class UserInfo
    {

        public FeedInfo NewsFeed { get; set; }

        public ProfileInfo Profile { get; set; }
    }
}