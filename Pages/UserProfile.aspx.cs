using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;

namespace CubeReportingModule.Pages
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UpdateUserData();
            }

            UpdateUserDisplay();
        }

        protected void UpdateUserDisplay()
        {
            MembershipUser selectedUser = Membership.GetUser();
            ProfileUserName.Text = "UserName: " + selectedUser.UserName;
            ProfileUserEmail.Text = "Email: " + selectedUser.Email;
            ProfileActivity.Text = "Last Login: " + selectedUser.LastLoginDate;
        }

        protected void UpdateUserData()
        {
            MembershipProvider adminProvider = Membership.Providers["AdminMembershipProvider"];
            MembershipUser selectedUser = Membership.GetUser();
            string newUserEmail = HttpUtility.HtmlEncode(EmailUpdate.Text);
            string newPassword = HttpUtility.HtmlEncode(NewPasswordText.Text);
            string passwordConfirm = HttpUtility.HtmlEncode(ConfirmNewPasswordText.Text);
            string currentPassword = HttpUtility.HtmlEncode(CurrentPasswordText.Text);

            if (newUserEmail != "")
            {
                if (!Membership.ValidateUser(selectedUser.UserName, currentPassword))
                {
                    ActionStatus.Text = "Incorrect password";
                    return;
                }

                selectedUser.Email = newUserEmail;
                ActionStatus.Text = "Email address successfully changed<br />";
                LogWriter.createAccessLog(LogWriter.changeEmail);
            }

            if (newPassword != "")
            {
                if (!newPassword.Equals(passwordConfirm))
                {
                    ActionStatus.Text = "Password confirmation does not match new password";
                    return;
                }

                if (newPassword.Length < 8)
                {
                    ActionStatus.Text = "Password must be at least 8 characters long";
                    return;
                }

                if (!Membership.ValidateUser(selectedUser.UserName, currentPassword))
                {
                    ActionStatus.Text = "Incorrect password";
                    return;
                }

                adminProvider.ChangePassword(selectedUser.UserName, currentPassword, newPassword);
                ActionStatus.Text += "Password successfully changed";
                LogWriter.createAccessLog(LogWriter.changePassword);
            }

            Membership.UpdateUser(selectedUser);
        }
    }
}