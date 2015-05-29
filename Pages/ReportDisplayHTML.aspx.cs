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

        public string reportName = @"C:\Users\M\Documents\School\Term6\GainReport1.xlsx";


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
            string currentPageHtmlString = outHtmlTextWriter.ToString();
            List<string> row = new List<string>();
            List<List<string>> allRows = new List<List<string>>();

            while (currentPageHtmlString != "</table>")
            {
                while(currentPageHtmlString != "</tr>"){
                
                row.Add(outTextWriter.ToString());
                    }
                
                allRows.Add(row);
            }
            //List<string> row = new List<string>();
           // row.Add(currentPageHtmlString);
            //allRows.Add(row);

            ExcelConverter.WriteExcelFile(reportName, "report1", allRows);

            //// Set response content type
            //Response.AddHeader("Content-Type", "application/xlsx");

            //// Instruct the browser to open the PDF file as an attachment or inline
            //Response.AddHeader("Content-Disposition", String.Format("attachment; filename=GAINReport.pdf; size={0}"));

            //// Write the PDF document buffer to HTTP response
            //Response.BinaryWrite(outPdfBuffer);

            //// End the HTTP response and stop the current page processing
            //Response.End();

        }

        public void CreatePdfButton_Click(object sender, EventArgs e)
        {
           // //// Get the current page HTML string by rendering into a TextWriter object
           // TextWriter outTextWriter = new StringWriter();
           // HtmlTextWriter outHtmlTextWriter = new HtmlTextWriter(outTextWriter);
           // base.Render(outHtmlTextWriter);

           // //// Obtain the current page HTML string
           // string currentPageHtmlString = outTextWriter.ToString();

           // //// Create a HTML to PDF converter object with default settings
           // HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();

           // //// Start reading document from the top of the table
           // string htmlElementSelector = "#pdfmarker";
           // htmlToPdfConverter.RenderedHtmlElementSelector = htmlElementSelector;

           // //// Store the pdf data into a buffer
           // byte[] outPdfBuffer = htmlToPdfConverter.ConvertHtml(currentPageHtmlString, HttpContext.Current.Request.Url.AbsoluteUri);

           // //// Set response content type
           // Response.AddHeader("Content-Type", "application/pdf");

           // //// Instruct the browser to open the PDF file as an attachment or inline
           //Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Partially_Convert_HTML.pdf; size={0}", outPdfBuffer.Length.ToString()));

           // //// Write the PDF document buffer to HTTP response
           // Response.BinaryWrite(outPdfBuffer);

           // //// End the HTTP response and stop the current page processing
           // Response.End();
        }
    }
}