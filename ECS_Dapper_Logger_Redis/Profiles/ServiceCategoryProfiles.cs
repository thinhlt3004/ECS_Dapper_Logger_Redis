using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.ServiceCategory;
using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Profiles
{
    public class ServiceCategoryProfiles : Profile
    {
        public ServiceCategoryProfiles()
        {
            CreateMap<ServiceCategory, ServiceCategoryReadDTO>();
        }
    }
}
