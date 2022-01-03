using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Role;
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
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepo _roleRepo;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepo roleRepo, IMapper mapper)
        {
            _roleRepo = roleRepo;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<RoleReadDTO>>> GetAll()
        {
            return Ok(_mapper.Map<List<RoleReadDTO>>(await _roleRepo.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleReadDTO>> GetByID(int id)
        {
            return Ok(_mapper.Map<RoleReadDTO>(await _roleRepo.GetByID(id)));
        }

    }
}
