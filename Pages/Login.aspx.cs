using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using CubeReportingModule.Cache;

namespace CubeReportingModule.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateDefaultSysAdmin();
            CreateTestUsers();
        }

        protected void CreateDefaultSysAdmin()
        {
            MembershipCreateStatus status;      //tracks the success of the CreateUser method
            MembershipUser sysAdmin;            //used to track the default sysadmin account

            //create the Sysadmin account, if it does not exist
            Membership.CreateUser("deus", "exmachina!", "email@email.com", "Really?", "yes really", true, out status);

            //Find the Sysadmin account
            MembershipUserCollection matchingUsers = Membership.FindUsersByName("deus");
            foreach (MembershipUser user in matchingUsers)
            {
                if (user.ToString() == "deus")
                {
                    sysAdmin = user;
                }
            }

            //Add the Sysadmin account to the SysAdmin role, if neccessary
            if (!Roles.IsUserInRole("deus", "SysAdmin"))
            {
                Roles.AddUserToRole("deus", "SysAdmin");
            }
        }

        private void CreateTestUsers()
        {
            MembershipCreateStatus status;      //tracks the success of the CreateUser method

            //create the user account, if it does not exist
            Membership.CreateUser("alice", "aliceiscool", "alice@email.com", "Really?", "yes really", true, out status);
            Membership.CreateUser("dave", "daveiscool", "dave@email.com", "Really?", "yes really", true, out status);
            Membership.CreateUser("fred", "frediscool", "fred@email.com", "Really?", "yes really", true, out status);

             //Find the user "alice"
            MembershipUserCollection matchingUsers = Membership.FindUsersByName("dave");
            foreach (MembershipUser i in matchingUsers)
            {
                if (i.ToString() == "alice")
                {
                    MembershipUser userAlice = i;
                }
            }

            //Add the user "alice" to the BasicUser role, if neccessary
            if (!Roles.IsUserInRole("alice", "BasicUser"))
            {
                Roles.AddUserToRole("alice", "BasicUser");
            }
            // Find the user "dave"
            matchingUsers = Membership.FindUsersByName("dave");
            foreach (MembershipUser i in matchingUsers)
            {
                if (i.ToString() == "dave")
                {
                    MembershipUser userDave = i;
                }
            }

            //Add the user "dave" to the BasicUser role, if neccessary
            if (!Roles.IsUserInRole("dave", "BasicUser"))
            {
                Roles.AddUserToRole("dave", "BasicUser");
            }

            // Find the user "fred"
            matchingUsers = Membership.FindUsersByName("fred");
            foreach (MembershipUser i in matchingUsers)
            {
                if (i.ToString() == "fred")
                {
                    MembershipUser userFred = i;
                }
            }

            //Add the user "fred" to the BasicUser role, if neccessary
            if (!Roles.IsUserInRole("fred", "BasicUser"))
            {
                Roles.AddUserToRole("fred", "BasicUser");
            }
        }
    }
}