using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Customer;
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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _cusRepo;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerRepo cusRepo, IMapper mapper)
        {
            _cusRepo = cusRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerReadDTO>>> Get()
        {
            return Ok(_mapper.Map<List<CustomerReadDTO>>(await _cusRepo.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerReadDTO>> GetByID([FromRoute] int id)
        {
            return Ok(_mapper.Map<CustomerReadDTO>(await _cusRepo.GetByID(id)));
        }

    }
}
