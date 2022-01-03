using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.EmpOfCustomer;
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
    public class EmpOfCustomerController : ControllerBase
    {
        private readonly IEmpOfCustomerRepo _eofRepo;
        private readonly IMapper _mapper;

        public EmpOfCustomerController(IEmpOfCustomerRepo eofRepo, IMapper mapper)
        {
            _eofRepo = eofRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<EmpOfCustomerReadDTO>>(await _eofRepo.GetAll()));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EmpOfCustomerReadDTO>> GetById(int id)
        {
            return Ok(_mapper.Map<EmpOfCustomerReadDTO>(await _eofRepo.GetByID(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmpOfCustomerCreateDTO sc)
        {
            if (!ModelState.IsValid) return BadRequest(sc);

            var emOfCus = _mapper.Map<EmpOfCustomer>(sc);
            var result = await _eofRepo.Create(emOfCus);
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
            var result = await _eofRepo.Update(id);
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
            var result = await _eofRepo.Delete(id);
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
