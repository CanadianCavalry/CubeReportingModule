using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CubeReportingModule.Admin
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindUsersToList();
        }

        protected void BindUsersToList()
        {
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
            DeleteUserList.DataSource = users;
            DeleteUserList.DataBind();
        }

        protected void RemoveUser_Click(object sender, EventArgs e)
        {
            Membership.DeleteUser(DeleteUserList.SelectedItem.Text);
        }
    }
}