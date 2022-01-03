using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.ServiceCustomer;
using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Profiles
{
    public class ServiceCustomerProfiles : Profile
    {
        public ServiceCustomerProfiles()
        {
            CreateMap<ServiceCustomerCreateDTO, ServiceCustomer>();

            CreateMap<ServiceCustomer, ServiceCustomerReadDTO>();
        }
    }
}
