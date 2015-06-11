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
            UpdateUserDisplay();
        }

        protected void UpdateUserDisplay()
        {
            MembershipUser selectedUser = Membership.GetUser();
            ProfileUserName.Text = "UserName: " + selectedUser.UserName;
            ProfileUserEmail.Text = "Email: " + selectedUser.Email;
        }

        protected void UpdateUserData()
        {
            MembershipProvider adminProvider = Membership.Providers["AdminMembershipProvider"];
            MembershipUser selectedUser = Membership.GetUser();
            string newUserEmail = EmailUpdate.Text;
            string newPassword = NewPasswordText.Text;
            string passwordConfirm = ConfirmNewPasswordText.Text;
            string currentPassword = CurrentPasswordText.Text;

            if (newUserEmail != "")
            {
                selectedUser.Email = newUserEmail;
            }

            if (newPassword != "")
            {
                if (!newPassword.Equals(passwordConfirm))
                {
                    ActionStatus.Text = "Password confirmation does not match new password";
                    return;
                }

                if (!Membership.ValidateUser(selectedUser.UserName, currentPassword))
                {
                    ActionStatus.Text = "Incorrect password";
                    return;
                }

                if (!newPassword.Equals(passwordConfirm))
                {
                    ActionStatus.Text = "Password confirmation does not match new password";
                    return;
                }

                adminProvider.ChangePassword(selectedUser.UserName, currentPassword, newPassword);
            }

            Membership.UpdateUser(selectedUser);
        }
    }
}