using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.ServiceCustomer;
using ECS_Dapper_Logger_Redis.Models;
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
    public class ServiceCustomerController : ControllerBase
    {
        private readonly IServiceCustomerRepo _scRepo;
        private readonly IMapper _mapper;

        public ServiceCustomerController(IServiceCustomerRepo scRepo, IMapper mapper)
        {
            _scRepo = scRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceCustomerReadDTO>>> GetAll()
        {
            return Ok(_mapper.Map<List<ServiceCustomerReadDTO>>(await _scRepo.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByID(int id)
        {
            return Ok(_mapper.Map<ServiceCustomerReadDTO>(await _scRepo.GetByID(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Create(ServiceCustomerCreateDTO sc)
        {
            if (!ModelState.IsValid) return BadRequest(sc);
            var serCus = _mapper.Map<ServiceCustomer>(sc);
            var result = await _scRepo.Create(serCus);
            if (result)
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest("Fail");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _scRepo.Delete(id);
            if (result)
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest("Fail");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id)
        {
            var result = await _scRepo.Update(id);
            if (result)
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest("Fail");
            }

        }
    }
}
