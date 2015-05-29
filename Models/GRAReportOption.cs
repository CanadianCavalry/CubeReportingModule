﻿using System;
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
        public int ReportId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ReportOptionId { get; set; }
        public string ControlType { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Condition { get; set; }
        public string Metric { get; set; }
        public string SelectCommand { get; set; }
        public string Id { get; set; }
        public string DataTextField { get; set; }
        public string DataSourceId { get; set; }
        public int InitValue { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}