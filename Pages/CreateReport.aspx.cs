using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using CubeReportingModule.Models;
using System.Web.UI.HtmlControls;

namespace CubeReportingModule.Pages
{
    public partial class CreateReport : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            SetReportOptions();
            SetReportRestrictions();
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            int step = Convert.ToInt32(Session["Step"] ?? "0");
            //debug
            Debug.WriteLine(String.Format("Step: {0}", step));

            if (Session["Options"] != null)
            {
                Control[] allOptions = (Control[])Session["Options"];
                int options = allOptions.Count();
                Debug.WriteLine("Options: " + options);
            }

            if (Session["Restrictions"] != null)
            {
                Control[] allRestrictions = (Control[])Session["Restrictions"];
                int restrictions = allRestrictions.Count();
                Debug.WriteLine("Restrictions: " + restrictions);
            }
            //end debug

            if (IsPostBack)
            {
                string trigger = Request.Params["__EVENTTARGET"];
                Button buttonTrigger = (Button)FindControl(trigger);
                //debug
                //List<Button> allButtons = GetAllPageControlsOfType<Button>();
                //Debug.WriteLine("Page Buttons:");
                //foreach (Button button in allButtons)
                //{
                //    Debug.WriteLine(String.Format("Id: {0}, ClientId: {1}, UniqueId: {2}", button.ID, button.ClientID, button.UniqueID));
                //}
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
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            int step = Convert.ToInt32(Session["Step"] ?? "0");
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
                    break;

                case 3:
                    BuildSummary();
                    break;

                default:
                    break;
            }
        }

        private void BuildSummary()
        {
            //Get next reportId
            AppContext db = new AppContext();
            int reportId = db.GRAReports.ToList().LastOrDefault().ReportId;
            reportId++;

            Queue<string> tableList = new Queue<string>();
            foreach (ListItem item in (ListItemCollection)Session["TableNames"])
            {
                string tableName = Global.CleanInput(item.ToString());
                tableList.Enqueue(tableName);
            }

            Queue<string> columnList = new Queue<string>();
            foreach (ListItem item in (ListItemCollection)Session["ColumnNames"])
            {
                string columnName = Global.CleanInput(item.ToString());
                columnList.Enqueue(columnName);
            }

            //Build Select clause of Query
            string selectClause = "";
            if (columnList.Count() == 0)
            {
                columnList.Enqueue("*");
            }

            string toAdd = columnList.Dequeue();
            selectClause += toAdd;

            while (columnList.Count > 0)
            {
                toAdd = columnList.Dequeue();
                selectClause += ", " + toAdd;
            }
            string columnSummary = selectClause;

            //Build From clause of Query
            string fromClause = "";
            string tableSummary = "";

            toAdd = tableList.Dequeue();
            fromClause += toAdd;
            tableSummary += toAdd;
            if (tableList.Count > 0)
            {
                while (tableList.Count > 0)
                {
                    toAdd = tableList.Dequeue();
                    fromClause += " join " + toAdd;
                    tableSummary += ", " + toAdd;
                }

                fromClause += " 1=1";
            }

            Queue<GRAReportOption> optionList = GetOptionQueue(reportId, fromClause);

            Queue<string> restrictionList = GetRestrictionQueue();

            //Build Where clause of Query
            string whereClause = "";
            string restrictionSummary = "";
            if (restrictionList.Count != 0)
            {
                toAdd = restrictionList.Dequeue();
                whereClause += toAdd;
                restrictionSummary += toAdd;

                while (restrictionList.Count > 0)
                {
                    toAdd = restrictionList.Dequeue();
                    whereClause += " And " + toAdd;
                    restrictionSummary += "<br/>" + toAdd;
                }
            }

            //Build Option summary
            List<GRAReportOption> allOptions = new List<GRAReportOption>();
            string optionSummary = "";
            if (optionList.Count != 0)
            {
                GRAReportOption option = optionList.Dequeue();
                allOptions.Add(option);
                toAdd = option.ToString();
                optionSummary += toAdd;

                while (optionList.Count > 0)
                {
                    option = optionList.Dequeue();
                    allOptions.Add(option);
                    toAdd = option.ToString();
                    optionSummary += "<br/>" + toAdd;
                }
            }

            //Build query
            string query = String.Format("Select {0} From {1}", selectClause, fromClause);
            if (!whereClause.Equals(""))
            {
                query = String.Format("{0} Where {1}", query, whereClause);
            }
            query += ";";

            //Display summary
            string reportName = Global.CleanInput(Session["ReportName"].ToString());
            HtmlGenericControl title = new HtmlGenericControl("h1");
            title.InnerHtml = reportName;
            SummaryDisplay.Controls.Add(title);

            string[] labels = { "Tables", "Columns", "Restrictions", "Options" };
            string[] summaries = { tableSummary, columnSummary, restrictionSummary, optionSummary };
            for (int i = 0; i < 4; i++)
            {
                Label summary = new Label();
                summary.Text = String.Format("{0}:<br/>{1}", labels[i], summaries[i]);
                SummaryDisplay.Controls.Add(summary);
                SummaryDisplay.Controls.Add(new LiteralControl("<br />"));
            }

            //Display Query
            Label querySummary = new Label();
            querySummary.Text = "Query:";
            TextBox queryDisplay = new TextBox();
            queryDisplay.Text = query;
            queryDisplay.ReadOnly = true;
            SummaryDisplay.Controls.Add(querySummary);
            SummaryDisplay.Controls.Add(queryDisplay);

            //Save Report and ReportOptions
            GRAReport report = new GRAReport();
            //report.ReportId = reportId;
            report.Name = reportName;
            report.SelectClause = selectClause;
            report.FromClause = fromClause;
            report.WhereClause = whereClause;

            Session["FinishedReport"] = report;
            Session["FinishedReportOptions"] = allOptions;
        }

        private Queue<string> GetRestrictionQueue()
        {
            Queue<string> restrictionQueue = new Queue<string>();

            if (Session["Restrictions"] == null)
            {
                return restrictionQueue;
            }

            Control[] allRestrictions = (Control[])Session["Restrictions"];
            foreach (Control restriction in allRestrictions)
            {
                ControlCollection restrictionValues = restriction.Controls;

                Label columns = (Label)restrictionValues[0];
                DropDownList columnsList = (DropDownList)restrictionValues[1];
                string columnName = columnsList.SelectedValue.ToString();

                Label conditions = (Label)restrictionValues[2];
                DropDownList conditionsList = (DropDownList)restrictionValues[3];
                string condition = conditionsList.SelectedValue.ToString();

                Label metric = (Label)restrictionValues[4];
                TextBox metricInput = (TextBox)restrictionValues[5];
                string metricValue = Global.CleanInput(metricInput.Text.ToString());

                //DropDownList metricsList = (DropDownList)restrictionValues[5];
                //Label or = (Label)restrictionValues[6];
                //TextBox metricInput = (TextBox)restrictionValues[7];

                //string metricFromList = metricsList.SelectedValue.ToString();
                //string metricFromInput = Global.CleanInput(metricInput.Text);

                //restrictionString += "'";

                //if (metricFromInput.Equals(""))
                //{
                //    restrictionString += " " + metricFromList;
                //}
                //else
                //{
                //    restrictionString += " " + metricFromInput;
                //}

                //restrictionString += "'";

                CheckBox compare = (CheckBox)restrictionValues[6];
                if (compare.Checked == true)
                {
                    string restrictionString = String.Format("{0} {1} {2}", columnName, condition, metricValue);
                    restrictionQueue.Enqueue(restrictionString);
                }

                CheckBox allowNull = (CheckBox)restrictionValues[7];
                if (allowNull.Checked != true)
                {
                    string removeNull = String.Format("{0} IS NOT NULL", columnName);
                    restrictionQueue.Enqueue(removeNull);
                }

                CheckBox allowEmptyString = (CheckBox)restrictionValues[8];
                if (allowEmptyString.Checked != true)
                {
                    string removeEmptyStrings = String.Format("{0} != ''", columnName);
                    restrictionQueue.Enqueue(removeEmptyStrings);
                }
            }

            return restrictionQueue;
        }

        private Queue<GRAReportOption> GetOptionQueue(int reportId, string fromClause)
        {
            Queue<GRAReportOption> optionQueue = new Queue<GRAReportOption>();
            if (Session["Options"] == null)
            {
                return optionQueue;
            }

            AppContext db = new AppContext();
            List<GRAReportOption> allDbOptions = db.GRAReportOptions.ToList();
            //GRAReportOption lastOption = allDbOptions.Where(dbOption => dbOption.ReportId == reportId).Max();
            GRAReportOption lastOption = allDbOptions.Where(dbOption => dbOption.ReportId == reportId).LastOrDefault();
            int nextOptionId = lastOption != null ? lastOption.ReportOptionId++ : 0;

            Control[] allOptions = (Control[])Session["Options"];
            foreach (Control option in allOptions)
            {
                GRAReportOption reportOption = new GRAReportOption();
                reportOption.ReportId = reportId;

                nextOptionId++;
                reportOption.ReportOptionId = nextOptionId;

                ControlCollection optionValues = option.Controls;

                Label label = (Label)optionValues[0];
                TextBox labelInput = (TextBox)optionValues[1];
                reportOption.Label = Global.CleanInput(labelInput.Text);

                Label columns = (Label)optionValues[2];
                DropDownList columnsList = (DropDownList)optionValues[3];
                string selectedColumn = columnsList.SelectedValue.ToString();

                Label type = (Label)optionValues[4];
                DropDownList typesList = (DropDownList)optionValues[5];
                string controlType = typesList.SelectedValue.ToString();
                reportOption.ControlType = controlType;

                Label conditions = (Label)optionValues[6];
                DropDownList conditionsList = (DropDownList)optionValues[7];
                reportOption.Condition = conditionsList.SelectedValue.ToString();

                Label init = (Label)optionValues[8];
                DropDownList initList = (DropDownList)optionValues[9];
                string initType = initList.SelectedValue.ToString();
                reportOption.InitType = initType;

                string selectCommand = String.Format("Select {0} From {1};", selectedColumn, fromClause);

                reportOption.Name = selectedColumn;
                int nameLength = selectedColumn.Length; //debug

                string id = selectedColumn + nextOptionId;
                int idLength = id.Length;   //debug
                reportOption.Id = id;

                reportOption.SelectCommand = selectCommand;
                int selectLength = selectCommand.Length;    //debug

                reportOption.DataTextField = selectedColumn;
                int textFieldLength = selectedColumn.Length;    //debug

                string dataId = id + "Data";
                reportOption.DataSourceId = dataId;
                int dataSourceLength = dataId.Length;   //debug

                optionQueue.Enqueue(reportOption);
            }

            return optionQueue;
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
                string value = Global.CleanInput(Request.Form[key]);
                Debug.WriteLine(String.Format("{0} : {1} : {2}", id, name, value));   //debug
            }
            //end debug

            switch (step)
            {
                case 0:
                    string reportName = Global.CleanInput((string)Request.Form[ReportName.UniqueID]);
                    //Session["GetReportName"] = GetReportName.Text;
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
                        string value = Global.CleanInput(Request.Form[key]);
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
                        string value = Global.CleanInput(Request.Form[key]);
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

            Control[] allOptions = (Control[])Session["Options"];
            foreach (Control option in allOptions)
            {
                Options.Controls.Add(option);
            }

            List<Button> allButtons = GetAllPageControlsOfType<Button>().Where(button => button.Text.Equals("Remove Option")).ToList();
            foreach (Button button in allButtons)
            {
                button.Click += new EventHandler(RemoveOption_Click);
            }
        }

        protected void SetReportRestrictions()
        {
            if (Session["Restrictions"] == null)
            {
                return;
            }

            Control[] allRestrictions = (Control[])Session["Restrictions"];
            foreach (Control restriction in allRestrictions)
            {
                Restrictions.Controls.Add(restriction);
            }

            List<Button> allButtons = GetAllPageControlsOfType<Button>().Where(button => button.Text.Equals("Remove Restriction")).ToList();
            foreach (Button button in allButtons)
            {
                button.Click += new EventHandler(RemoveRestriction_Click);
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
            option.Controls.Add(label);
            TextBox labelInput = new TextBox();
            option.Controls.Add(labelInput);

            Label columns = new Label();
            columns.Text = "Choose column:";
            option.Controls.Add(columns);
            DropDownList columnsList = new DropDownList();
            columnsList.DataSource = (ListItemCollection)Session["ColumnNames"];
            columnsList.DataTextField = "Text";
            columnsList.DataValueField = "Value";
            columnsList.DataBind();
            option.Controls.Add(columnsList);

            Label type = new Label();
            type.Text = "Option Type:";
            option.Controls.Add(type);
            DropDownList typesList = new DropDownList();
            typesList.Items.Add("ListBox");
            typesList.Items.Add("Date");
            typesList.Items.Add("Number");
            typesList.Items.Add("Text");
            option.Controls.Add(typesList);

            Label conditions = new Label();
            conditions.Text = "Choose condition";
            option.Controls.Add(conditions);
            DropDownList conditionsList = new DropDownList();
            conditionsList.Items.Add(">");
            conditionsList.Items.Add("<");
            conditionsList.Items.Add("=");
            conditionsList.Items.Add("!=");
            conditionsList.Items.Add(">=");
            conditionsList.Items.Add("<=");
            option.Controls.Add(conditionsList);

            Label init = new Label();
            init.Text = "Choose initial value";
            option.Controls.Add(init);
            DropDownList initList = new DropDownList();
            initList.Items.Add("");
            initList.Items.Add("Min");
            initList.Items.Add("Mid");
            initList.Items.Add("Max");
            option.Controls.Add(initList);

            Button remove = new Button();
            //remove.ID = "Remove";
            //remove.ClientIDMode = ClientIDMode.Static;
            remove.Text = "Remove Option";
            remove.UseSubmitBehavior = false;
            remove.Click += new EventHandler(this.RemoveOption_Click);
            option.Controls.Add(remove);

            Options.Controls.Add(option);

            Control[] allOptions = new Control[Options.Controls.Count];
            Options.Controls.CopyTo(allOptions, 0);

            Session["Options"] = allOptions;
        }

        protected void AddRestriction_Click(object sender, EventArgs e)
        {
            Panel restriction = new Panel();

            Label columns = new Label();
            columns.Text = "Choose column:";
            restriction.Controls.Add(columns);
            DropDownList columnsList = new DropDownList();
            ListItemCollection selectedColumnNames = (ListItemCollection)Session["ColumnNames"];
            columnsList.DataSource = selectedColumnNames;
            columnsList.DataTextField = "Text";
            columnsList.DataValueField = "Value";
            columnsList.DataBind();
            restriction.Controls.Add(columnsList);

            Label conditions = new Label();
            conditions.Text = "Choose condition";
            restriction.Controls.Add(conditions);
            DropDownList conditionsList = new DropDownList();
            conditionsList.Items.Add(">");
            conditionsList.Items.Add("<");
            conditionsList.Items.Add("=");
            conditionsList.Items.Add("!=");
            conditionsList.Items.Add(">=");
            conditionsList.Items.Add("<=");
            restriction.Controls.Add(conditionsList);

            Label metric = new Label();
            metric.Text = "Value to compare against:";
            restriction.Controls.Add(metric);
            //DropDownList metricsList = new DropDownList();
            //restriction.Controls.Add(metricsList);
            //Label or = new Label();
            //or.Text = " Or ";
            //restriction.Controls.Add(or);
            TextBox metricInput = new TextBox();
            restriction.Controls.Add(metricInput);

            CheckBox compare = new CheckBox();
            //compare.ID = "Compare";
            //compare.ClientIDMode = ClientIDMode.Static;
            compare.Text = "Do comparison:";
            restriction.Controls.Add(compare);

            CheckBox allowNull = new CheckBox();
            //allowNull.ID = "AllowNull";
            //allowNull.ClientIDMode = ClientIDMode.Static;
            allowNull.Text = "Allow Nulls:";
            restriction.Controls.Add(allowNull);

            CheckBox allowEmptyString = new CheckBox();
            //allowEmptyString.ID = "AllowEmptyString";
            //allowEmptyString.ClientIDMode = ClientIDMode.Static;
            allowEmptyString.Text = "Allow Empty Strings:";
            restriction.Controls.Add(allowEmptyString);

            Button remove = new Button();
            //remove.ID = "Remove";
            //remove.ClientIDMode = ClientIDMode.Static;
            remove.Text = "Remove Restriction";
            remove.UseSubmitBehavior = false;
            remove.Click += new EventHandler(RemoveRestriction_Click);
            restriction.Controls.Add(remove);

            Restrictions.Controls.Add(restriction);

            Control[] allRestrictions = new Control[Restrictions.Controls.Count];
            Restrictions.Controls.CopyTo(allRestrictions, 0);
            Session["Restrictions"] = allRestrictions;
        }

        protected void RemoveOption_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Panel parent = (Panel)button.Parent;

            Options.Controls.Remove(parent);
            Control[] allOptions = new Control[Options.Controls.Count];
            Options.Controls.CopyTo(allOptions, 0);
            Session["Options"] = allOptions;
        }

        protected void RemoveRestriction_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Panel parent = (Panel)button.Parent;

            Restrictions.Controls.Remove(parent);
            Control[] allRestrictions = new Control[Restrictions.Controls.Count];
            Restrictions.Controls.CopyTo(allRestrictions, 0);
            Session["Restrictions"] = allRestrictions;
        }

        protected void Done_Click(object sender, EventArgs e)
        {
            AppContext db = new AppContext();

            GRAReport reportToAdd = (GRAReport)Session["FinishedReport"];
            db.GRAReports.Add(reportToAdd);

            db.SaveChanges();

            int reportId = reportToAdd.ReportId;
            Debug.WriteLine("ReportId: " + reportId);   //debug

            foreach (GRAReportOption option in (List<GRAReportOption>)Session["FinishedReportOptions"])
            {
                db.GRAReportOptions.Add(option);
            }

            db.SaveChanges();

            LogWriter.createAccessLog(LogWriter.createReport);

            Cancel_Click(sender, e);
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Session.Remove("Step");
            Session.Remove("ReportName");
            Session.Remove("TableNames");
            Session.Remove("ColumnNames");
            Session.Remove("Options");
            Session.Remove("Restrictions");
            Session.Remove("FinishedReport");
            Session.Remove("FinishedReportOptions");
            Response.Redirect("Menu.aspx");
        }
    }
}