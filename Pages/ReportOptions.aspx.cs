using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;

namespace CubeReportingModule.Pages
{
    public partial class ReportOptions : System.Web.UI.Page
    {
        private Repository repo = new Repository();
        public int reportId { get; set; }
        public string reportName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Response.Redirect("ReportDisplayHTML.aspx");
            }

            reportId = Convert.ToInt32((string) Session["ReportId"]);

            IEnumerable<Report> allReports = repo.Reports;
            Report pageReport = allReports.Where(option => option.ReportId == reportId).FirstOrDefault();
            reportName = pageReport.Name;
        }

        public IEnumerable<ReportOption> GetReportOptions()
        {
            IEnumerable<ReportOption> allReportOptions = repo.ReportOptions.Where(option => option.ReportId == reportId).OrderBy(option => option.ReportOptionId);
            return allReportOptions;
        }
    }
}