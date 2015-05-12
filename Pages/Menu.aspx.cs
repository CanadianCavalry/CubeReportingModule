using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using CubeReportingModule.Models;
using CubeReportingModule.Cache;

namespace CubeReportingModule.Pages
{
    public partial class Menu : System.Web.UI.Page
    {
        private Repository repo = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Request.Form["report"] != null)
                {
                    string reportId = Request.Form["report"];
                    Session["ReportId"] = reportId;
                    Response.Redirect("ReportOptions.aspx");
                }
            }
        }

        public IEnumerable<Report> GetReports()
        {
            IEnumerable<Report> reports = repo.Reports;
            return reports;
        }
    }
}