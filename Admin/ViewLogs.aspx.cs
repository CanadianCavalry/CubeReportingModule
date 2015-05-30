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

        public IEnumerable<AccessLog> GetAccessLogs()
        {
            IEnumerable<AccessLog> allLogs = repo.AccessLogs;
            return allLogs;
        }

        public IQueryable<AccessLog> GetAccessLogsAsQuery()
        {
            IQueryable<AccessLog> allLogs = GetAccessLogs().AsQueryable<AccessLog>();
            return allLogs;
        }

        public List<AccessLog> GetAccessLogList()
        {
            return GetAccessLogList(int.MaxValue, 0, String.Empty);
        }

        public List<AccessLog> GetAccessLogList(string SortExpression)
        {
            return GetAccessLogList(int.MaxValue, 0, SortExpression);
        }

        public List<AccessLog> GetAccessLogList(int logsPerPage, int pageIndex, string SortExpression)
        {
            List<AccessLog> allLogs = GetAccessLogsAsQuery().ToList();

            string sortBy = SortExpression;
            bool isDesc = false;
            if (SortExpression.ToLowerInvariant().EndsWith(" desc"))
            {
                sortBy = SortExpression.Substring(0, SortExpression.Length - 5);
                isDesc = true;
            }

            List<AccessLog> sortedLogs = new List<AccessLog>();
            IEnumerable<AccessLog> toSort = from log in allLogs
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

            foreach (AccessLog log in toSort)
            {
                sortedLogs.Add(log);
            }

            return sortedLogs;
        }

        //public List<AccessLog> GetPageOfAccessLogs(int logsPerPage, int pageIndex)
        //{
        //    List<AccessLog> pageOfLogs = GetAccessLogsAsQuery()
        //        .Skip((pageIndex - 1) * logsPerPage)
        //        .Take(logsPerPage).ToList();
        //    return pageOfLogs;
        //}

        protected void Display_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //IQueryable<AccessLog> pageOfLogs = GetAccessLogsAsQuery()
            //    .Skip((e.NewPageIndex - 1) * Display.PageSize)
            //    .Take(Display.PageSize);
            Display.PageIndex = e.NewPageIndex;
            DataBind();
            //Display.DataSource = pageOfLogs;
        }
    }
}