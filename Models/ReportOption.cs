using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CubeReportingModule.Models
{
    public class ReportOption
    {
        public int ReportId { get; set; }
        public int ReportOptionId { get; set; }
        public string type { get; set; }
        public string alias { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public List<string> listOptions { get; set; }
        public string minValue { get; set; }
        public string maxValue { get; set; }

        public string toString()
        {
            string markup = @"<asp:";

            switch (type)
            {
                case "list":
                    markup += @"DropDownList runat=""server"">";
                    markup += "/n";

                    foreach(string listOption in listOptions)
                    {
                        markup += @"<asp:ListItem Enabled=""true"" Selected=""True"" Text=" + "\"" + listOption + "\"" + @" Value=" + "\"" + listOption + "\"" + @" />";
                        markup += "/n";
                    }
                    markup += @"</asp:DropDownList>";
                    break;

                case "date":
                    markup += @"Calendar ID=" + "\"" + name + "\"" + @" runat=""server"">" + "\n" + @"<TodayDayStyle />";
                    markup += "/n";
                    markup += @"</asp:Calendar>";
                    break;

                case "range":
                    break;

                case "checkbox":
                    markup += @"CheckBox";
                    markup += @"</asp:CheckBox>";
                    break;
            }

            markup += "/n";

            return markup;
        }
    }
}