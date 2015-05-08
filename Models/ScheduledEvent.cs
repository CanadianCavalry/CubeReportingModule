using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CubeReportingModule.Models
{
    [Table("ScheduledEvents")]
    public class ScheduledEvent
    {
        [Key]
        public int EventId { get; set; }
        public int ReportId { get; set; }
        //public Report attachedReport { get; set; }
        public DateTime TimeInterval { get; set; }
        //public List<string> allRecipients { get; set; }

        //public ScheduledEvent(int inId, Report inReport, DateTime inInterval, List<string> inRecipients)
        //{
        //    eventId = inId;
        //    attachedReport = inReport;
        //    timeInterval = inInterval;
        //    allRecipients = inRecipients;
        //}

        //public uint EventId
        //{
        //    get { return eventId; }
        //}

        //public void addRecipient(string toAdd)
        //{
        //    allRecipients.Add(toAdd);
        //}

        //public void removeRecipient(string toRemove)
        //{
        //    string toFind = allRecipients.Find(toCheck => toCheck == toRemove);
        //    allRecipients.Remove(toFind);
        //}

        public void sendReport()
        {
        }
    }
}