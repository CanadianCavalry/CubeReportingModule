using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class ScheduledEvent
    {
        private readonly uint eventId;
        public Report attachedReport { get; set; }
        public DateTime timeInterval { get; set; }
        public List<string> allRecipients { get; set; }

        public ScheduledEvent(uint inId, Report inReport, DateTime inInterval, List<string> inRecipients)
        {
            eventId = inId;
            attachedReport = inReport;
            timeInterval = inInterval;
            allRecipients = inRecipients;
        }

        public uint EventId
        {
            get { return eventId; }
        }

        public void addRecipient(string toAdd)
        {
            allRecipients.Add(toAdd);
        }

        public void removeRecipient(string toRemove)
        {
            string toFind = allRecipients.Find(toCheck => toCheck == toRemove);
            allRecipients.Remove(toFind);
        }

        public void sendReport()
        {
        }
    }
}