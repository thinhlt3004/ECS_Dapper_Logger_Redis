using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Report;
using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Profiles
{
    public class ReportProfiles : Profile
    {
        public ReportProfiles()
        {
            CreateMap<ReportCreateDTO, Report>();
            CreateMap<Report, ReportReadDTO>();
        }
    }
}
