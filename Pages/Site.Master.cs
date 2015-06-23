using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace CubeReportingModule.Pages
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            nav.Visible = HttpContext.Current.Request.IsAuthenticated;
        }

        protected void Menu_Click(object sender, EventArgs e)
        {
            //Create Report page
            Session.Remove("Step");
            Session.Remove("ReportName");
            Session.Remove("TableNames");
            Session.Remove("ColumnNames");
            Session.Remove("Options");
            Session.Remove("Restrictions");
            Session.Remove("FinishedReport");
            Session.Remove("FinishedReportOptions");
            //Manage Scheduled Events page
            Session.Remove("Events");
            Session.Remove("Recipients");
            //Report page
            Session.Remove("ReportId");
            Session.Remove("ReportName");
            Session.Remove("Query");

            Response.Redirect("~/Pages/Menu.aspx");
        }

        protected void Profile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/UserProfile.aspx");
        }
    }
}