using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.Report
{
    public class ReportReadDTO
    {
        public int Id { get; set; }
        public int? ServiceOfCus { get; set; }
        public DateTime? Date { get; set; }
        public int? Count { get; set; }
        public double? TotalPrice { get; set; }
    }
}
