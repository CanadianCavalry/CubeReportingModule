using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using CubeReportingModule.Resources;

namespace CubeReportingModule.Pages
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AppContext db = new AppContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void reportButton_Click(object sender, CommandEventArgs e)
        {
            var pageName = e.CommandName;
            var pageNumber = e.CommandArgument;
            Server.Transfer("ReportOptions.aspx", false);
        }
    }
}