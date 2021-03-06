﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;

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
            // Create the initial list of users and hide the sysadmin and current user.
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
            LogWriter.createAccessLog(LogWriter.deleteUser + DeleteUserList.SelectedItem.Text);
        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            LogWriter.createAccessLog(LogWriter.createUser + CreateUserWizard1.UserName);
        }
    }
}