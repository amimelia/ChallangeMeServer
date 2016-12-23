using ChallengeMeServer.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Managers
{
    public class AccauntManager
    {

        #region StaticConstants

        public static Client INVALID_CLIENT_TOKEN = new Client();

        #endregion


        #region Singleton

        static readonly AccauntManager _accountManager = new AccauntManager();
        public static AccauntManager Current
        {
            get
            {
                return _accountManager;
            }
        }
        #endregion

        public Client GetClientByToken(Guid tokenKey)
        {
            return INVALID_CLIENT_TOKEN;
        }
    }
}