using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;

namespace CubeReportingModule.Pages
{
    public partial class ViewAccessLogs : System.Web.UI.Page
    {
        private Repository repo = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected IEnumerable<AccessLog> GetLogs()
        {
            IEnumerable<AccessLog> logs = repo.AccessLogs;
            return logs;
        }
    }
}