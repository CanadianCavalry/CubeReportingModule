using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winnovative;

namespace CubeReportingModule.Pages
{
    public partial class ReportDisplayHTML : System.Web.UI.Page
    {
        public string queryString;
        public string connectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Response.Redirect("ReportDisplayPDF.aspx");
            }

            connectionString = "Data Source=204.174.60.182;Initial Catalog=GainTest;Persist Security Info=True;User ID=Thomas;Password=Coral3dAir";
            queryString = (string)Session["queryString"];
        }

    }
}