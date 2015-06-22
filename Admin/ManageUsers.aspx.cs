using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;

namespace CubeReportingModule.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Populate the drop-down box of users
            BindUsersToUserList();
            if (IsPostBack)
            {
                UserList.SelectedValue = Request.Form[UserList.UniqueID];
                UpdateUserRole();
            }

            UpdateRoleRadioButtons();
        }
        
        private void BindUsersToUserList()
        {
            // Get all of the user accounts
            MembershipUserCollection users = Membership.GetAllUsers();
            // Remove the SysAdmin
            users.Remove("deus");
            // Remove the current user
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
            UserList.DataSource = users;
            UserList.DataBind();
        }

        private void UpdateRoleRadioButtons()
        {
            string userRole = Roles.GetRolesForUser(UserList.SelectedItem.Text)[0];

            // Find the button corresponding to the users role
            ListItem item = RoleList.Items.FindByText(userRole);
            item.Selected = true;
        }

        protected void UpdateUserRole()
        {
            //Get the currently selected user and their roles
            string userName = Membership.GetUser(UserList.SelectedItem.Text).UserName;
            string selectedRole = RoleList.SelectedItem.Text;
            string[] userRoles = Roles.GetRolesForUser(userName);

            // If they are already in that role, return
            if (Roles.IsUserInRole(userName, selectedRole)) 
            {
                return;
            }

            // Remove them from all roles
            Roles.RemoveUserFromRoles(userName, userRoles);

            // Add them to the selected role and display a success message
            Roles.AddUserToRole(userName, RoleList.SelectedItem.Text);
            ActionStatus.Text = string.Format("User {0} was changed to role {1}.", userName, RoleList.SelectedItem.Text);
            LogWriter.createAccessLog(LogWriter.changeRole + userName + " from " + userRoles[0] + " to " + RoleList.SelectedItem.Text);
        }

        protected void UserList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RoleList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}