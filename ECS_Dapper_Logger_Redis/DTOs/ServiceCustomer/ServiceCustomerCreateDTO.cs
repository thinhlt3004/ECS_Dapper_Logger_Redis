using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.ServiceCustomer
{
    public class ServiceCustomerCreateDTO
    {
        [Required]
        public int? CustomerId { get; set; }
        [Required]
        public string ServiceId { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public double? CurrentPrice { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        public double? ProductPrice { get; set; }
    }
}
