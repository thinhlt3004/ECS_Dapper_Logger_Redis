using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.ServiceCustomer
{
    public class ServiceCustomerReadDTO
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string ServiceId { get; set; }
        public DateTime? StartDate { get; set; }
        public double? CurrentPrice { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? EmployeeHandle { get; set; }
        public string Product { get; set; }
        public double? ProductPrice { get; set; }
    }
}
