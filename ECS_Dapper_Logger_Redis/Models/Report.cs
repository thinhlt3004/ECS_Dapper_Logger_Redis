using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Models
{
    [Table("Report")]
    public class Report
    {
        [ExplicitKey]
        public int Id { get; set; }
        public int? ServiceOfCus { get; set; }
        public DateTime? Date { get; set; }
        public int? Count { get; set; }
        public double? TotalPrice { get; set; }

        public virtual ServiceCustomer ServiceOfCusNavigation { get; set; }
    }
}
