using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Account;
using ECS_Dapper_Logger_Redis.DTOs.Service;
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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepo _serviceRepo;
        private readonly IMapper _mapper;


        public ServiceController(IServiceRepo serviceRepo, IMapper mapper)
        {
            _serviceRepo = serviceRepo;
            _mapper = mapper;
        }

        [HttpGet("pages/{pages}")]
        public async Task<ActionResult<List<ServiceReadDTO>>> GetAll(int pages)
        {
            return Ok(_mapper.Map<List<ServiceReadDTO>>(await _serviceRepo.GetAll(pages)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceReadDTO>> GetByID(string id)
        {
            return Ok(_mapper.Map<ServiceReadDTO>(await _serviceRepo.GetByID(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Create(ServiceCreateDTO sc)
        {
            if (!ModelState.IsValid) return BadRequest(sc);
            var acc = _mapper.Map<Service>(sc);
            var result = await _serviceRepo.Create(acc);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _serviceRepo.Delete(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(ServiceCreateDTO s)
        {
            var sc = _mapper.Map<Service>(s);
            var result = await _serviceRepo.Update(sc);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
