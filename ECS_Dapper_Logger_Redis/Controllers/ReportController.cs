using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Report;
using ECS_Dapper_Logger_Redis.Models;
using ECS_Dapper_Logger_Redis.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace ECS_Dapper_Logger_Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepo _rRepo;
        private readonly IMapper _mapper;
        private readonly IGeneratePdf _generatedPdf;

        public ReportController(IReportRepo rRepo, IMapper mapper, IGeneratePdf generatedPdf)
        {
            _rRepo = rRepo;
            _mapper = mapper;
            _generatedPdf = generatedPdf;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReportReadDTO>>> GetAll()
        {
            return Ok(_mapper.Map<List<ReportReadDTO>>(await _rRepo.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ReportReadDTO>>> GetByID(int id)
        {
            return Ok(_mapper.Map<List<ReportReadDTO>>(await _rRepo.GetById(id)));
        }

        [HttpGet("get-report-pdf/{id}")]
        public async Task<IActionResult> GetReportPDF(int id)
        {
            var result = _mapper.Map<List<ReportReadDTO>>(await _rRepo.GetById(id));
            return await _generatedPdf.GetPdf("Views/Report/ReportDetails.cshtml", result);
        }


        [HttpPost]
        public async Task<ActionResult> Create(ReportCreateDTO r)
        {
            if (!ModelState.IsValid) return BadRequest(r);

            var rObj = _mapper.Map<Report>(r);

            var result = await _rRepo.Create(rObj);
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
            var result = await _rRepo.Delete(id);
            if (result)
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest("Fail");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(ReportCreateDTO r)
        {
            if (!ModelState.IsValid) return BadRequest(r);
            var rObj = _mapper.Map<Report>(r);
            var result = await _rRepo.Update(rObj);
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
