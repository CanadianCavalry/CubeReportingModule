using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class BasicUser
    {
        public readonly string username { get; set; }
        public string password { get; set; }
        public List<SecurityQuestion> allSecurityQuestions { get; set; }
        public string email { get; set; }
        public readonly bool isAdmin { get; set; }
        public readonly bool isClient { get; set; }
        public bool firstLogin { get; set; }
        public bool isSuspended { get; set; }
        public DateTime suspendedUntil { get; set; }

        public BasicUser(string inUsername, string inEmail, bool setAdmin, bool setClient)
        {
            username = inUsername;
            email = inEmail;
            isAdmin = setAdmin;
            isClient = setClient;
            firstLogin = true;
            isSuspended = false;
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