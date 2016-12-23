using ChallengeMeServer.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Managers
{
    public class AccountManager
    {
        private Dictionary<Guid,Client> _onlineClients = new Dictionary<Guid, Client>();

        #region StaticConstants

        public static Client InvalidClientToken = new Client();
        public static Guid InvalidCreditials = new Guid("00000000-0000-0000-0000-000000000000");

        #endregion


        #region Singleton

        public static AccountManager Current { get; } = new AccountManager();

        #endregion

        public Client GetClientByToken(Guid tokenKey)
        {
            return InvalidClientToken;
        }

        public Guid CheckSignInValidation(String userName, String password)
        {

            //_onlineClients.Add();
            return Guid.NewGuid();
        }
    }
}