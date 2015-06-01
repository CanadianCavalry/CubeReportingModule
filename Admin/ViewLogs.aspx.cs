using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;
using System.Data;

namespace CubeReportingModule.Admin
{
    public partial class ViewLogs : System.Web.UI.Page
    {
        private Repository repo = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Display.Sort("LogDate", SortDirection.Descending);
            }
        }

        public IEnumerable<GRAAccessLog> GetAccessLogs()
        {
            IEnumerable<GRAAccessLog> allLogs = repo.GRAAccessLogs;
            return allLogs;
        }

        public IQueryable<GRAAccessLog> GetAccessLogsAsQuery()
        {
            IQueryable<GRAAccessLog> allLogs = GetAccessLogs().AsQueryable<GRAAccessLog>();
            return allLogs;
        }

        public List<GRAAccessLog> GetAccessLogList()
        {
            return GetAccessLogList(int.MaxValue, 0, String.Empty);
        }

        public List<GRAAccessLog> GetAccessLogList(string SortExpression)
        {
            return GetAccessLogList(int.MaxValue, 0, SortExpression);
        }

        public List<GRAAccessLog> GetAccessLogList(int logsPerPage, int pageIndex, string SortExpression)
        {
            List<GRAAccessLog> allLogs = GetAccessLogsAsQuery().ToList();

            string sortBy = SortExpression;
            bool isDesc = false;
            if (SortExpression.ToLowerInvariant().EndsWith(" desc"))
            {
                sortBy = SortExpression.Substring(0, SortExpression.Length - 5);
                isDesc = true;
            }

            List<GRAAccessLog> sortedLogs = new List<GRAAccessLog>();
            IEnumerable<GRAAccessLog> toSort = from log in allLogs
                                            select log;

            switch (sortBy)
            {
                case "Username":
                    toSort = isDesc ? toSort.OrderByDescending(log => log.Username) : toSort.OrderBy(log => log.Username);
                    break;

                case "Description":
                    toSort = isDesc ? toSort.OrderByDescending(log => log.Description) : toSort.OrderBy(log => log.Description);
                    break;

                case "LogDate":
                    toSort = isDesc ? toSort.OrderByDescending(log => log.LogDate) : toSort.OrderBy(log => log.LogDate);
                    break;
            }

            foreach (GRAAccessLog log in toSort)
            {
                sortedLogs.Add(log);
            }

            return sortedLogs;
        }

        //public List<GRAAccessLog> GetPageOfAccessLogs(int reportsPerPage, int pageIndex)
        //{
        //    List<GRAAccessLog> pageOfLogs = GetAccessLogsAsQuery()
        //        .Skip((pageIndex - 1) * reportsPerPage)
        //        .Take(reportsPerPage).ToList();
        //    return pageOfLogs;
        //}

        protected void Display_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //IQueryable<GRAAccessLog> pageOfLogs = GetAccessLogsAsQuery()
            //    .Skip((e.NewPageIndex - 1) * Display.PageSize)
            //    .Take(Display.PageSize);
            Display.PageIndex = e.NewPageIndex;
            DataBind();
            //Display.DataSource = pageOfLogs;
        }
    }
}