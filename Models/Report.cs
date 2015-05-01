using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;

namespace CubeReportingModule.Models
{
    public class Report
    {
        private readonly uint reportId;
        public string name { get; set; }
        private readonly BasicUser creator;
        public List<string> allColumns { get; set; }
        public List<ReportOption> sortingCriteria { get; set; }
        public string eliminationCriterium { get; set; }

        public Report(uint inID, string inName, BasicUser inCreator, List<string> inColumns, List<ReportOption> inSortCriteria, string inElimCriterium)
        {
            reportId = inID;
            name = inName;
            creator = inCreator;
            allColumns = inColumns;
            sortingCriteria = inSortCriteria;
            eliminationCriterium = inElimCriterium;
        }

        public uint ReportId
        {
            get { return reportId; }
        }

        public BasicUser Creator
        {
            get { return creator; }
        }

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