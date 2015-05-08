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
        }

        public void reportButton_Click()
        {
            HttpContext.Current.RewritePath("ReportOptions.aspx");
            //Server.Transfer("ReportOptions.aspx", false);
        }

        //public void reportButton_Click(object sender, CommandEventArgs e)
        //{
        //    var pageName = e.CommandName;
        //    var pageNumber = e.CommandArgument;
        //    Server.Transfer("ReportOptions.aspx", false);
        //}

        public IEnumerable<Report> GetReports()
        {
            //Report test = new Report();
            //test.ReportId = 1;
            //test.Name = "Test Report";
            //List<Report> list = new List<Report>();
            //list.Add(test);
            //IEnumerable<Report> reports = list;
            IEnumerable<Report> reports = repo.Reports;
            return reports;
        }
    }
}