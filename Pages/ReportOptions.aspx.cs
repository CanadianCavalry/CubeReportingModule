using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;
using System.Diagnostics;
using System.Data;

namespace CubeReportingModule.Pages
{
    public partial class ReportOptions : System.Web.UI.Page
    {
        private Repository repo = new Repository();
        public GRAReport pageReport;

        protected void Page_Load(object sender, EventArgs e)
        {
            pageReport = getPageReport();
            BuildSelectOptions();
        }

        private GRAReport getPageReport()
        {
            int reportId = Convert.ToInt32((string)Session["ReportId"]);
            IEnumerable<GRAReport> allReports = repo.GRAReports;
            GRAReport toGet = allReports.Where(option => option.ReportId == reportId).FirstOrDefault();
            return toGet;
        }

        public void BuildSelectOptions()
        {
            //HtmlGenericControl div = FindControl("SelectOptions") as HtmlGenericControl;
            HtmlGenericControl div = OptionControls;
            if (div == null)
            {
                return;
            }

            OptionsHeader.InnerText = pageReport.Name;

            IEnumerable<GRAReportOption> allReportOptions = GetReportOptions();
            foreach (GRAReportOption option in allReportOptions)
            {
                string optionType = option.ControlType;

                Label label = new Label();
                label.Text = option.Label + " " + option.Condition;
                div.Controls.Add(label);

                SqlDataSource dataSource = new SqlDataSource();
                dataSource.ID = option.DataSourceId;
                dataSource.SelectCommand = option.SelectCommand;
                dataSource.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString;
                div.Controls.Add(dataSource);

                switch (optionType)
                {
                    case "ListBox":
                        ListBox listBox = new ListBox();
                        listBox.ID = option.Id;
                        listBox.ClientIDMode = ClientIDMode.Static;
                        listBox.DataTextField = option.DataTextField;
                        listBox.DataSourceID = option.DataSourceId;
                        dataSource.DataSourceMode = SqlDataSourceMode.DataReader;

                        div.Controls.Add(listBox);
                        break;

                    case "Date":
                        Calendar calendar = new Calendar();
                        calendar.ID = option.Id;
                        calendar.ClientIDMode = ClientIDMode.Static;

                        div.Controls.Add(calendar);
                        break;

                    case "Number":
                        dataSource.DataSourceMode = SqlDataSourceMode.DataSet;
                        DataView data = (DataView)dataSource.Select(DataSourceSelectArguments.Empty);
                        List<int> allValues = new List<int>();
                        foreach (DataRow row in data.ToTable().Rows)
                        {
                            int value = Convert.ToInt32(row[option.DataTextField]);
                            allValues.Add(value);
                        }
                        int maxValue = allValues.Max();
                        int minValue = allValues.Min();
                        if (minValue < 0)
                        {
                            minValue = 0;
                        }

                        int? initValue;
                        switch (option.InitType)
                        {
                            case "min":
                                initValue = minValue;
                                break;

                            case "max":
                                initValue = maxValue;
                                break;

                            case "mid":
                                initValue = (maxValue - minValue) / 2;
                                break;

                            default:
                                initValue = null;
                                break;
                        }

                        HtmlInputText number = new HtmlInputText();
                        number.Attributes["type"] = "number";
                        number.Attributes["name"] = option.Id;
                        number.Attributes["min"] = minValue.ToString();
                        number.Attributes["max"] = maxValue.ToString();
                        if (initValue != null)
                        {
                            number.Attributes["value"] = initValue.ToString();
                        }
                        number.ID = option.Id;
                        number.ClientIDMode = ClientIDMode.Static;

                        div.Controls.Add(number);
                        break;

                    case "Text":
                        dataSource.DataSourceMode = SqlDataSourceMode.DataSet;
                        data = (DataView)dataSource.Select(DataSourceSelectArguments.Empty);
                        List<string> allItems = new List<string>();
                        foreach (DataRow row in data.ToTable().Rows)
                        {
                            string item = row[option.DataTextField].ToString();
                            allItems.Add(item);
                        }
                        string maxItem = allItems.Max();
                        string minItem = allItems.Min();

                        string initItem;
                        switch (option.InitType)
                        {
                            case "min":
                                initItem = minItem;
                                break;

                            case "max":
                                initItem = maxItem;
                                break;

                            default:
                                initItem = "";
                                break;
                        }

                        HtmlInputText text = new HtmlInputText();
                        text.ID = option.Id;
                        text.ClientIDMode = ClientIDMode.Static;
                        text.Name = option.Name;
                        if (!initItem.Equals(""))
                        {
                            text.Value = initItem;
                        }

                        div.Controls.Add(text);
                        break;
                }
            }
        }

        private List<Control> GetAllPageControls()
        {
            List<Control> allPageControls = new List<Control>();

            AddControlsToList<Control>(Page.Controls, allPageControls);

            return allPageControls;
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

        private string BuildQuery()
        {
            if (pageReport.SelectClause == null || pageReport.SelectClause.Equals(""))
            {
                pageReport.SelectClause = "*";
            }
            string query = "Select " + pageReport.SelectClause;

            query += "\n";

            query += "From " + pageReport.FromClause;
            query += "\n";

            Queue<string> WhereClauses = new Queue<string>();

            List<Control> allControls = GetAllPageControls();

            if (pageReport.WhereClause != null && !pageReport.WhereClause.Equals(""))
            {
                WhereClauses.Enqueue(pageReport.WhereClause);
            }

            //NameValueCollection postData = Request.Form;
            //foreach (string key in postData.AllKeys)
            //{
            //    string[] values = postData.GetValues(key);
            //    Debug.Write(key);
            //    foreach(string value in values) {
            //        Debug.Write(" " + value);
            //    }
            //    Debug.Write("\n");
            //}

            IEnumerable<GRAReportOption> allReportOptions = GetReportOptions();
            foreach (GRAReportOption option in allReportOptions)
            {
                string optionName = option.Name;
                string optionCondition = option.Condition;
                string optionId = option.Id;
                Control optionControl = allControls.Where(control => control.ClientID.Equals(optionId)).FirstOrDefault();
                string controlName = optionControl.UniqueID;
                string optionValue = "";
                optionValue = Server.HtmlDecode(String.Format("{0}", Request.Form[controlName]));

                if (optionValue == null || optionValue.Equals(""))
                {
                    optionValue = Server.HtmlDecode(String.Format("{0}", Request.Form[option.Id]));
                }

                //if (option.Metric != null && !option.Metric.Equals(""))
                //{
                //    optionValue = option.Metric;
                //}

                if (optionValue == null || optionValue.Equals(""))
                {
                    continue;
                }

                string clauseToAdd = optionName + " " + optionCondition + " '" + optionValue + "'";
                WhereClauses.Enqueue(clauseToAdd);
            }

            if (WhereClauses.Count != 0)
            {
                query += "Where " + WhereClauses.Dequeue();

                while (WhereClauses.Count > 0)
                {
                    query += " And " + WhereClauses.Dequeue();
                }

                query += "\n";
            }

            return query;
        }

        public IEnumerable<GRAReportOption> GetReportOptions()
        {
            int reportId = pageReport.ReportId;
            IEnumerable<GRAReportOption> allReportOptions = repo.GRAReportOptions.Where(option => option.ReportId == reportId).OrderBy(option => option.ReportOptionId);
            return allReportOptions;
        }

        protected void SubmitReportButton_Click(object sender, EventArgs e)
        {
            string query = BuildQuery();
            Debug.Write(query); //debug
            Session["Query"] = query;
            Response.Redirect("ReportDisplayHTML.aspx");
            return;
        }
    }
}