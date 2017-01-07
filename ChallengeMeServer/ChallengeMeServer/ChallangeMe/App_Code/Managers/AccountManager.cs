using ChallengeMeServer.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using ChallengeMeServer.ChallengeMe.App_Code.DataAccess;
using ChallengeMeServer.Controllers.Web;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using ChallengeMeServer.Models;
using System.Text.RegularExpressions;

namespace ChallengeMeServer.Managers
{
    public class AccountManager
    {
        private readonly Dictionary<Guid, Client> _onlineClients = new Dictionary<Guid, Client>();

        #region StaticConstants

        public static Client InvalidClientToken = new Client();
        public static Guid InvalidCreditials = new Guid("00000000-0000-0000-0000-000000000000");

        #endregion


        #region Singleton

        public static AccountManager Current { get; } = new AccountManager();

        #endregion

        public Client GetClientByToken(Guid tokenKey)
        {
            Client requestedClient;
            return _onlineClients.TryGetValue(tokenKey, out requestedClient) ? requestedClient : InvalidClientToken;
        }


        public Guid CheckSignInValidation(string userName, string password, HttpRequestMessage request)
        {
            var user = DataControllerCore.Current.GetUser(userName, password);
            if (user == null) return InvalidCreditials;
            var tokenKey = Guid.NewGuid(); //dasatestia
            _onlineClients.Add(tokenKey, new Client
            {
                UserId = user.UserID,
                IpAddress = HttpRequestHelper.GetClientIpString(request)
            });
            return tokenKey;
        }

        internal void AddUserFollower(Client client, int userToFollowId)
        {
            DataControllerCore.Current.AddUserFollower(client.UserId, userToFollowId);
            // throw new NotImplementedException();
        }

        internal void RemoveUserFollower(Client client, int userToFollowId)
        {
            DataControllerCore.Current.RemoveUserFollower(client.UserId, userToFollowId);
            //throw new NotImplementedException();
        }



        internal void UpdateUserBasicInfo(Client client, string userName, string userFirstName, string userLastName)
        {
            throw new NotImplementedException();
        }

        internal UserInfo GetUserInfo(Client client, int targetUserId)
        {
            FeedInfo feedInfo = new FeedInfo(DataControllerCore.Current.GetPostsForUser(targetUserId));
            ProfileInfo profileInfo = new ProfileInfo(DataControllerCore.Current.GetProfile(targetUserId));
            return new UserInfo(feedInfo, profileInfo);
        }

        internal List<UserSearchResultInfo> GetSearchResults(string searchRequest)
        {
            List<UserSearchResultInfo> searchResults = new List<UserSearchResultInfo>();
            searchRequest = searchRequest.Trim();  // ikidebs tavshi da boloshi spacebs
            int numberOfSpaces = Regex.Matches(searchRequest, "[ ]+").Count;  // edzebs space(eb)s ( " " da "   " orive aris erti match)
            if (numberOfSpaces <= 1)
            {
                string[] requests = Regex.Split(searchRequest, "[ ]+");
                requests.ToList().ForEach(request =>
                {
                    List<profile_info> searchResultsForRequest = DataControllerCore.Current.GetSearchResults(request);
                    //dasatestia
                    var searchInfoResultsForRequest = searchResultsForRequest.Select(userProfile =>
                        new UserSearchResultInfo(userProfile));
                    searchResults.AddRange(searchInfoResultsForRequest);
                });
            }
            return searchResults;
        }

        internal void SetLikeToComment(Client client, int targetCommentId)
        {
            DataControllerCore.Current.SetLikeToComment(targetCommentId);
        }
        internal void SetLikeToPost(Client client, int targetPostId)
        {
            DataControllerCore.Current.SetLikeToPost(targetPostId);
        }

        internal void WritePostComment(Client client, int targetPostId, string postCommentContent, string postCommentDescription)
        {
            DataControllerCore.Current.CreatePostComment(client.UserId,targetPostId,postCommentContent,postCommentDescription);
        }
    }
}