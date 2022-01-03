using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.EmpOfCustomer
{
    public class EmpOfCustomerCreateDTO
    {
        [Required]
        public string EmpId { get; set; }
        [Required]
        public int ServiceOfCus { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
    }
}
