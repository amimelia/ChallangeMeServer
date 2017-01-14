using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChallengeMeServer.ChallengeMe.App_Code.DataAccess;
using ChallengeMeServer.Clients;
using ChallengeMeServer.Models;

namespace ChallengeMeServer.ChallangeMe.App_Code.Managers
{
    public class NewsFeedManager
    {
        public static NewsFeedManager Current { get; } = new NewsFeedManager();

        internal TimeLineInfo GetUserTimeLine(Client client, int targetUserId)
        {
            TimeLineInfo timeLineInfo = new TimeLineInfo(DataControllerCore.Current.GetUserPosts(client.UserId,targetUserId));
            return timeLineInfo;
        }
        internal UserInfo GetUserInfo(Client client, int targetUserId)
        {
            TimeLineInfo timeLineInfo = new TimeLineInfo(DataControllerCore.Current.GetUserPosts(client.UserId,targetUserId));
            ProfileInfo profileInfo = new ProfileInfo(DataControllerCore.Current.GetProfile(targetUserId));
            return new UserInfo(timeLineInfo, profileInfo);
        }

        public NewsFeedInfo GetUserNewsFeedInfo(Client client)
        {
            return new NewsFeedInfo(DataControllerCore.Current.GetFollowingPosts(client.UserId));
        }
    }
}