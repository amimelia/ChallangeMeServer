using ChallengeMeServer.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Managers
{
    public class AccountManager
    {

        #region StaticConstants

        public static Client InvalidClientToken = new Client();

        #endregion


        #region Singleton

        public static AccountManager Current { get; } = new AccountManager();

        #endregion

        public Client GetClientByToken(Guid tokenKey)
        {
            return InvalidClientToken;
        }
    }
}