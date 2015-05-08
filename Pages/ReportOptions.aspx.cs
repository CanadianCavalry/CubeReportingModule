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
        public string reportName;
        private List<ReportOption> allReportOptions;

        protected void Page_Load(object sender, CommandEventArgs e)
        {
            int reportId = (int) Session["ReportId"];
            reportName = repo.Reports.Where(option => option.ReportId == reportId).FirstOrDefault().Name;
            allReportOptions = new List<ReportOption>(repo.ReportOptions.Where(option => option.ReportId == reportId));
        }

        public IEnumerable<ReportOption> allOptions()
        {
            IEnumerable<ReportOption> allOptions = allReportOptions;
            return allOptions;
        }
    }
}