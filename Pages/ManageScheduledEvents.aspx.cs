using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;
using System.Web.Security;
using System.Diagnostics;
using System.Data.Entity;

namespace CubeReportingModule.Pages
{
    public partial class ManageScheduledEvents : System.Web.UI.Page
    {
        Repository repo = new Repository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<GRAScheduledEvent> GetEventsAsQuery()
        {
            List<GRAScheduledEvent> allEvents = repo.GRAScheduledEvents.ToList();
            IQueryable<GRAScheduledEvent> sortedEvents = allEvents
                .OrderBy(schedEvent => schedEvent.ReportName)
                .AsQueryable<GRAScheduledEvent>();
            return sortedEvents;
        }

        public bool SetModifyVisibility(string creator)
        {
            if ((Roles.IsUserInRole("Admin")) || (Roles.IsUserInRole("SysAdmin")))
            {
                return true;
            }

            if (creator == null)
            {
                return false;
            }

            string currentUsername = Membership.GetUser().UserName;

            bool visible = creator.Equals(currentUsername);

            return visible;
        }

        public bool SetDeleteVisibility(string creator)
        {
            if ((Roles.IsUserInRole("Admin")) || (Roles.IsUserInRole("SysAdmin")))
            {
                return true;
            }

            if (creator == null)
            {
                return false;
            }

            string currentUsername = Membership.GetUser().UserName;

            bool visible = creator.Equals(currentUsername);

            return visible;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void Display_UpdateItem(int id)
        {
            GRAScheduledEvent item = null;
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            AppContext db = new AppContext();
            item = db.GRAScheduledEvents.Where(schedEvent => schedEvent.EventId == id).FirstOrDefault();
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                // Save changes here, e.g. MyDataLayer.SaveChanges();
                db.SaveChanges();
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void Display_DeleteItem(int? id)
        {
            if (id == null)
            {
                return;
            }

            AppContext db = new AppContext();
            GRAScheduledEvent toDelete = db.GRAScheduledEvents.Where(schedEvent => schedEvent.EventId == id)
                .FirstOrDefault();
            db.GRAScheduledEvents.Remove(toDelete);
            db.SaveChanges();
        }

        protected void Display_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Display.PageIndex = e.NewPageIndex;
            DataBind();
        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int eventId = Convert.ToInt32(Server.HtmlDecode(button.CommandArgument));
            Display_UpdateItem(eventId);
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int eventId = Convert.ToInt32(Server.HtmlDecode(button.CommandArgument));
            Display_DeleteItem(eventId);
        }

        protected void AddEvent_Click(object sender, EventArgs e)
        {
            AddEvent.Visible = false;

            Panel allControls = new Panel();

            Label reportLabel = new Label();
            reportLabel.Text = "Choose report";
            allControls.Controls.Add(reportLabel);

            DropDownList reportList = new DropDownList();
            reportList.ID = "ReportName";
            reportList.ClientIDMode = ClientIDMode.Static;
            reportList.Attributes["required"] = "true";
            reportList.DataSource = repo.GRAReports.ToList<GRAReport>();
            reportList.DataTextField = "Name";
            reportList.DataValueField = "Name";
            reportList.DataBind();
            allControls.Controls.Add(reportList);

            Panel interval = new Panel();
            interval.ID = "Interval";
            interval.ClientIDMode = ClientIDMode.Static;

            Label intervalLabel = new Label();
            intervalLabel.Text = "Refresh interval:";
            interval.Controls.Add(intervalLabel);

            Label daysLabel = new Label();
            daysLabel.Text = "Days";
            interval.Controls.Add(daysLabel);

            TextBox daysInput = new TextBox();
            daysInput.ID = "Days";
            daysInput.ClientIDMode = ClientIDMode.Static;
            daysInput.TextMode = TextBoxMode.Number;
            daysInput.Attributes["min"] = "0";
            daysInput.Attributes["max"] = "30";
            daysInput.Text = "0";
            interval.Controls.Add(daysInput);

            Label hoursLabel = new Label();
            hoursLabel.Text = "Hours";
            interval.Controls.Add(hoursLabel);

            TextBox hoursInput = new TextBox();
            hoursInput.ID = "Hours";
            hoursInput.ClientIDMode = ClientIDMode.Static;
            hoursInput.TextMode = TextBoxMode.Number;
            hoursInput.Attributes["min"] = "0";
            hoursInput.Attributes["max"] = "23";
            hoursInput.Text = "0";
            interval.Controls.Add(hoursInput);

            Label minsLabel = new Label();
            minsLabel.Text = "Minutes";
            interval.Controls.Add(minsLabel);

            TextBox minsInput = new TextBox();
            minsInput.ID = "Minutes";
            minsInput.ClientIDMode = ClientIDMode.Static;
            minsInput.TextMode = TextBoxMode.Number;
            minsInput.Attributes["min"] = "0";
            minsInput.Attributes["max"] = "59";
            minsInput.Text = "0";
            interval.Controls.Add(minsInput);

            allControls.Controls.Add(interval);

            Panel recipientControls = new Panel();
            
            Panel recipients = new Panel();
            recipients.ID = "Recipients";
            recipients.ClientIDMode = ClientIDMode.Static;
            recipientControls.Controls.Add(recipients);

            TextBox recipientInput = new TextBox();
            recipientInput.ID = "Email";
            recipientInput.ClientIDMode = ClientIDMode.Static;
            recipientInput.TextMode = TextBoxMode.Email;
            recipientControls.Controls.Add(recipientInput);

            Button addRecipient = new Button();
            addRecipient.ID = "AddRecipient";
            addRecipient.ClientIDMode = ClientIDMode.Static;
            addRecipient.Text = "Add email recipient";
            addRecipient.UseSubmitBehavior = false;
            addRecipient.Click += AddRecipient;
            recipientControls.Controls.Add(addRecipient);

            allControls.Controls.Add(recipientControls);

            Button addEvent = new Button();
            addEvent.ID = "DoneEvent";
            addEvent.ClientIDMode = ClientIDMode.Static;
            addEvent.Text = "Done";
            addEvent.UseSubmitBehavior = false;
            addEvent.Click += AddScheduledEvent;
            allControls.Controls.Add(addEvent);

            Button cancel = new Button();
            cancel.ID = "Cancel";
            cancel.ClientIDMode = ClientIDMode.Static;
            cancel.Text = "Cancel";
            cancel.UseSubmitBehavior = false;
            cancel.Click += RemoveOption;
            allControls.Controls.Add(cancel);

            NewEvent.Controls.Add(allControls);
        }

        public void AddScheduledEvent(object sender, EventArgs e)
        {
            int days = Convert.ToInt32(Server.HtmlDecode(Request.Form["Days"]));
            int hours = Convert.ToInt32(Server.HtmlDecode(Request.Form["Hours"]));
            int mins = Convert.ToInt32(Server.HtmlDecode(Request.Form["Minutes"]));
            TimeSpan interval = new TimeSpan(days, hours, mins, 0);

            if (interval < new TimeSpan(0, 0, 2, 0))
            {
                return;
            }

            GRAScheduledEvent toAdd = new GRAScheduledEvent();

            string reportName = Server.HtmlDecode(Request.Form["ReportName"]);
            int reportId = repo.GRAReports.Where(report => report.Name.Equals(reportName))
                .FirstOrDefault().ReportId;
            toAdd.ReportId = reportId;

            toAdd.TimeInterval = interval;

            DateTime next = DateTime.Now.AddDays(days).AddHours(hours).AddMinutes(mins);
            toAdd.NextDate = next;

            toAdd.Creator = Membership.GetUser().UserName;

            List<string> allRecipients = new List<string>();
            var entry = Request.Form["Recipients"];


            toAdd.SetRecipientList(allRecipients);

            AppContext db = new AppContext();
            db.GRAScheduledEvents.Add(toAdd);
            db.SaveChanges();

            Button button = (Button)sender;
            Panel parent = (Panel)button.Parent;
            Page.Controls.Remove(parent);
        }

        public void AddRecipient(object sender, EventArgs e)
        {
            Panel entry = new Panel();

            Label email = new Label();
            entry.Controls.Add(email);

            Button removeEmail = new Button();
            removeEmail.Text = "Remove email recipient";
            removeEmail.UseSubmitBehavior = false;
            removeEmail.Click += RemoveOption;
            entry.Controls.Add(removeEmail);
        }

        public void RemoveOption(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Panel parent = (Panel)button.Parent;
            Page.Controls.Remove(parent);
        }
    }
}