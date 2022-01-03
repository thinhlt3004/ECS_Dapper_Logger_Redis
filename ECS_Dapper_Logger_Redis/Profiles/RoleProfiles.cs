using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Role;
using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Profiles
{
    public class RoleProfiles : Profile
    {
        public RoleProfiles()
        {
            CreateMap<Role, RoleReadDTO>();
        }
    }
}
