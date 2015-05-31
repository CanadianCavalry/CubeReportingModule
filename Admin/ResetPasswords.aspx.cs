using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CubeReportingModule.Admin
{
    public partial class ResetPasswords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindUsersToList();

            if (IsPostBack)
            {
                UserResetList.SelectedValue = Request.Form[UserResetList.UniqueID];
            }
        }

        private void BindUsersToList()
        {
             // Get all of the user accounts
            MembershipUserCollection users = Membership.GetAllUsers();
            users.Remove("deus");
            users.Remove(Membership.GetUser().UserName);

            // If the logged in user is not a SysAdmin, hide all the Admin users
            if (!Roles.IsUserInRole("SysAdmin"))
            {
                string[] admins = Roles.GetUsersInRole("Admin");
                foreach (string userName in admins)
                {
                    users.Remove(userName);
                }
            }
            UserResetList.DataSource = users;
            UserResetList.DataBind();
        }

        protected void ResetPasswordButton_Click(object sender, EventArgs e)
        {
            MembershipUser selectedUser = Membership.GetUser(UserResetList.SelectedItem.Text);
            selectedUser.ResetPassword();
        }
    }
}