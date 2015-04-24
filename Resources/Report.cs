using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Resources
{
    public class Report
    {
        public readonly uint reportId { get; set; }
        public string name { get; set; }
        public BasicUser creator { get; set; }
        public List<string> allColumns { get; set; }
        public List<string> sortingCriteria { get; set; }
        public string eliminationCriterium { get; set; }

        public Report(uint inID, string inName, BasicUser inCreator, List<string> inColumns, List<string> inSortCriteria, string inElimCriterium)
        {
            reportId = inID;
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