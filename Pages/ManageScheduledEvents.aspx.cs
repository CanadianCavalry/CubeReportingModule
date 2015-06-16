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

        protected void Page_Init(object sender, EventArgs e)
        {
            SetEvents();

            SetRecipients();

            SetButtonHandlers();
        }

        private void SetButtonHandlers()
        {
            List<Button> allButtons = GetAllPageControlsOfType<Button>().Where(button => button.ClientID.Equals("AddRecipient")).ToList();
            foreach (Button button in allButtons)
            {
                button.Click += new EventHandler(AddRecipient);
            }

            allButtons = GetAllPageControlsOfType<Button>().Where(button => button.ClientID.Equals("DoneEvent")).ToList();
            foreach (Button button in allButtons)
            {
                button.Click += new EventHandler(AddScheduledEvent);
            }

            allButtons = GetAllPageControlsOfType<Button>().Where(button => button.ClientID.Equals("Cancel")).ToList();
            foreach (Button button in allButtons)
            {
                button.Click += new EventHandler(RemoveOption);
            }

            allButtons = GetAllPageControlsOfType<Button>().Where(button => button.CssClass.Equals("RemoveEmail")).ToList();
            foreach (Button button in allButtons)
            {
                button.Click += new EventHandler(RemoveRecipient);
            }
        }

        private void SetEvents()
        {
            if (Session["Events"] == null)
            {
                return;
            }

            AddEvent.Visible = false;

            Control[] allEvents = (Control[])Session["Events"];
            foreach (Control option in allEvents)
            {
                EventToAdd.Controls.Add(option);
            }
        }

        private void SetRecipients()
        {
            if (Session["Recipients"] == null)
            {
                return;
            }

            Panel recipientControls = GetRecipients();

            Control[] allRecipients = (Control[])Session["Recipients"];
            foreach (Control option in allRecipients)
            {
                recipientControls.Controls.Add(option);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected List<Control> GetAllPageControls()
        {
            return GetAllPageControlsOfType<Control>();
        }

        protected List<T> GetAllPageControlsOfType<T>()
            where T : Control
        {
            List<T> allControlsOfType = new List<T>();
            AddControlsToList<T>(Page.Controls, allControlsOfType);
            return allControlsOfType;
        }

        private void AddControlsToList<T>(ControlCollection controls, List<T> list)
            where T : Control
        {
            foreach (Control control in controls)
            {
                if (control is T)
                {
                    list.Add((T)control);
                }

                if (control.HasControls())
                {
                    AddControlsToList(control.Controls, list);
                }
            }
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

                int eventId = item.EventId;

                LogWriter.createAccessLog(LogWriter.modifyEvent + " " + eventId.ToString());
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
            int eventId = toDelete.EventId;
            db.GRAScheduledEvents.Remove(toDelete);
            db.SaveChanges();

            LogWriter.createAccessLog(LogWriter.deleteEvent + " " + eventId.ToString());
        }

        protected void Display_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Display.PageIndex = e.NewPageIndex;
            DataBind();
        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int eventId = Convert.ToInt32(Global.CleanInput(button.CommandArgument));
            Display_UpdateItem(eventId);
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int eventId = Convert.ToInt32(Global.CleanInput(button.CommandArgument));
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

            interval.Controls.Add(new LiteralControl("<br />"));

            Label intervalNote = new Label();
            int mins = (int) Global.GetTimerInterval().TotalMinutes;
            intervalNote.Text = String.Format("Interval must be greater than {0}mins", mins);
            interval.Controls.Add(intervalNote);

            interval.Controls.Add(new LiteralControl("<br />"));

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
            recipientControls.CssClass = "Recipients";
            
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

            EventToAdd.Controls.Add(allControls);

            Control[] allEvents = new Control[EventToAdd.Controls.Count];
            EventToAdd.Controls.CopyTo(allEvents, 0);

            Session["Events"] = allEvents;
        }

        public void AddScheduledEvent(object sender, EventArgs e)
        {
            List<Panel> allPanels = GetAllPageControlsOfType<Panel>().ToList();
            Panel intervalControls = allPanels.Where(panel => panel.ClientID.Equals("Interval") == true).FirstOrDefault();
            List<TextBox> inputs = intervalControls.Controls.OfType<TextBox>().ToList();    
            TextBox dayInput = inputs.Where(textbox => textbox.ClientID.Equals("Days")).FirstOrDefault();
            TextBox hourInput = inputs.Where(textbox => textbox.ClientID.Equals("Hours")).FirstOrDefault();
            TextBox minInput = inputs.Where(textbox => textbox.ClientID.Equals("Minutes")).FirstOrDefault();

            int days = Convert.ToInt32(Global.CleanInput(dayInput.Text));
            int hours = Convert.ToInt32(Global.CleanInput(hourInput.Text));
            int mins = Convert.ToInt32(Global.CleanInput(minInput.Text));
            TimeSpan interval = new TimeSpan(days, hours, mins, 0);

            TimeSpan timerInterval = Global.GetTimerInterval();
            if (TimeSpan.Compare(interval, timerInterval) < 0)
            {
                int intervalMins = (int) Global.GetTimerInterval().TotalMinutes;
                DisplayMessage(String.Format("Refresh interval cannot be less than {0}min", intervalMins));
                return;
            }

            GRAScheduledEvent toAdd = new GRAScheduledEvent();

            DropDownList reportList = GetAllPageControlsOfType<DropDownList>().Where(list => list.ClientID.Equals("ReportName")).FirstOrDefault();
            string reportName = Global.CleanInput(reportList.SelectedValue);
            int reportId = repo.GRAReports.Where(report => report.Name.Equals(reportName))
                .FirstOrDefault().ReportId;
            toAdd.ReportId = reportId;

            toAdd.TimeInterval = (int)interval.TotalMinutes;

            DateTime next = DateTime.Now.AddDays(days).AddHours(hours).AddMinutes(mins);
            toAdd.NextDate = next;

            toAdd.Creator = Membership.GetUser().UserName;

            List<string> allRecipients = new List<string>();
            List<Label> allEmails = GetAllPageControlsOfType<Label>().Where(label => label.CssClass.Equals("RecipientEmail")).ToList();
            if (allEmails.Count == 0)
            {
                DisplayMessage("Event must have at least 1 recipient");
                return;
            }

            foreach (Label email in allEmails)
            {
                string emailText = email.Text;
                allRecipients.Add(emailText);
            }

            toAdd.SetRecipientList(allRecipients);

            AppContext db = new AppContext();
            db.GRAScheduledEvents.Add(toAdd);
            db.SaveChanges();

            LogWriter.createAccessLog(LogWriter.createEvent);

            Session.Remove("Events");
            Session.Remove("Recipients");

            EventToAdd.Visible = false;
            AddEvent.Visible = true;

            //Button button = (Button)sender;
            //Panel parent = (Panel)button.Parent;
            ////Page.Controls.Remove(parent);

            //EventToAdd.Controls.Remove(parent);

            ////Control[] allEvents = new Control[EventToAdd.Controls.Count];
            ////EventToAdd.Controls.CopyTo(allEvents, 0);
            //Session["Events"] = null;
            //Session["Recipients"] = null;
        }

        private void DisplayMessage(string text)
        {
            Label message = new Label();
            message.Text = text;
            message.CssClass = "StatusMessage";
            Message.Controls.Add(message);
        }

        public void AddRecipient(object sender, EventArgs e)
        {
            TextBox recipientInput = GetAllPageControlsOfType<TextBox>().ToList().Where(textbox => textbox.ClientID.Equals("Email")).FirstOrDefault();
            string emailToAdd = Global.CleanInput(recipientInput.Text);
            if(emailToAdd.Equals(String.Empty)) {
                return;
            }

            Panel entry = new Panel();

            Label email = new Label();
            email.CssClass = "RecipientEmail";
            email.Text = emailToAdd;
            entry.Controls.Add(email);

            Button removeEmail = new Button();
            removeEmail.CssClass = "RemoveEmail";
            removeEmail.Text = "Remove email recipient";
            removeEmail.UseSubmitBehavior = false;
            removeEmail.Click += new EventHandler(RemoveRecipient);
            entry.Controls.Add(removeEmail);

            Panel recipientControls = GetRecipients();
            recipientControls.Controls.Add(entry);

            Control[] allRecipients = new Control[recipientControls.Controls.Count];
            recipientControls.Controls.CopyTo(allRecipients, 0);
            Session["Recipients"] = allRecipients;

            recipientInput.Text = String.Empty;
            //SetButtonHandlers();
        }

        public void RemoveOption(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Panel parent = (Panel)button.Parent;
            //Page.Controls.Remove(parent);

            EventToAdd.Controls.Remove(parent);

            //Control[] allEvents = new Control[EventToAdd.Controls.Count];
            //EventToAdd.Controls.CopyTo(allEvents, 0);
            Session["Events"] = null;
            Session["Recipients"] = null;

            AddEvent.Visible = true;
        }

        public void RemoveRecipient(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Panel parent = (Panel)button.Parent;
            //Page.Controls.Remove(parent);

            Panel recipientControls = GetRecipients();
            recipientControls.Controls.Remove(parent);

            Control[] allRecipients = new Control[recipientControls.Controls.Count];
            recipientControls.Controls.CopyTo(allRecipients, 0);
            Session["Recipients"] = allRecipients;
            //SetButtonHandlers();
        }

        private Panel GetRecipients()
        {
            Panel recipientControls = GetAllPageControlsOfType<Panel>().ToList().Where(panel => panel.CssClass.Equals("Recipients")).FirstOrDefault();
            return recipientControls;
        }
    }
}