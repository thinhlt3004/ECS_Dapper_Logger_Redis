using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Models
{
    [Table("Departments")]
    public class Department
    {
        public Department()
        {
            Accounts = new HashSet<Account>();
        }
        [ExplicitKey]
        public string Id { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
