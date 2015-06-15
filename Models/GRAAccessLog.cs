using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CubeReportingModule.Models
{
    [Table("GRAAccessLogs")]
    public class GRAAccessLog
    {
        [Key]
        public Int64 LogId { get; set; }
        public string Description { get; set; }
        public DateTime LogDate { get; set; }
        public string Username { get; set; }

        public override string ToString()
        {
            string output = String.Format("{0} {1} at {2}.", Username, Description, LogDate.ToString());
            return output;
        }
    }
}