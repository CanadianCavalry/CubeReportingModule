using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class FileItem
    {
        private readonly string name;
        public string description { get; set; }

        public FileItem(string inName, string inDescription)
        {
            name = inName;
            description = inDescription;
        }

        public string Name
        {
            get { return name; }
        }
    }
}