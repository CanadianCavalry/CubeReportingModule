using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class AccessLog
    {
        public readonly string description { get; set; }
        public readonly DateTime date { get; set; }
        public readonly BasicUser user { get; set; }

        public AccessLog(string inDescription, DateTime inDate, BasicUser inUser)
        {
            description = inDescription;
            date = inDate;
            user = inUser;
        }
    }
}