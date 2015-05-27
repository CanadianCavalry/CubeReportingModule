using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CubeReportingModule.Models
{
    [Table("GRMUsers")]
    public class BasicUser
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        protected bool AdminFlag { get; set; }
        protected bool ClientFlag { get; set; }
        public bool FirstLoginFlag { get; set; }
        public bool SuspendedFlag { get; set; }
        public DateTime SuspendedUntil { get; set; }

        //public BasicUser(string inUsername, string inEmail)
        //{
        //    username = inUsername;
        //    email = inEmail;
        //    isAdmin = false;
        //    isClient = false;
        //    firstLogin = true;
        //    isSuspended = false;
        //}

        //public string Username
        //{
        //    get { return username; }
        //}

        //public bool IsAdmin
        //{
        //    get { return isAdmin; }
        //}

        //public bool IsClient
        //{
        //    get { return isClient; }
        //}

        public void login(string providedUsername, string providedPassword)
        {
        }

        public void logout()
        {
        }

        public void resetPassword()
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
            SuspendedFlag = true;
            SuspendedUntil = suspendUntil;
        }

        public void unsuspendAccount()
        {
            SuspendedFlag = false;
        }
    }
}