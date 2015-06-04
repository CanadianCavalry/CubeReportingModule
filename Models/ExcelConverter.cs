using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
//using Ctl.Data.Excel;
using OfficeOpenXml;
using System.IO;

namespace CubeReportingModule.Models
{
    public static class ExcelConverter
    {
        public static void WriteExcelFile(string path, string filename, string worksheetName, List<List<string>> allRows)
        {
            string extension = @".xlsx";
            string fullPath = path + filename + extension;
            FileInfo file = new FileInfo(fullPath);
            bool filenameConflict = file.Exists;
            int number = 0;
            while (filenameConflict == true)
            {
                number++;
                fullPath = String.Format("{0}{1}({2}){3}", path, filename, number, extension);
                file = new FileInfo(fullPath);
                filenameConflict = file.Exists;
            }

            ExcelPackage package = new ExcelPackage(file);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetName);
            for (int i = 0; i < allRows.Count; i++)
            {
                List<string> row = allRows.ElementAt(i);
                for(int j = 0; j < row.Count; j++) {
                    string cell = row.ElementAt(j);
                    worksheet.Cells[i+1, j+1].Value = cell.ToString();
                }
            }

            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Cells.AutoFitColumns(0);

            package.Save();
        }

        //public void ConvertToFile()
        //{
        //    Application app = new Microsoft.Office.Interop.Excel.Application();

        //    if (app == null)
        //    {
        //        Debug.Write("Excel is not properly installed!!");
        //        return;
        //    }

        //    Workbook workbook;
        //    Worksheet worksheet;
        //    object misValue = System.Reflection.Missing.Value;

        //    workbook = app.Workbooks.Add(misValue);
        //    worksheet = (Worksheet)workbook.Worksheets.get_Item(1);
        //    worksheet.Cells[1, 1] = "Sheet 1 content";

        //    workbook.SaveAs("i:\\csharp-Excel.xls", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //    workbook.Close(true, misValue, misValue);
        //    app.Quit();

        //    releaseObject(worksheet);
        //    releaseObject(workbook);
        //    releaseObject(app);

        //    Debug.Write("Excel file created , you can find the file d:\\csharp-Excel.xls");
        //}

        //private void releaseObject(object obj)
        //{
        //    try
        //    {
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        //        obj = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        obj = null;
        //        Debug.Write("Unable to release the Object " + ex.ToString());
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}
    }
}