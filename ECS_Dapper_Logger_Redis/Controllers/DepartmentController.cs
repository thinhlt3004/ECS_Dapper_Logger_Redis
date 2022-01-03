using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Department;
using ECS_Dapper_Logger_Redis.Models;
using ECS_Dapper_Logger_Redis.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepo _deRepo;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepo deRepo, IMapper mapper)
        {
            _deRepo = deRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentReadDTO>>> GetAll()
        {
            var depart = await _deRepo.GetAll();
            return Ok(_mapper.Map<List<DepartmentReadDTO>>(depart));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentReadDTO>> GetByID(string id)
        {
            var depart = await _deRepo.GetByID(id);
            return Ok(_mapper.Map<DepartmentReadDTO>(depart));
        }


        [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DepartmentCreateDTO d)
        {
            var depart = _mapper.Map<Department>(d);
            var result = await _deRepo.Create(depart);

            if (result)
            {
                return Ok("Sucess");
            }
            else
            {
                return BadRequest("Fail");
            }
        }
        [Authorize(Roles = "Admin, Employee")]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] DepartmentCreateDTO d)
        {
            var depart = _mapper.Map<Department>(d);
            var result = await _deRepo.Update(depart);

            if (result)
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest("Fail");
            }
        }
        [Authorize(Roles = "Admin, Employee")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _deRepo.Delete(id);
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
