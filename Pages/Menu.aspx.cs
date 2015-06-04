using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using CubeReportingModule.Models;
using CubeReportingModule.Cache;
using System.Web.Security;

namespace CubeReportingModule.Pages
{
    public partial class Menu : System.Web.UI.Page
    {
        private Repository repo = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {
            AdminPane.Visible = false;

            if (IsPostBack)
            {
                if (Request.Form["Report"] != null)
                {
                    string reportId = String.Format("{0}", Global.CleanInput(Request.Form["Report"]));
                    Session["ReportId"] = reportId;
                    Response.Redirect("ReportOptions.aspx");
                    return;
                }
            }

            if ((Roles.IsUserInRole("Admin")) || (Roles.IsUserInRole("SysAdmin"))){
                AdminPane.Visible = true;
            }
            
        }

        public IEnumerable<GRAReport> GetReports()
        {
            IEnumerable<GRAReport> reports = repo.GRAReports.OrderBy(report => report.Name);
            return reports;
        }

        protected void CreateReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateReport.aspx");
        }

        protected void Accounts_Click(object sender, EventArgs e)
        {
            LogWriter.createAccessLog(LogWriter.manageUsersIn);
            Response.Redirect("~/Admin/ManageUsers.aspx");
        }

        protected void AddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AddUser.aspx");
        }

        protected void Logs_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ViewLogs.aspx");
        }

        protected void ResetPasswords_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ResetPasswords.aspx");
        }

        protected void Templates_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageReports.aspx");
        }

        protected void Events_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageScheduledEvents.aspx");
        }
    }
}
