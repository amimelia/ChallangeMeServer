using ChallengeMeServer.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using ChallengeMeServer.ChallengeMe.App_Code.DataAccess;
using ChallengeMeServer.Controllers.Web;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using ChallengeMeServer.Models;
using System.Text.RegularExpressions;
using Facebook;
using Microsoft.ApplicationInsights.Web;

namespace ChallengeMeServer.Managers
{
    public class AccountManager
    {
        private readonly Dictionary<Guid, Client> _onlineClients = new Dictionary<Guid, Client>();
        private static readonly int _invalidId = -1;
        #region StaticConstants

        public static Client InvalidClientToken = new Client();
        public static Guid InvalidCreditials = new Guid("00000000-0000-0000-0000-000000000000");

        #endregion


        #region Singleton

        public static AccountManager Current { get; } = new AccountManager();

        #endregion

        private Guid _facebookSignUp(String token, String facebookId, HttpRequestMessage request)
        {
            var client = new FacebookClient(token);
            // dynamic me = client.Get("me/picture?type=large");//mojna zomebitac client.Get(me/picture?width=1000&height=1000)
            //String pictureUrl = me.picture.data.url;
            dynamic me = client.Get("me?fields=birthday");
            DateTime birthday = Convert.ToDateTime(me.birthday);
            me = client.Get("me?field=email");
            String email = me.email.ToString();
            me = client.Get("me?field=gender");
            String gender = me.gender.ToString();
            me = client.Get("me?field=name");
            String fullName = me.name.ToString();
            me = client.Get("me?fields=first_name");
            String firstName = me.first_name.ToString();
            me = client.Get("me?fields=last_name");
            String lastName = me.last_name.ToString();
            //todo gaugzavnos mailze random password ro shecvalos mere.
            int userId = DataControllerCore.Current.AddNewUser(email, "random password", fullName, firstName, lastName, birthday, gender, "picture url");
            DataControllerCore.Current.AddFacebookId(userId, facebookId);
            var tokenKey = Guid.NewGuid();
            _onlineClients.Add(tokenKey, new Client
            {
                UserId = userId,
                IpAddress = HttpRequestHelper.GetClientIpString(request)
            });
            return tokenKey;
        }

        private Guid _facebookSignIn(int userId, HttpRequestMessage request)
        {
            var tokenKey = Guid.NewGuid();
            _onlineClients.Add(tokenKey, new Client
            {
                UserId = userId,
                IpAddress = HttpRequestHelper.GetClientIpString(request)
            });
            return tokenKey;
        }

        public Client GetClientByToken(Guid tokenKey)
        {
            Client requestedClient;
            return _onlineClients.TryGetValue(tokenKey, out requestedClient) ? requestedClient : InvalidClientToken;
        }

        public Guid EmailSignUp(String email, String password, String fullName, String name, String lastName,
            DateTime birthDate, String gender, HttpRequestMessage request)
        {
            int userId = DataControllerCore.Current.AddNewUser(email, password, fullName, name, lastName, birthDate,
                gender, "");
            if (userId == _invalidId)
            {
                return InvalidCreditials;
            }
            else
            {
                var tokenKey = Guid.NewGuid();
                _onlineClients.Add(tokenKey, new Client
                {
                    UserId = userId,
                    IpAddress = HttpRequestHelper.GetClientIpString(request)

                });

                return tokenKey;
            }
        }

        public Guid CheckEmailSignInValidation(String email, String password, HttpRequestMessage request)
        {
            var user = DataControllerCore.Current.GetUser(email, password);
            if (user == null) return InvalidCreditials;
            var tokenKey = Guid.NewGuid();
            _onlineClients.Add(tokenKey, new Client
            {
                UserId = user.UserID,
                IpAddress = HttpRequestHelper.GetClientIpString(request)
            });
            return tokenKey;
        }

        public Guid CheckFacebookAuthenticationValidation(String token, String facebookId, HttpRequestMessage request)
        {
            var user = DataControllerCore.Current.GetUserByFacebookId(facebookId);//todo gasaarkvevia facebookis id ra sigrdzisaa stringad shevinaxot bazashi tu intad
            return user == null ? _facebookSignUp(token, facebookId, request) : _facebookSignIn(user.UserID, request);
        }

        internal void AddUserFollower(Client client, int userToFollowId)
        {
            DataControllerCore.Current.AddUserFollower(client.UserId, userToFollowId);
        }

        internal void RemoveUserFollower(Client client, int userToFollowId)
        {
            DataControllerCore.Current.RemoveUserFollower(client.UserId, userToFollowId);
        }



        internal void UpdateUserBasicInfo(Client client, String userName, String userFirstName, String userLastName)
        {
            throw new NotImplementedException();
        }

        

        internal List<UserSearchResultInfo> GetSearchResults(String searchRequest)
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
            DataControllerCore.Current.CreatePostComment(client.UserId, targetPostId, postCommentContent, postCommentDescription);
        }
    }
}