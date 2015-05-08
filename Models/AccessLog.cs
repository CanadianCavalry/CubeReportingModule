using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CubeReportingModule.Models
{
    [Table("AccessLogs")]
    public class AccessLog
    {
        [Key]
        public int LogId { get; set; }
        public string Description { get; set; }
        public DateTime LogDate { get; set; }
        public BasicUser Username { get; set; }

        //public AccessLog(string inDescription, DateTime inDate, BasicUser inUser)
        //{
        //    description = inDescription;
        //    date = inDate;
        //    user = inUser;
        //}

        //public string Description 
        //{
        //    get { return description; }
        //}

        //public DateTime Date
        //{
        //    get { return date; }
        //}

        //public BasicUser User
        //{
        //    get { return user; }
        //}
    }
}