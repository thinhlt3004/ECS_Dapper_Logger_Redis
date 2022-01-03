using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Models
{
    [Table("Roles")]
    public class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }
        [ExplicitKey]
        public int Id { get; set; }
        public string Role1 { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
