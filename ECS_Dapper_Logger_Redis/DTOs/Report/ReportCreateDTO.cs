using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.Report
{
    public class ReportCreateDTO
    {
        public int Id { get; set; }
        [Required]
        public int? ServiceOfCus { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public int? Count { get; set; }
        [Required]
        public double? TotalPrice { get; set; }
    }
}
