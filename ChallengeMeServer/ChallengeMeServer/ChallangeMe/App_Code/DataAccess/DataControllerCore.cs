using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace ChallengeMeServer.ChallangeMe.App_Code.DataAccess
{
    public class DataControllerCore
    {
        public static DataControllerCore Current { get; } = new DataControllerCore();

        private Object _lockObject = new Object();

        public List<user> GetUsers()
        {
            using (var db = new ChallengeMeEntities())
            {
                return db.users.ToList();
            }
        }

        public user GetUser(int userId)
        {
            using (var db = new ChallengeMeEntities())
            {
                return db.users.FirstOrDefault(x => x.UserID == userId);
            }
        }

        public void RegisterNewUser()
        {
            
        }
    }
}