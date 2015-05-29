using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using CubeReportingModule.Models;

namespace CubeReportingModule.Pages
{
    public partial class CreateReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int step = Convert.ToInt32(Session["Step"] ?? "0");
            Response.Write(String.Format("Step: {0}", step));   //debug

            if (IsPostBack)
            {
                string trigger = Request.Params["__EVENTTARGET"];
                Button buttonTrigger = (Button) FindControl(trigger);
                //debug
                List<Button> allButtons = GetAllPageControlsOfType<Button>();
                Debug.WriteLine("Page Buttons:");
                foreach (Button button in allButtons)
                {
                    Debug.WriteLine(String.Format("Id: {0}, ClientId: {1}, UniqueId: {2}", button.ID, button.ClientID, button.UniqueID));
                }
                //end debug
                //Button buttonTrigger = allButtons.Where(button => button.ID.Equals(trigger)).FirstOrDefault();
                string triggerId = "";
                
                if (buttonTrigger != null)
                {
                    triggerId = buttonTrigger.ClientID;
                    Debug.WriteLine("Trigger: " + triggerId);   //debug
                }

                if (triggerId.Equals(Next.ClientID))
                {
                    NextStep(ref step);
                }

                if (triggerId.Equals(Previous.ClientID))
                {
                    PreviousStep(ref step);
                }

                if (triggerId.Equals(Summary.ClientID))
                {
                    step = 3;
                    Session["Step"] = step;
                }

                if (triggerId.Equals(Modify.ClientID))
                {
                    step = 2;
                    Session["Step"] = step;
                }
            }

            SetControlVisibilities(step);
            SetControlValues(step);
        }

        private void SetControlVisibilities(int step)
        {
            SetReportName();

            switch (step)
            {
                case 0:
                    ColumnControls.Visible = false;

                    OptionsControls.Visible = false;
                    RestrictionsControls.Visible = false;

                    Previous.Visible = false;
                    Summary.Visible = false;

                    ReportSummary.Visible = false;
                    break;

                case 1:
                    NameControls.Enabled = false;
                    TableControls.Enabled = false;

                    OptionsControls.Visible = false;
                    RestrictionsControls.Visible = false;

                    Summary.Visible = false;

                    ReportSummary.Visible = false;
                    break;

                case 2:
                    NameControls.Enabled = false;
                    TableControls.Enabled = false;

                    ColumnControls.Enabled = false;

                    Next.Visible = false;

                    ReportSummary.Visible = false;
                    break;

                case 3:
                    CreateTemplate.Visible = false;
                    break;

                default:
                    NameControls.Visible = false;
                    TableControls.Visible = false;

                    ColumnControls.Visible = false;

                    OptionsControls.Visible = false;
                    RestrictionsControls.Visible = false;

                    Next.Visible = false;
                    Summary.Visible = false;

                    ReportSummary.Visible = false;
                    break;
            }
        }

        private void SetControlValues(int step)
        {
            SetReportName();

            switch (step)
            {
                case 0:
                    SetSelectedTableNames();
                    break;

                case 1:
                    if (Session["TableNames"] == null)
                    {
                        Session["Step"] = "0";
                        //Page_Load(this, new EventArgs());
                        Response.Redirect("CreateReport.aspx");
                        break;
                    }

                    ListItemCollection selectedTableNames = (ListItemCollection)Session["TableNames"];
                    SetSelectedTableNames();

                    string queryString = BuildColumnQueryString(selectedTableNames);
                    ColumnQuery.SelectCommand = queryString;

                    SetSelectedColumnNames();
                    break;

                case 2:
                    if (Session["ColumnNames"] == null)
                    {
                        Session["Step"] = "1";
                        //Page_Load(this, new EventArgs());
                        Response.Redirect("CreateReport.aspx");
                        break;
                    }

                    selectedTableNames = (ListItemCollection)Session["TableNames"];
                    SetSelectedTableNames();

                    queryString = BuildColumnQueryString(selectedTableNames);
                    ColumnQuery.SelectCommand = queryString;

                    SetSelectedColumnNames();

                    SetReportOptions();
                    break;

                case 3:
                    Label summary = new Label();
                    string reportInfo = "";

                    reportInfo += "Tables: ";
                    foreach (ListItem item in (ListItemCollection)Session["TableNames"])
                    {
                        reportInfo += item.ToString();
                        reportInfo += ", ";
                    }
                    reportInfo += "/n";

                    reportInfo += "Columns: ";
                    foreach (ListItem item in (ListItemCollection)Session["ColumnNames"])
                    {
                        reportInfo += item.ToString();
                        reportInfo += ", ";
                    }
                    reportInfo += "/n";

                    summary.Text = String.Format(reportInfo);
                    SummaryDisplay.Controls.Add(summary);
                    break;

                default:
                    break;
            }
        }

        protected void NextStep(ref int step)
        {
            //debug
            foreach (string key in Request.Form.AllKeys)
            {
                Control control = FindControl(key);
                if (control == null)
                {
                    continue;
                }

                string id = control.UniqueID;
                string name = control.ClientID;
                string value = Server.HtmlDecode(Request.Form[key]);
                Debug.WriteLine(String.Format("{0} : {1} : {2}", id, name, value));   //debug
            }
            //end debug

            switch (step)
            {
                case 0:
                    string reportName = Server.HtmlDecode((string)Request.Form[ReportName.UniqueID]);
                    //Session["ReportName"] = ReportName.Text;
                    Session["ReportName"] = reportName;

                    //if (TableNames.Items.Count == 0)
                    //{
                    //    return;
                    //}
                    //Session["TableNames"] = TableNames.Items;
                    ListItemCollection allTableNames = new ListItemCollection();
                    IEnumerable<string> tableKeys = Request.Form.AllKeys.Where(key => key.StartsWith(TableNames.UniqueID) == true);
                    foreach (string key in tableKeys)
                    {
                        string value = Server.HtmlDecode(Request.Form[key]);
                        ListItem toAdd = new ListItem();
                        toAdd.Value = value;
                        toAdd.Selected = true;
                        allTableNames.Add(toAdd);
                    }

                    Session["TableNames"] = allTableNames;
                    break;

                case 1:
                    //bool visible = ColumnNames.Visible; //debug

                    //if (ColumnNames.Items.Count == 0)
                    //{
                    //    return;
                    //}

                    //Session["ColumnNames"] = ColumnNames.Items;
                    ListItemCollection allColumnNames = new ListItemCollection();
                    IEnumerable<string> columnKeys = Request.Form.AllKeys.Where(key => key.StartsWith(ColumnNames.UniqueID) == true);
                    foreach (string key in columnKeys)
                    {
                        string value = Server.HtmlDecode(Request.Form[key]);
                        ListItem toAdd = new ListItem();
                        toAdd.Value = value;
                        toAdd.Selected = true;
                        allColumnNames.Add(toAdd);
                    }

                    Session["ColumnNames"] = allColumnNames;
                    break;

                case 2:
                    break;

                case 3:
                    break;
            }

            if (step >= 2)
            {
                return;
            }

            step++;
            Session["Step"] = step.ToString();
        }

        protected void PreviousStep(ref int step)
        {
            if (step <= 0)
            {
                return;
            }

            step--;
            Session["Step"] = step.ToString();
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

        protected void SetSelectedColumnNames()
        {
            if (Session["ColumnNames"] == null)
            {
                return;
            }

            if (ColumnNames.Items.Count == 0)
            {
                ListItemCollection selectedTableNames = (ListItemCollection)Session["TableNames"];
                string queryString = BuildColumnQueryString(selectedTableNames);
                ColumnQuery.SelectCommand = queryString;
            }

            if (ColumnNames.Items.Count == 0)
            {
                return;
            }

            ListItemCollection allColumnNames = (ListItemCollection)Session["ColumnNames"];

            foreach (ListItem item in allColumnNames)
            {
                string value = item.Value;
                ListItem toSelect = ColumnNames.Items.FindByValue(value);
                toSelect.Selected = item.Selected;
            }
        }

        protected void SetSelectedTableNames()
        {
            if (Session["TableNames"] == null)
            {
                return;
            }

            ListItemCollection allTableNames = (ListItemCollection)Session["TableNames"];

            foreach (ListItem item in allTableNames)
            {
                string value = item.Value;
                ListItem toSelect = TableNames.Items.FindByValue(value);
                toSelect.Selected = item.Selected;
            }
        }

        protected void SetReportName()
        {
            if (Session["ReportName"] == null)
            {
                return;
            }

            string reportName = (string)Session["ReportName"];
            ReportName.Text = reportName;
        }

        protected void SetReportOptions()
        {
            if (Session["Options"] == null)
            {
                return;
            }

            ControlCollection allOptions = (ControlCollection)Session["Options"];
            foreach (Control option in allOptions)
            {
                Options.Controls.Add(option);
            }
        }

        protected string BuildColumnQueryString(ListItemCollection selectedTableNames)
        {
            if (selectedTableNames == null)
            {
                return null;
            }

            if (selectedTableNames.Count == 0)
            {
                return null;
            }

            Queue<string> namesToAdd = new Queue<string>();
            foreach (ListItem item in selectedTableNames)
            {
                //bool isSelected = item.Selected;
                //if (isSelected == false)
                //{
                //    continue;
                //}

                string valueToAdd = item.Value;
                namesToAdd.Enqueue(valueToAdd);
            }

            string queryString = @"Select column_name, table_name From information_schema.columns Where table_name = '";

            string tableNameToAdd = namesToAdd.Dequeue();
            queryString += tableNameToAdd + @"'";

            while (namesToAdd.Count > 0)
            {
                tableNameToAdd = namesToAdd.Dequeue();
                queryString += @" Or table_name = '" + tableNameToAdd + @"'";
            }

            queryString += " Order By table_name Asc\n";

            return queryString;
        }

        protected void AddOption_Click(object sender, EventArgs e)
        {
            Panel option = new Panel();

            Label label = new Label();
            label.Text = "Add Label:";
            TextBox labelInput = new TextBox();

            Label columns = new Label();
            columns.Text = "Choose column:";
            CheckBoxList columnsList = new CheckBoxList();

            Label type = new Label();
            type.Text = "Option Type:";
            DropDownList typesList = new DropDownList();
            typesList.Items.Add("ListBox");
            typesList.Items.Add("Date");
            typesList.Items.Add("Number");
            typesList.Items.Add("Text");

            Label conditions = new Label();
            conditions.Text = "Choose condition";
            DropDownList conditionsList = new DropDownList();
            conditionsList.Items.Add(">");
            conditionsList.Items.Add("<");
            conditionsList.Items.Add("=");
            conditionsList.Items.Add(">=");
            conditionsList.Items.Add("<=");

            Label metric = new Label();
            metric.Text = "Value to compare against:";
            //RadioButtonList metricChoice = new RadioButtonList();
            //metricChoice.Text = "Metric:";
            DropDownList metricsList = new DropDownList();
            Label or = new Label();
            or.Text = "Or";
            TextBox metricInput = new TextBox();
            //metricChoice.Items.Add(metricsList);
            //metricChoice.Items.Add(metricInput);

            Button remove = new Button();
            //remove.ID = "Remove";
            //remove.ClientIDMode = ClientIDMode.Static;
            remove.Text = "Remove Option";
            remove.UseSubmitBehavior = false;
            remove.Click += Remove_Click;

            option.Controls.Add(label);
            option.Controls.Add(labelInput);
            option.Controls.Add(columns);
            option.Controls.Add(columnsList);
            option.Controls.Add(type);
            option.Controls.Add(typesList);
            option.Controls.Add(conditions);
            option.Controls.Add(conditionsList);
            option.Controls.Add(metric);
            //option.Controls.Add(metricChoice);
            option.Controls.Add(metricsList);
            option.Controls.Add(or);
            option.Controls.Add(metricInput);
            option.Controls.Add(remove);

            Options.Controls.Add(option);

            ControlCollection allOptions = Options.Controls;
            if (Session["Options"] == null)
            {
                Session["Options"] = allOptions;
                return;
            }

            allOptions = (ControlCollection)Session["Options"];
            allOptions.Add(option);
            Session["Options"] = allOptions;
        }

        protected void AddRestriction_Click(object sender, EventArgs e)
        {
            Panel restriction = new Panel();

            Label columns = new Label();
            columns.Text = "Choose column:";
            CheckBoxList columnsList = new CheckBoxList();

            Label conditions = new Label();
            conditions.Text = "Choose condition";
            DropDownList conditionsList = new DropDownList();
            conditionsList.Items.Add(">");
            conditionsList.Items.Add("<");
            conditionsList.Items.Add("=");
            conditionsList.Items.Add(">=");
            conditionsList.Items.Add("<=");

            Label metric = new Label();
            metric.Text = "Value to compare against:";
            DropDownList metricsList = new DropDownList();
            Label or = new Label();
            or.Text = "Or";
            TextBox metricInput = new TextBox();

            Button remove = new Button();
            //remove.ID = "Remove";
            //remove.ClientIDMode = ClientIDMode.Static;
            remove.Text = "Remove Option";
            remove.UseSubmitBehavior = false;
            remove.Click += Remove_Click;

            restriction.Controls.Add(columns);
            restriction.Controls.Add(columnsList);
            restriction.Controls.Add(conditions);
            restriction.Controls.Add(conditionsList);
            restriction.Controls.Add(metric);
            restriction.Controls.Add(metricsList);
            restriction.Controls.Add(metricInput);
            restriction.Controls.Add(remove);

            Restrictions.Controls.Add(restriction);
        }

        protected void Remove_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Panel parent = (Panel)button.Parent;

            //Page page = button.Page;
            //page.Controls.Remove(parent);
            Page.Controls.Remove(parent);
        }

        protected void Done_Click(object sender, EventArgs e)
        {
            AppContext db = new AppContext();

            Report reportToAdd = new Report();
            reportToAdd.Name = "";

            //db.Reports.Add(reportToAdd);

            foreach (Panel option in Options.Controls)
            {
                GRAReportOption optionToAdd = new GRAReportOption();

                //db.GRAReportOptions.Add(optionToAdd);
            }

            //db.SaveChanges();
            Cancel_Click(sender, e);
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Session.Remove("Step");
            Response.Redirect("Menu.aspx");
        }
    }
}