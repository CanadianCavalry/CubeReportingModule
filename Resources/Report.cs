using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class Report
    {
        public string name { get; set; }
        public BasicUser creator { get; set; }
        public List<string> allColumns { get; set; }
        public List<string> sortingCriteria { get; set; }
        public string eliminationCriterium { get; set; }

        public Report(string inName, BasicUser inCreator, List<string> inColumns, List<string> inSortCriteria, string inElimCriterium)
        {
            name = inName;
            creator = inCreator;
            allColumns = inColumns;
            sortingCriteria = inSortCriteria;
            eliminationCriterium = inElimCriterium;
        }

        public void refreshData()
        {
        }

        public void sendReports(List<string> toSendTo)
        {
        }

        public void printReport()
        {
        }
    }
}