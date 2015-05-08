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
    }
}