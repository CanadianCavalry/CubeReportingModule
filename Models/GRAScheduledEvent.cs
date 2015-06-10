using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace CubeReportingModule.Models
{
    [Table("GRAScheduledEvents")]
    public class GRAScheduledEvent
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public int ReportId { get; set; }
        [Required]
        public TimeSpan TimeInterval { get; set; }
        [Required]
        public DateTime NextDate { get; set; }
        public string Creator { get; set; }
        public string Recipients { get; set; }   //json string
        public string ReportName { get { return GetReportName(); } }

        public string GetReportName()
        {
            Repository repo = new Repository();
            GRAReport assocReport = repo.GRAReports.Where(report => report.ReportId == ReportId)
                .FirstOrDefault();
            string reportName = assocReport.Name;
            return reportName;
        }

        public List<string> GetRecipientList()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<string> listOfRecipients = json.Deserialize<List<string>>(Recipients);

            return listOfRecipients;
        }

        public void SetRecipientList(List<string> allRecipients)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            string jsonString = json.Serialize(allRecipients);
            Recipients = jsonString;
        }

        public void addRecipient(string recipientToAdd)
        {
            List<string> listOfRecipients = GetRecipientList();
            listOfRecipients.Add(recipientToAdd);

            JavaScriptSerializer json = new JavaScriptSerializer();
            string jsonString = json.Serialize(listOfRecipients);
            Recipients = jsonString;
        }

        public void removeRecipient(string recipientToRemove)
        {
            List<string> listOfRecipients = GetRecipientList();
            bool contains = listOfRecipients.Contains(recipientToRemove);
            if (contains == false)
            {
                return;
            }

            listOfRecipients.Remove(recipientToRemove);

            JavaScriptSerializer json = new JavaScriptSerializer();
            string jsonString = json.Serialize(listOfRecipients);
            Recipients = jsonString;
        }

        private void SetNextDate()
        {
            var days = TimeInterval.Days;
            var hours = TimeInterval.Hours;
            var minutes = TimeInterval.Minutes;

            DateTime next = DateTime.Now.AddDays(days).AddHours(hours).AddMinutes(minutes);
            NextDate = next;
        }

        public void sendReport()
        {

            SetNextDate();
        }
    }
}