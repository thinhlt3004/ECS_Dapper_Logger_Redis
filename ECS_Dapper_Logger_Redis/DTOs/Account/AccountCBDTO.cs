﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.Account
{
    public class AccountCBDTO
    {
        public string EmployeeId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirm { get; set; }
        public int RoleId { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmToken { get; set; }
        public string Department { get; set; }
        public string Image { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
