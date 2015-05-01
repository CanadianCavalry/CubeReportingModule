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
        public Report reportPage;

        protected void Page_Load(object sender, CommandEventArgs e)
        {
            uint pageReportId = (uint) e.CommandArgument;

        }

        public string ReportName()
        {
            return reportPage.name;
        }

        public IEnumerable<ReportOption> allOptions()
        {
            return reportPage.sortingCriteria;
        }
    }
}