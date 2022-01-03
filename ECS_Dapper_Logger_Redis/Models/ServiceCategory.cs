using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Models
{
    [Table("ServiceCategory")]
    public class ServiceCategory
    {
        public ServiceCategory()
        {
            Services = new HashSet<Service>();
        }
        [ExplicitKey]
        public int Id { get; set; }
        public string CaterogoryName { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
