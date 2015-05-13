using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using CubeReportingModule.Cache;

namespace CubeReportingModule.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string Name { get; set; }
        //public BasicUser Creator { get; set; }
        public string Query { get; set; }
        //public string SelectClause { get; set; }
        //public string FromClause { get; set; }
        //public string WhereClause { get; set; }
        //public List<ReportOption> allOptions { get; set; }
        //public List<ReportOption> sortingCriteria { get; set; }
        //public string eliminationCriterium { get; set; }

        //public Report(uint inID, string inName, BasicUser inCreator, List<string> inColumns, List<ReportOption> inSortCriteria, string inElimCriterium)
        //{
        //    reportId = inID;
        //    name = inName;
        //    creator = inCreator;
        //    allColumns = inColumns;
        //    sortingCriteria = inSortCriteria;
        //    eliminationCriterium = inElimCriterium;
        //}

        //public Report(int inID, string inName, BasicUser inCreator, List<string> inColumns)
        //{
        //    reportId = inID;
        //    name = inName;
        //    creator = inCreator;
        //    allColumns = inColumns;

        //    AppContext db = (AppContext) HttpContext.Current.Application["appContext"];
        //    IEnumerable<ReportOption> allReportOptions = db.ReportOptions;

        //    foreach(ReportOption option in allReportOptions) {
        //        uint optionReportId = option.ReportId;
        //        if (optionReportId != inID)
        //        {
        //            continue;
        //        }

        //        allOptions.Add(option);
        //    }
        //}

        //public int ReportId
        //{
        //    get { return reportId; }
        //}

        //public BasicUser Creator
        //{
        //    get { return creator; }
        //}

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