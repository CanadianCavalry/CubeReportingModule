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
            if (!IsPostBack)
            {
                return;
            }

            int step = Convert.ToInt32(Session["Step"] ?? "0");
            Response.Write(String.Format("Step: {0}", step));   //debug
            switch (step)
            {
                case 0:
                    TableControls.Visible = true;
                    TableControls.Enabled = true;
                    ColumnControls.Visible = false;
                    OptionsControls.Visible = false;
                    Back.Visible = false;
                    Done.Visible = false;

                    List<string> selectedTableNames = GetSelectedTableNames();
                    if (selectedTableNames.Count == 0)
                    {
                        break;
                    }

                    Session["TableNames"] = selectedTableNames;
                    //string queryString = BuildColumnQueryString(selectedTableNames);
                    break;

                case 1:
                    ReportName.Enabled = false;
                    TableControls.Enabled = false;
                    ColumnControls.Visible = true;
                    OptionsControls.Visible = false;
                    Back.Visible = true;
                    Done.Visible = false;

                    selectedTableNames = (List<string>)Session["TableNames"];
                    string queryString = BuildColumnQueryString(selectedTableNames);
                    ColumnQuery.SelectCommand = queryString;
                    //Debug.WriteLine(queryString);   //debug

                    List<string> selectedColumnNames = GetSelectedColumnNames();
                    if (selectedColumnNames.Count == 0)
                    {
                        break;
                    }

                    Session["ColumnNames"] = selectedColumnNames;
                    Debug.WriteLine(selectedColumnNames.ToString());    //debug
                    break;

                case 2:
                    ReportName.Enabled = false;
                    TableControls.Enabled = false;
                    ColumnControls.Visible = true;
                    ColumnControls.Enabled = false;
                    OptionsControls.Visible = true;
                    Back.Visible = true;
                    Done.Visible = true;

                    selectedTableNames = (List<string>)Session["TableNames"];
                    queryString = BuildColumnQueryString(selectedTableNames);
                    ColumnQuery.SelectCommand = queryString;
                    break;

                case 3:
                    CreateTemplate.Visible = false;
                    ReportSummary.Visible = true;
                    break;

                default:
                    ReportName.Visible = false;
                    TableControls.Visible = false;
                    ColumnControls.Visible = false;
                    OptionsControls.Visible = false;
                    Next.Visible = false;
                    Back.Visible = true;
                    Done.Visible = false;
                    break;
            }
        }

        protected List<string> GetSelectedTableNames()
        {
            List<string> selectedTableNames = new List<string>();
            foreach (ListItem item in TableNames.Items)
            {
                if (!item.Selected)
                {
                    continue;
                }

                selectedTableNames.Add(item.Value);
            }

            return selectedTableNames;
        }

        private List<string> GetSelectedColumnNames()
        {
            List<string> selectedColumnNames = new List<string>();
            foreach (ListItem item in ColumnNames.Items)
            {
                if (!item.Selected)
                {
                    continue;
                }

                selectedColumnNames.Add(item.Value);
            }

            return selectedColumnNames;
        }

        protected string BuildColumnQueryString(List<string> selectedTableNames)
        {
            if (selectedTableNames == null)
            {
                return null;
            }

            Queue<string> namesToAdd = new Queue<string>(selectedTableNames);
            if (namesToAdd.Count == 0)
            {
                return null;
            }

            string queryString = @"Select column_name, table_name From information_schema.columns Where table_name = '";

            string toAdd = namesToAdd.Dequeue();
            queryString += toAdd + @"'";

            while (namesToAdd.Count > 0)
            {
                toAdd = namesToAdd.Dequeue();
                queryString += @" Or table_name = '" + toAdd + @"'";
            }

            queryString += " Order By table_name Asc\n";

            return queryString;
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            int step = Convert.ToInt32(Session["Step"] ?? "0");
            if (step >= 2)
            {
                return;
            }

            step++;
            Session["Step"] = step.ToString();
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            int step = Convert.ToInt32(Session["Step"] ?? "0");
            if (step <= 0)
            {
                return;
            }

            step--;
            Session["Step"] = step.ToString();
        }

        protected void AddOption_Click(object sender, EventArgs e)
        {
            Panel option = new Panel();

            Label label = new Label();
            label.Text = "Add Label:";
            TextBox labelControl = new TextBox();

            Label columns = new Label();
            columns.Text = "Choose column:";
            CheckBoxList columnsControl = new CheckBoxList();

            Label type = new Label();
            type.Text = "Option Type:";
            DropDownList typesControl = new DropDownList();
            typesControl.Items.Add("ListBox");
            typesControl.Items.Add("Date");
            typesControl.Items.Add("Number");
            typesControl.Items.Add("Text");

            Label conditions = new Label();
            conditions.Text = "Choose condition";
            DropDownList conditionsControl = new DropDownList();
            conditionsControl.Items.Add(">");
            conditionsControl.Items.Add("<");
            conditionsControl.Items.Add("=");
            conditionsControl.Items.Add(">=");
            conditionsControl.Items.Add("<=");

            Label metric = new Label();
            metric.Text = "Value to compare against:";
            //RadioButtonList metricChoice = new RadioButtonList();
            //metricChoice.Text = "Metric:";
            DropDownList metricsControl = new DropDownList();
            Label or = new Label();
            or.Text = "Or";
            TextBox metricControl = new TextBox();
            //metricChoice.Items.Add(metricsControl);
            //metricChoice.Items.Add(metricControl);
            
            Button remove = new Button();
            remove.Text = "Remove Option";
            remove.Click += Remove_Click;

            option.Controls.Add(label);
            option.Controls.Add(labelControl);
            option.Controls.Add(columns);
            option.Controls.Add(columnsControl);
            option.Controls.Add(type);
            option.Controls.Add(typesControl);
            option.Controls.Add(conditions);
            option.Controls.Add(conditionsControl);
            option.Controls.Add(metric);
            //option.Controls.Add(metricChoice);
            option.Controls.Add(metricsControl);
            option.Controls.Add(or);
            option.Controls.Add(metricControl);
            option.Controls.Add(remove);

            Options.Controls.Add(option);
        }

        protected void Summary_Click(object sender, EventArgs e)
        {
            int step = 3;
            Session["Step"] = step.ToString();
        }

        protected void Remove_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Panel parent = (Panel) button.Parent;

            Page page = button.Page;

            page.Controls.Remove(parent);
        }

        protected void Done_Click(object sender, EventArgs e)
        {
            AppContext db = new AppContext();

            Report reportToAdd = new Report();
            reportToAdd.Name = "";

            //db.Reports.Add(reportToAdd);

            foreach (Panel option in Options.Controls)
            {
                ReportOption optionToAdd = new ReportOption();

                //db.ReportOptions.Add(optionToAdd);
            }

            //db.SaveChanges();
            Response.Redirect("Menu.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            int step = 2;
            Session["Step"] = step.ToString();
        }
    }
}