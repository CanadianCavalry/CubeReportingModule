using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CubeReportingModule.Pages
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateUserInfo();
        }

        protected void UpdateUserInfo()
        {
            MembershipUser selectedUser = Membership.GetUser();
            ProfileUserName.Text = "UserName: " + selectedUser.UserName;
            ProfileUserEmail.Text = "Email: " + selectedUser.Email;
        }
    }
}