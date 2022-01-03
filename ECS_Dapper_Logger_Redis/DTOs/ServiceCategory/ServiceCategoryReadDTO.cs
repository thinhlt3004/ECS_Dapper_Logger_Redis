using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.ServiceCategory
{
    public class ServiceCategoryReadDTO
    {
        public int Id { get; set; }
        public string CaterogoryName { get; set; }
    }
}
