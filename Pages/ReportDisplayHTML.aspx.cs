using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winnovative;

namespace CubeReportingModule.Pages
{
    public partial class ReportDisplayHTML : System.Web.UI.Page
    {
        string reportName = "GainReport.pdf";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CreatePdfButton_Click(object sender, EventArgs e)
        {
            // Get the current page HTML string by rendering into a TextWriter object
            TextWriter outTextWriter = new StringWriter();
            HtmlTextWriter outHtmlTextWriter = new HtmlTextWriter(outTextWriter);
            base.Render(outHtmlTextWriter);

            // Obtain the current page HTML string
            string currentPageHtmlString = outTextWriter.ToString();

            // Create a HTML to PDF converter object with default settings
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();

            // Start reading document from the top of the table
            string htmlElementSelector = "#pdfmarker";
            htmlToPdfConverter.RenderedHtmlElementSelector = htmlElementSelector;

            // Store the pdf data into a buffer
            byte[] outPdfBuffer = htmlToPdfConverter.ConvertHtml(currentPageHtmlString, HttpContext.Current.Request.Url.AbsoluteUri);

            // Set response content type
            Response.AddHeader("Content-Type", "application/pdf");

            // Instruct the browser to open the PDF file as an attachment or inline
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename=Partially_Convert_HTML.pdf; size={0}", outPdfBuffer.Length.ToString()));

            // Write the PDF document buffer to HTTP response
            Response.BinaryWrite(outPdfBuffer);

            // End the HTTP response and stop the current page processing
            Response.End();
        }

    }
}