using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CubeReportingModule.Models
{
    public static class LogWriter
    {
        public static readonly string login = "logged in at";
        public static readonly string logout = "logged out at";
        public static readonly string manageUsersIn = "entered the Manage Users panel at";
        public static readonly string manageUsersOut = "left the Manage Users panel at";

        public static void createAccessLog(string description)
        {
            AccessLog logFile = new AccessLog();
            logFile.Username = Membership.GetUser().UserName;
            logFile.LogDate = DateTime.Now;
            logFile.Description = description;

            AppContext db = new AppContext();
            db.AccessLogs.Add(logFile);
            db.SaveChanges();
        }
    }
}