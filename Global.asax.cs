using CubeReportingModule.Cache;
using CubeReportingModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Diagnostics;
using System.Web.Caching;
using System.Net;
using System.Text.RegularExpressions;

namespace CubeReportingModule
{
    public class Global : System.Web.HttpApplication
    {
        private const string timerKey = "timerKey";
        private const string timerPageUrl = @"http://localhost:5099/Admin/TimerRefresh.aspx";
        private static TimeSpan timerInterval = new TimeSpan(0, 5, 0);

        protected void Application_Start(object sender, EventArgs e)
        {
            MembershipCreateStatus status;
            Membership.CreateUser("deus", "ExMachina!", "email@email.com", "Really?", "yes really", true, out status);

            string JQueryVer = "1.7.1";
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-" + JQueryVer + ".min.js",
                DebugPath = "~/Scripts/jquery-" + JQueryVer + ".js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".js",
                CdnSupportsSecureConnection = true,
                LoadSuccessExpression = "window.jQuery"
            });

            // Create the internal data model
            WarehouseCache.createWarehouseCache();

            //Create the user roles


            //Setup the timer for GRAScheduledEvents using Cache expiry callback
            StartCacheTimer();
        }

        public static TimeSpan GetTimerInterval()
        {
            return timerInterval;
        }

        private bool StartCacheTimer()
        {
            if (HttpContext.Current.Cache[timerKey] != null)
            {
                return false;
            }

            CheckScheduledEvents();

            TimeSpan timerExpirey = timerInterval;
            if (timerExpirey < new TimeSpan(0, 2, 0))
            {
                timerExpirey = new TimeSpan(0, 2, 0);
            }

            Debug.WriteLine("Cache Timer started: " + DateTime.Now.ToString());

            HttpContext.Current.Cache.Add(timerKey, "Test", null,
                DateTime.MaxValue, timerExpirey,
                CacheItemPriority.Normal,
                new CacheItemRemovedCallback(CacheTimerExpiredCallback));

            return true;
        }

        public void CacheTimerExpiredCallback(string key,
            object value, CacheItemRemovedReason reason)
        {
            Debug.WriteLine("Cache Timer callback: " + DateTime.Now.ToString());

            //Refresh the cache timer
            WebClient client = new WebClient();
            client.DownloadData(timerPageUrl);

            // Do the service work
            CheckScheduledEvents();
        }

        private void CheckScheduledEvents()
        {
            AppContext db = new AppContext();
            List<GRAScheduledEvent> allEvents = db.GRAScheduledEvents.ToList();
            IEnumerable<GRAScheduledEvent> activeEvents = allEvents
                .Where(schedEvent => DateTime.Compare(schedEvent.NextDate, DateTime.Now) <= 0);
            if (activeEvents.Count() == 0)
            {
                return;
            }
            foreach (GRAScheduledEvent schedEvent in activeEvents)
            {
                schedEvent.sendReport();
            }
        }

        public static string CleanInput(string input)
        {
            //char[] specialChars;
            //string cleanOutput = new String(input.Except(specialChars).ToArray());

            //string cleanOutput = Regex.Replace(input, @"[^0-9a-zA-Z\._]", string.Empty);
            string cleanOutput = Regex.Replace(input, @"[^0-9a-zA-Z\._\-]--.*", string.Empty);
            return cleanOutput;
        }

        public static string GetSelectClause(string query)
        {
            string regex = @"(?:select )([^;]*)(?= from)";

            Match match = Regex.Match(query, regex, RegexOptions.IgnoreCase);
            string selectClause = match.Groups[1].Value;
            Debug.WriteLine("Select clause: " + selectClause);  //debug

            return selectClause;
        }

        public static List<string> GetColumnsInSelectClause(string selectClause)
        {
            List<string> keywords = new List<string>();
            keywords.Add(", ");
            keywords.Add(",");

            List<string> allTableNames = BreakStringOnKeywords(selectClause, keywords);
            return allTableNames;
        }

        public static string GetFromClause(string query)
        {
            string regex = @"(?:from )([^;]*)";

            Match match = Regex.Match(query, regex, RegexOptions.IgnoreCase);
            string fromClause = match.Groups[1].Value;

            //Remove where clause
            regex = @"([^;]*)(?=\r?\n?\swhere)";
            Match secondMatch = Regex.Match(fromClause, regex, RegexOptions.IgnoreCase);
            if (secondMatch.Success == false)
            {
                Debug.WriteLine("From clause: " + fromClause);  //debug
                return fromClause;
            }

            fromClause = secondMatch.Groups[1].Value;
            
            Debug.WriteLine("From clause: " + fromClause);  //debug
            return fromClause;
        }

        public static List<string> GetTablesInFromClause(string fromClause)
        {
            List<string> keywords = new List<string>();
            keywords.Add(" join ");
            keywords.Add(" inner join ");
            keywords.Add(" outer join ");
            keywords.Add(" self join ");
            keywords.Add(" using");
            keywords.Add(" on ");
            keywords.Add(@",\s?");
            //keywords.Add(" 1=1");

            string regexPrefix = @"(?<=\s|,|^)";

            List<string> allTableNames = BreakStringOnKeywords(fromClause, keywords, regexPrefix);
            return allTableNames;
        }

        public static string GetWhereClause(string query)
        {
            string regex = @"(?:where )([^;]*)";

            Match match = Regex.Match(query, regex, RegexOptions.IgnoreCase);
            string whereClause = match.Groups[1].Value;
            Debug.WriteLine("From clause: " + whereClause);  //debug

            return whereClause;
        }

        public static List<string> BreakStringOnKeywords(string input, List<string> allKeywordsToIgnoreList)
        {
            return BreakStringOnKeywords(input, allKeywordsToIgnoreList, String.Empty);
        }

        public static List<string> BreakStringOnKeywords(string input, List<string> allKeywordsToIgnoreList, string regexPrefix)
        {
            Queue<string> allKeywordsToIgnoreQueue = new Queue<string>(allKeywordsToIgnoreList);
            string regexSuffix = GetRegex(allKeywordsToIgnoreQueue);
            string regex = regexPrefix + regexSuffix;
            Debug.WriteLine("Regex: " + regex);

            MatchCollection allMatches = Regex.Matches(input, regex, RegexOptions.IgnoreCase);

            if (allMatches.Count == 0)
            {
                return null;
            }

            List<string> fragments = new List<string>();
            int tableNumber = 1;

            foreach (Match match in allMatches)
            {
                string value = match.Groups[1].Value;
                if (value.Equals(String.Empty) == true)
                {
                    continue;
                }
                Debug.WriteLine("Table {0}: ({1})", tableNumber, value);
                tableNumber++;
                fragments.Add(value);
            }

            return fragments;
        }

        private static string GetRegex(Queue<string> allKeywordsToIgnore)
        {
            string regex = @"([\w\-]*)(?=";
            if (allKeywordsToIgnore.Count == 0)
            {
                regex += @"$)";
                return regex;
            }

            string keyword = allKeywordsToIgnore.Dequeue();
            regex += keyword;

            while (allKeywordsToIgnore.Count > 0)
            {
                keyword = allKeywordsToIgnore.Dequeue();
                regex += @"|" + keyword;
            }
            regex += @"|$)";
            return regex;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // If the timer page is hit, then it means we want to restart the cache timer
            if (HttpContext.Current.Request.Url.ToString() == timerPageUrl)
            {
                StartCacheTimer();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}