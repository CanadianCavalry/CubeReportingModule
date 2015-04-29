using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CubeReportingModule.Resources
{
    public class AppContext : DbContext
    {
        public DbSet<BasicUser> Users { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public DbSet<AccessLog> AccessLogs { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ScheduledEvent> ScheduledEvents { get; set; }
    }
}