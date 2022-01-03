using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.Account
{
    public class AccountLogin
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
