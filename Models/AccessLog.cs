using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class AccessLog
    {
        private readonly string description;
        private readonly DateTime date;
        private readonly BasicUser user;

        public AccessLog(string inDescription, DateTime inDate, BasicUser inUser)
        {
            description = inDescription;
            date = inDate;
            user = inUser;
        }

        public string Description 
        {
            get { return description; }
        }

        public DateTime Date
        {
            get { return date; }
        }

        public BasicUser User
        {
            get { return user; }
        }
    }
}