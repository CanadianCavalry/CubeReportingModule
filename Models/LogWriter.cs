using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CubeReportingModule.Models
{
    public static class LogWriter
    {
        public static readonly string login = "logged in";
        public static readonly string logout = "logged out";
        public static readonly string manageUsersIn = "entered the Manage Users panel";
        public static readonly string manageUsersOut = "left the Manage Users panel";
        public static readonly string createReport = "created a new report";
        public static readonly string modifyReport = "modified report";
        public static readonly string deleteReport = "removed report";
        public static readonly string createEvent = "created a new scheduled event";
        public static readonly string modifyEvent = "modified scheduled event";
        public static readonly string deleteEvent = "removed scheduled event";
        public static readonly string lockAccount = "locked the account of ";
        public static readonly string unlockAccount = "unlocked the account of ";
        public static readonly string createUser = "created the new user account ";
        public static readonly string deleteUser = "deleted the user account ";
        public static readonly string changeRole = "changed the role of user ";
        public static readonly string changePassword = "changed their password";
        public static readonly string changeEmail = "changed their email address";

        public static void createAccessLog(string description)
        {
            GRAAccessLog logFile = new GRAAccessLog();
            logFile.Username = Membership.GetUser().UserName;
            logFile.LogDate = DateTime.Now;
            logFile.Description = description;

            AppContext db = new AppContext();
            db.GRAAccessLogs.Add(logFile);
            db.SaveChanges();
        }
    }
}