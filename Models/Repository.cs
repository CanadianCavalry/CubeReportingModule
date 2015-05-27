using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class Repository
    {
        private AppContext context = new AppContext();

        public IEnumerable<Report> Reports
        {
            get { return context.Reports; }
        }

        public IEnumerable<GRAReportOption> GRAReportOptions
        {
            get { return context.GRAReportOptions; }
        }

        public IEnumerable<AccessLog> AccessLogs
        {
            get { return context.AccessLogs; }
        }
    }
}