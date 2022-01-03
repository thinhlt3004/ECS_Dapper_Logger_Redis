using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.Department
{
    public class DepartmentCreateDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
