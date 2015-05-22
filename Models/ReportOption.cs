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
        public string ControlType { get; set; }
        public string ColumnName { get; set; }
        public string Label { get; set; }
        public string Condition { get; set; }
        public string Metric { get; set; }
        public string SelectCommand { get; set; }
        public string Id { get; set; }
        public string DataTextField { get; set; }
        public string DataSourceId { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public bool isVisible(string controlType)
        {
            if (ControlType.Equals(controlType))
            {
                return true;
            }

            return false;
        }
    }
}