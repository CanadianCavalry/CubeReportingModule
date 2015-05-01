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
        protected void Application_Start(object sender, EventArgs e)
        {
        }

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
            AppContext db = (AppContext) HttpContext.Current.Application["appContext"];
            IEnumerable<Report> reports = db.Reports;
            return reports;
        }
    }
}