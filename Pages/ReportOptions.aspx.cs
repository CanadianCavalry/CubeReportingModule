using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;
using System.Diagnostics;

namespace CubeReportingModule.Pages
{
    public partial class ReportOptions : System.Web.UI.Page
    {
        private Repository repo = new Repository();
        public Report pageReport;

        protected void Page_Load(object sender, EventArgs e)
        {
            pageReport = getPageReport();

            if (IsPostBack)
            {
                string query = BuildQuery();
                Debug.Write(query); //debug
                Session["Query"] = query;
                Response.Redirect("ReportDisplayHTML.aspx");
                return;
            }
        }

        private Report getPageReport()
        {
            int reportId = Convert.ToInt32((string)Session["ReportId"]);
            IEnumerable<Report> allReports = repo.Reports;
            Report toGet = allReports.Where(option => option.ReportId == reportId).FirstOrDefault();
            return toGet;
        }

        private string BuildQuery()
        {
            if (pageReport.SelectClause == null || pageReport.SelectClause.Equals(""))
            {
                pageReport.SelectClause = "*";
            }
            string query = "Select " + pageReport.SelectClause;

            query += "\n";

            query += "From " + pageReport.FromClause;
            query += "\n";

            Queue<string> WhereClauses = new Queue<string>();

            if (pageReport.WhereClause != null && ! pageReport.WhereClause.Equals(""))
            {
                WhereClauses.Enqueue(pageReport.WhereClause);
            }

            IEnumerable<ReportOption> allReportOptions = GetReportOptions();
            foreach (ReportOption option in allReportOptions)
            {
                string optionName = option.Name;
                string optionValue = Request.Form[optionName];
                if (optionValue == null || optionValue.Equals(""))
                {
                    continue;
                }

                string clauseToAdd = optionName + " " + optionValue;
                WhereClauses.Enqueue(clauseToAdd);
            }

            if(WhereClauses.Count != 0) {
                query += "Where " + WhereClauses.Dequeue();

                while (WhereClauses.Count > 0)
                {
                    query += " And " + WhereClauses.Dequeue();
                }

                query += "\n";
            }

            Queue<string> OrderByClauses = new Queue<string>();

            if (pageReport.OrderByClause != null && ! pageReport.OrderByClause.Equals(""))
            {
                WhereClauses.Enqueue(pageReport.OrderByClause);
            }

            if (OrderByClauses.Count != 0)
            {
                query += "Order By " + OrderByClauses.Dequeue();
                query += " Desc";

                while (OrderByClauses.Count > 0)
                {
                    query += " And " + OrderByClauses.Dequeue();
                    query += " Desc";
                }
            }

            return query;
        }

        public IEnumerable<ReportOption> GetReportOptions()
        {
            int reportId = pageReport.ReportId;
            IEnumerable<ReportOption> allReportOptions = repo.ReportOptions.Where(option => option.ReportId == reportId).OrderBy(option => option.ReportOptionId);
            return allReportOptions;
        }
    }
}