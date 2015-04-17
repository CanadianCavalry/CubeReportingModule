using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CubeReportingModule.Pages
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void report1_Click(object sender, EventArgs e)
        {
            Server.Transfer("Report.aspx", false);
        }
    }
}