using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Service;
using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Profiles
{
    public class ServiceProfiles : Profile
    {
        public ServiceProfiles()
        {
            CreateMap<ServiceCreateDTO, Service>();
            CreateMap<Service, ServiceReadDTO>();
        }
    }
}
