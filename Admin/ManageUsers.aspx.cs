using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CubeReportingModule.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bind the users and roles 
            BindUsersToUserList();
            BindUserRolesToList();
 
        }

        private void BindUsersToUserList()
        {
            // Get all of the user accounts 
            MembershipUserCollection users = Membership.GetAllUsers();
            UserList.DataSource = users;
            UserList.DataBind();
        }

        private void BindUserRolesToList()
        {
            // Get all of the roles 
            string[] roles = Roles.GetRolesForUser("admin");
            UsersRoleList.DataSource = roles;
            UsersRoleList.DataBind();
        }

        private void BindRolesToList()
        {
            string[] roles = Roles.GetAllRoles();
            RoleList.DataSource = roles;
            RoleList.DataBind();
        }

        //private void CheckRolesForSelectedUser()
        //{
        //    // Determine what roles the selected user belongs to 
        //    string selectedUserName = UserList.SelectedValue;
        //    string[] selectedUsersRoles = Roles.GetRolesForUser(selectedUserName);

        //    // Loop through the Repeater's Items and check or uncheck the checkbox as needed 

        //    foreach (RepeaterItem ri in UsersRoleList.Items)
        //    {
        //        // Programmatically reference the CheckBox 
        //        CheckBox RoleCheckBox = ri.FindControl("RoleCheckBox") as CheckBox;
        //        // See if RoleCheckBox.Text is in selectedUsersRoles 
        //        if (selectedUsersRoles.Contains<string>(RoleCheckBox.Text))
        //            RoleCheckBox.Checked = true;
        //        else
        //            RoleCheckBox.Checked = false;
        //    }
        //}

        protected void UserList_SelectedIndexChanged1(object sender, EventArgs e)
        {
            
        }
    }
}