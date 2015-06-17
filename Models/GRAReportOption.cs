using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CubeReportingModule.Models
{
    [Table("GRAReportOptions")]
    public class GRAReportOption
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        public int ReportId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ReportOptionId { get; set; }
        public string ControlType { get; set; }
        [Required]
        public string Name { get; set; }
        public string Label { get; set; }
        [Required]
        public string Condition { get; set; }
        public string SelectCommand { get; set; }
        public string Id { get; set; }
        public string DataTextField { get; set; }
        public string DataSourceId { get; set; }
        public string InitType { get; set; }

        public override string ToString()
        {
            string summary = String.Format("ID: {0}, Type: {1}, Label: {2}, Condition: {3}, Init value: {4}", 
                Id, ControlType, Label, Condition, InitType);
            return summary;
        }
    }
}