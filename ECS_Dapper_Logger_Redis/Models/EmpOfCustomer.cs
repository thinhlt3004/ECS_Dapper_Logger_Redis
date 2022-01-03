using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Models
{
    [Table("EmpOfCustomer")]
    public class EmpOfCustomer
    {
        [ExplicitKey]
        public int Id { get; set; }
        public string EmpId { get; set; }
        public int ServiceOfCus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Status { get; set; }

        public virtual Account Emp { get; set; }
        public virtual ServiceCustomer ServiceOfCusNavigation { get; set; }
    }
}
