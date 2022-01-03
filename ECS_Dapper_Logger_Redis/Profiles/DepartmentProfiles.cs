using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Department;
using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Profiles
{
    public class DepartmentProfiles : Profile
    {
        public DepartmentProfiles()
        {
            CreateMap<DepartmentCreateDTO, Department>();

            CreateMap<Department, DepartmentReadDTO>();
        }
    }
}
