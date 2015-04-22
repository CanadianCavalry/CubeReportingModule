using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class FileItem
    {
        public readonly string name { get; set; }
        public string description { get; set; }

        public FileItem(string inName, string inDescription)
        {
            name = inName;
            description = inDescription;
        }
    }
}