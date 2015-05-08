using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CubeReportingModule.Models
{
    [Table("ReportOptions")]
    public class ReportOption
    {
        [Key]
        [Column(Order = 1)]
        public int ReportId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ReportOptionId { get; set; }
        public string Type { get; set; }
        public string Alias { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        //public List<string> listOptions { get; set; }
        //public string MinValue { get; set; }
        //public string MaxValue { get; set; }

        public string toString()
        {
            string markup = "";

            switch (Type)
            {
                case "customer":
                    markup += @"SqlDataSource ID=""CompanyNameSelect"" runat=""server"" DataSourceMode=""DataReader"" SelectCommand=""SELECT Company_Name FROM Org_Company WHERE Company_Name IS NOT NULL AND Company_Name != '' ORDER BY Company_Name ASC "; 
                    markup += @"ConnectionString=""Data Source=204.174.60.182;Initial Catalog=GainTest;Persist Security Info=True;User ID=Michelle;Password=SRGTChronos3"">";
                    markup += @"</asp:SqlDataSource>";
                    markup += @"<asp:ListBox id=""ClientListBox"" runat=""server"" DataTextField=""Company_Name"" DataSourceID=""CompanyNameSelect"" Height=""200"">";
                    markup += @"</asp:ListBox>";
                    break;

                case "list":
                    markup += @"DropDownList runat=""server"">";

                    //foreach(string listOption in listOptions)
                    //{
                    //    markup += @"<asp:ListItem Enabled=""true"" Selected=""True"" Text=" + "\"" + listOption + "\"" + @" Value=" + "\"" + listOption + "\"" + @" />";
                    //    markup += @"<br>";
                    //}
                    markup += @"</asp:DropDownList>";
                    break;

                case "date":
                    markup += @"Calendar ID=" + "\"" + Name + "\"" + @" runat=""server"">" + "\n";
                    markup += @"<TodayDayStyle />";
                    markup += @"</asp:Calendar>";
                    break;

                case "range":
                    break;

                case "checkbox":
                    markup += @"CheckBox";
                    markup += @"</asp:CheckBox>";
                    break;
            }

            markup += @"<br>";

            return markup;
        }
    }
}