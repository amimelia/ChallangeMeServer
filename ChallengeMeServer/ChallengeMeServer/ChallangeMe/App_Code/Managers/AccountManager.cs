using ChallengeMeServer.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using ChallengeMeServer.ChallangeMe.App_Code.DataAccess;
using ChallengeMeServer.Controllers.Web;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace ChallengeMeServer.Managers
{
    public class AccountManager
    {
        private Dictionary<Guid, Client> _onlineClients = new Dictionary<Guid, Client>();

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
            return _onlineClients.TryGetValue(tokenKey,out requestedClient) ? requestedClient : InvalidClientToken;
        }


        public Guid CheckSignInValidation(String userName, String password, HttpRequestMessage request)
        {
            var user = DataControllerCore.Current.GetUser(userName, password);
            if (user == null) return InvalidCreditials;
            var tokenKey = Guid.NewGuid();
            _onlineClients.Add(tokenKey, new Client
            {
                UserID = user.UserID,
                IpAddress = HttpRequestHelper.GetClientIpString(request)
            });
            return tokenKey;
        }

        internal void AddUserFollower(Client client, int userToFollowId)
        {
            DataControllerCore.Current.AddUserFollower(client.UserID, userToFollowId);
            // throw new NotImplementedException();
        }

        internal void RemoveUserFollower(Client client, int userToFollowId)
        {
            DataControllerCore.Current.RemoveUserFollower(client.UserID, userToFollowId);
            //throw new NotImplementedException();
        }

        internal void UpdateUserBasicInfo(Client client, string userName, string userFirstName, string userLastName)
        {
            throw new NotImplementedException();
        }

        internal Client GetInfoForUser(Client client, int targetUser)
        {
            throw new NotImplementedException();
        }
    }
}