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
            string currentDate = DateTime.Now.ToShortDateString();
            string fullPath = String.Format(@"{0}\{1}{2}{3}", path, filename, currentDate, extension);
            FileInfo file = new FileInfo(fullPath);
            //Check to see if there is a preexisting file with the same name
            bool filenameConflict = file.Exists;
            //If there is a conflict create a copy with a numbered suffix
            int number = 1;
            while (filenameConflict == true)
            {
                fullPath = String.Format(@"{0}\{1}{2}({3}){4}", path, filename, currentDate, number, extension);
                file = new FileInfo(fullPath);
                filenameConflict = file.Exists;
                number++;
            }

            //Populate file with column data by row
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

            //Set column names to bold
            worksheet.Row(1).Style.Font.Bold = true;

            //Autofit all columns
            worksheet.Cells.AutoFitColumns(0);

            //Save file and close
            package.Save();
        }
    }
}