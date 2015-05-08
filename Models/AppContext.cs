using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CubeReportingModule.Models
{
    public class AppContext : DbContext
    {
        public DbSet<BasicUser> GRMUsers { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportOption> ReportOptions { get; set; }
        public DbSet<ScheduledEvent> ScheduledEvents { get; set; }
    }
}