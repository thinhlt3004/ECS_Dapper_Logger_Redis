using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.Account
{
    public class AccountReadDTO
    {
        public string EmployeeId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Image { get; set; }
    }
}
