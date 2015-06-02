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
            BindUsersToResetList();

            if (IsPostBack)
            {
                UserResetList.SelectedValue = Request.Form[UserResetList.UniqueID];
            }

            updateUserDisplay();
            updateLockControls();
        }

        private void BindUsersToResetList()
        {
             // Get all of the user accounts
            MembershipUserCollection users = Membership.GetAllUsers();
            // Remove the current user and the sysadmin account
            users.Remove("deus");
            users.Remove(Membership.GetUser().UserName);

            // If the logged in user is not a SysAdmin, hide all the Admin users
            if (!Roles.IsUserInRole("SysAdmin"))
            {
                users = removeAdmins(users);
            }
            // Populate the dropdown box
            UserResetList.DataSource = users;
            UserResetList.DataBind();
        }

        private MembershipUserCollection removeAdmins(MembershipUserCollection userList)
        {
            string[] admins = Roles.GetUsersInRole("Admin");
            foreach (string userName in admins)
            {
                userList.Remove(userName);
            }
            return userList;
        }

        private MembershipUserCollection removeUnapprovedUsers(MembershipUserCollection userList)
        {
            foreach (MembershipUser user in userList)
            {
                if (!user.IsApproved)
                {
                    userList.Remove(user.UserName);
                }
            }
            return userList;
        }

        protected void ResetPasswordButton_Click(object sender, EventArgs e)
        {
            MembershipUser selectedUser = Membership.GetUser(UserResetList.SelectedItem.Text);
            MembershipProvider adminProvider = Membership.Providers["AdminMembershipProvider"];
            string newPassword = adminProvider.ResetPassword(selectedUser.UserName, null);
            ActionStatus.Text = string.Format("User {0}'s password was reset. The new password is {1}.\nPlease record this before exiting this screen.", selectedUser.UserName, newPassword);
        }

        protected void LockAccountButton_Click(object sender, EventArgs e)
        {
            MembershipUser selectedUser = Membership.GetUser(UserResetList.SelectedItem.Text);
            MembershipProvider adminProvider = Membership.Providers["AdminMembershipProvider"];
            if (!selectedUser.IsApproved)
            {
                ActionStatus.Text = string.Format("The account belonging to user {0} is already suspended.", selectedUser);
            }
            else
            {
                selectedUser.IsApproved = false;
                Membership.UpdateUser(selectedUser);
                ActionStatus.Text = string.Format("The account belonging to user {0} has been suspended.\nThey will be unable to log in until the suspension is lifted.", selectedUser);
            }
            updateLockControls();
        }
        
        protected void UnlockAccountButton_Click(object sender, EventArgs e)
        {
            MembershipUser selectedUser = Membership.GetUser(UserResetList.SelectedItem.Text);
            MembershipProvider adminProvider = Membership.Providers["AdminMembershipProvider"];
            if (selectedUser.IsApproved)
            {
                ActionStatus.Text = string.Format("The account belonging to user {0} is already unlocked.", selectedUser);
            }
            else
            {
                selectedUser.IsApproved = true;
                Membership.UpdateUser(selectedUser);
                ActionStatus.Text = string.Format("The account belonging to user {0} has been unlocked.\nThey can now log in normally.", selectedUser);
            }
            updateLockControls();
        }

        private void updateLockControls()
        {
            MembershipUser selectedUser = Membership.GetUser(UserResetList.SelectedItem.Text);
            if (selectedUser.IsApproved)
            {
                LockAccountButton.Enabled = true;
                UnlockAccountButton.Enabled = false;
            }
            else
            {
                LockAccountButton.Enabled = false;
                UnlockAccountButton.Enabled = true;
            }
        }

        private void updateUserDisplay()
        {
            MembershipUser selectedUser = Membership.GetUser(UserResetList.SelectedItem.Text);
            UserName.Text = "UserName: " + selectedUser.UserName;
            UserEmail.Text = "Email: " + selectedUser.Email;
            if (selectedUser.IsApproved) 
            {
                UserSuspended.Text = "Suspended: No";
            }      
            else {
                UserSuspended.Text = "Suspended: Yes";
            }
            UserLastLogin.Text = "Last Login: " + selectedUser.LastLoginDate;
            UserLastActivity.Text = "Last Active: " + selectedUser.LastActivityDate;
        }
    }
}