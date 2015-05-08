using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class AdminUser : BasicUser
    {
        //public AdminUser(string inUsername, string inEmail)
        //    : base(inUsername, inEmail)
        //{
        //    isAdmin = true;
        //}

        public void resetUserPassword(BasicUser user)
        {
            user.resetPassword();
        }

        public void addNewUser(string username, string email, uint userType)
        {
        }

        public List<AccessLog> getAccessLogs()
        {
            List<AccessLog> allAccessLogs = new List<AccessLog>();

            return allAccessLogs;
        }
    }
}