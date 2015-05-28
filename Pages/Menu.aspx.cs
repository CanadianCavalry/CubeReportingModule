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
            if (IsPostBack)
            {
                if (Request.Form["Report"] != null)
                {
                    string reportId = String.Format("{0}", Server.HtmlDecode(Request.Form["Report"]));
                    Session["ReportId"] = reportId;
                    Response.Redirect("ReportOptions.aspx");
                    return;
                }
            }

            if ((Roles.IsUserInRole("Admin")) || (Roles.IsUserInRole("SysAdmin"))){
                AdminPane.Visible = true;
            }
            
        }

        public IEnumerable<Report> GetReports()
        {
            IEnumerable<Report> reports = repo.Reports;
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

        protected void Templates_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AddTemplate.aspx");
        }
    }
}
