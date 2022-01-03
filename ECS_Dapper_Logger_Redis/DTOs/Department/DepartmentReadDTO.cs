using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.Department
{
    public class DepartmentReadDTO
    {
        public string Id { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
    }
}
