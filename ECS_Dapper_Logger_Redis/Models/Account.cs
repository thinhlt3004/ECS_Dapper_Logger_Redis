using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Models
{
    [Table("Account")]
    public class Account
    {
        [ExplicitKey]
        public string EmployeeId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirm { get; set; }
        public int RoleId { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmToken { get; set; }
        public string Department { get; set; }
        public string Image { get; set; }
        public virtual Department DepartmentNavigation { get; set; }
        //[JsonIgnore]
        //[IgnoreDataMember]
        public virtual Role Role { get; set; }
        public virtual ICollection<EmpOfCustomer> EmpOfCustomers { get; set; }
    }
}
