using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.EmpOfCustomer
{
    public class EmpOfCustomerReadDTO
    {
        public int Id { get; set; }
        public string EmpId { get; set; }
        public int ServiceOfCus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Status { get; set; }
    }
}
