using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class BasicUser
    {
        protected readonly string username;
        public string password { get; set; }
        public List<SecurityQuestion> allSecurityQuestions { get; set; }
        public string email { get; set; }
        protected bool isAdmin;
        protected bool isClient;
        public bool firstLogin { get; set; }
        public bool isSuspended { get; set; }
        public DateTime suspendedUntil { get; set; }

        public BasicUser(string inUsername, string inEmail)
        {
            username = inUsername;
            email = inEmail;
            isAdmin = false;
            isClient = false;
            firstLogin = true;
            isSuspended = false;
        }

        public string Username
        {
            get { return username; }
        }

        public bool IsAdmin
        {
            get { return isAdmin; }
        }

        public bool IsClient
        {
            get { return isClient; }
        }

        public void login(string providedUsername, string providedPassword)
        {
        }

        public void logout()
        {
        }

        public void resetPassword()
        {
        }

        public void confirmSecurityAnswers(List<string> allAnswers)
        {
        }

        public void changeSecurityQuestion(SecurityQuestion toChange, SecurityQuestion replacement)
        {
        }

        public void generateReport()
        {
        }

        public void createReport()
        {
        }

        public void createScheduledReport()
        {
        }

        public void removeScheduledReport()
        {
        }

        public void suspendAccount(DateTime suspendUntil)
        {
            isSuspended = true;
            suspendedUntil = suspendUntil;
        }

        public void unsuspendAccount()
        {
            isSuspended = false;
        }
    }
}