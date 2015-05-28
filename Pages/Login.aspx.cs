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
    }
}