using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class Repository
    {
        private AppContext context = new AppContext();

        public IEnumerable<GRAReport> GRAReports
        {
            get { return context.GRAReports; }
        }

        public IEnumerable<GRAReportOption> GRAReportOptions
        {
            get { return context.GRAReportOptions; }
        }

        public IEnumerable<GRAAccessLog> GRAAccessLogs
        {
            get { return context.GRAAccessLogs; }
        }

        public IEnumerable<GRAScheduledEvent> GRAScheduledEvents
        {
            get { return context.GRAScheduledEvents; }
        }
    }
}