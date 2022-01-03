using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.Service
{
    public class ServiceReadDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? ServiceCategoryId { get; set; }
        public string Image { get; set; }
    }
}
