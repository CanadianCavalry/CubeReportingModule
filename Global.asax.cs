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

namespace CubeReportingModule
{
    public class Global : System.Web.HttpApplication
    {
        private const string timerKey = "timerKey";
        private const string timerPageUrl = @"http://localhost:5099/Admin/TimerRefresh.aspx";
        public const int timerInterval = 5;

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

        private bool StartCacheTimer()
        {
            if (HttpContext.Current.Cache[timerKey] != null)
            {
                return false;
            }

            int timerExpirey = timerInterval;
            if (timerExpirey < 2)
            {
                timerExpirey = 2;
            }

            Debug.WriteLine("Cache Timer started: " + DateTime.Now.ToString());

            HttpContext.Current.Cache.Add(timerKey, "Test", null,
                DateTime.MaxValue, TimeSpan.FromMinutes(timerExpirey),
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
            IEnumerable<GRAScheduledEvent> activeEvents = db.GRAScheduledEvents
                .Where(schedEvent => schedEvent.NextDate == DateTime.Now);
            foreach (GRAScheduledEvent schedEvent in activeEvents)
            {
                schedEvent.sendReport();
            }
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