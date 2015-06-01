using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using CubeReportingModule.Cache;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CubeReportingModule.Models
{
    [Table("GRAReports")]
    public class GRAReport
    {
        [Key]
        public int ReportId { get; set; }
        public string Name { get; set; }
        public string SelectClause { get; set; }
        public string FromClause { get; set; }
        public string WhereClause { get; set; }
        public string Creator { get; set; }

        public void refreshData()
        {
        }

        public void sendReports(List<string> toSendTo)
        {
        }

        public void printReport()
        {
            PrintDocument report = new PrintDocument();
            report.Print();
        }
    }
}