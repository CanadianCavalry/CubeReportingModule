using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CubeReportingModule.Models
{
    public class ReportOption
    {
        public string type { get; set; }
        public string label { get; set; }
        public string description { get; set; }
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

                    int min = Convert.ToInt32(minValue);
                    int max = Convert.ToInt32(maxValue);
                    for (int i = min; i <= max; i++)
                    {
                        markup += @"<asp:ListItem Enabled=""true"" Selected=""True"" Text=" + "\"" + description + i + "\"" + @" Value=" + "\"" + description + i + "\"" + @" />";
                        markup += "/n";
                    }
                    markup += @"</asp:DropDownList>";
                    break;

                case "date":
                    markup += @"Calendar ID=" + "\"" + description + "\"" + @" runat=""server"">" + "\n" + @"<TodayDayStyle />";
                    markup += "/n";
                    markup += "</asp:Calendar>";
                    break;
            }

            markup += "/n";

            return markup;
        }
    }
}