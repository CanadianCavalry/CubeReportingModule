using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CubeReportingModule.Models
{
    public class AppContext : DbContext
    {
        public DbSet<GRAAccessLog> GRAAccessLogs { get; set; }
        public DbSet<GRAReport> GRAReports { get; set; }
        public DbSet<GRAReportOption> GRAReportOptions { get; set; }
        public DbSet<GRAScheduledEvent> GRAScheduledEvents { get; set; }
    }
}