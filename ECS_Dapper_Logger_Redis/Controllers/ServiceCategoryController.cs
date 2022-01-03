using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.ServiceCategory;
using ECS_Dapper_Logger_Redis.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly IServiceCategoryRepo _serCateRepo;
        private readonly IMapper _mapper;


        public ServiceCategoryController(IServiceCategoryRepo serCateRepo, IMapper mapper)
        {
            _serCateRepo = serCateRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceCategoryReadDTO>>> GetAll()
        {
            return Ok(_mapper.Map<List<ServiceCategoryReadDTO>>(await _serCateRepo.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceCategoryReadDTO>> GetByID(int id)
        {
            return Ok(_mapper.Map<ServiceCategoryReadDTO>(await _serCateRepo.GetByID(id)));
        }


    }
}
