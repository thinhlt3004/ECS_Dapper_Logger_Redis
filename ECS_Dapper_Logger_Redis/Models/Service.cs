using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Models
{
    [Table("Services")]
    public class Service
    {
        public Service()
        {
            ServiceCustomers = new HashSet<ServiceCustomer>();
        }
        [ExplicitKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int? ServiceCategoryId { get; set; }
        public string Image { get; set; }

        public virtual ServiceCategory ServiceCategory { get; set; }
        public virtual ICollection<ServiceCustomer> ServiceCustomers { get; set; }
    }
}
