using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CubeReportingModule.Models;

namespace CubeReportingModule.Pages
{
    public partial class ReportDisplayHTML : System.Web.UI.Page
    {

        public string reportPath = @"C:\";


        protected void Page_Load(object sender, EventArgs e)
        {
            QueryData.SelectCommand = GetSelectCommand();
            Display.DataSourceID = "QueryData";
        }

        public string GetSelectCommand()
        {
            string selectCommand = (string) Session["Query"];
            return selectCommand;
        }

        public void CreateExcelButton_Click(object sender, EventArgs e)
        {
            //// Get the current page HTML string by rendering into a TextWriter object
            TextWriter outTextWriter = new StringWriter();
            HtmlTextWriter outHtmlTextWriter = new HtmlTextWriter(outTextWriter);
            base.Render(outHtmlTextWriter);

             ////Obtain the current page HTML string
            string currentPageHtmlString = outHtmlTextWriter.InnerWriter.ToString();

            // Set up all variables for parsing the html page
            List<List<string>> allRows = new List<List<string>>();
            string cellContents;
            int tdStart = 0;
            int tdEnd = 0;
            int rowStart = 0;
            int rowEnd = 0;

            // Find the next <tr> element on the page
            while (currentPageHtmlString.IndexOf("<tr>", rowEnd) != -1) {
                // Get the table row as a string
                rowStart = currentPageHtmlString.IndexOf("<tr>", rowEnd) + 4;
                rowEnd = currentPageHtmlString.IndexOf("</tr>", rowStart);
                string rowString = currentPageHtmlString.Substring(rowStart, rowEnd - rowStart);
                List<string> row = new List<string>();

                //Find the next <th> element in the current row
                while (rowString.IndexOf(";)\">", tdEnd) != -1)
                {
                    // Get the contents of the <td> element and add it to the array
                    tdStart = rowString.IndexOf(";)\">", tdEnd) + 4;
                    tdEnd = rowString.IndexOf("</a>", tdStart);
                    cellContents = rowString.Substring(tdStart, tdEnd - tdStart);
                    row.Add(cellContents);
                }

            //Find the next <td> element in the current row
                while (rowString.IndexOf("<td>", tdEnd) != -1)
                {
                    // Get the contents of the <td> element and add it to the array
                    tdStart = rowString.IndexOf("<td>", tdEnd) + 4;
                    tdEnd = rowString.IndexOf("</td>", tdStart);
                    cellContents = rowString.Substring(tdStart, tdEnd - tdStart);
                    row.Add(cellContents);
                }
                allRows.Add(row);
                tdStart = 0;
                tdEnd = 0;
            }

            string reportName = Session["ReportName"].ToString();
            ExcelConverter.WriteExcelFile(reportPath, reportName, "report", allRows);

            //// Set response content type
            //Response.AddHeader("Content-Type", "application/xlsx");

            //// Instruct the browser to open the PDF file as an attachment or inline
            //Response.AddHeader("Content-Disposition", String.Format("attachment; filename=GAINReport.xls; size={0}"));

            //// Write the PDF document buffer to HTTP response
            //Response.BinaryWrite(outPdfBuffer);

            //// End the HTTP response and stop the current page processing
            //Response.End();

        }
    }
}