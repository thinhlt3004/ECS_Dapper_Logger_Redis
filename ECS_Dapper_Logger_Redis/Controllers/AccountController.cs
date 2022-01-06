using AutoMapper;
using ECS_Dapper_Logger_Redis.DTOs.Account;
using ECS_Dapper_Logger_Redis.Models;
using ECS_Dapper_Logger_Redis.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Controllers
{
    //[EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _accRepo;
        private readonly IDistributedCache _distributedCache;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepo accRepo, IDistributedCache distributedCache, ITokenService tokenService, IMapper mapper)
        {
            _accRepo = accRepo;
            _distributedCache = distributedCache;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        //[DisableCors]
        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("pages/{pages}")]
        public async Task<ActionResult> GetAll(int pages)
        {
            return Ok(_mapper.Map<List<AccountReadDTO>>(await _accRepo.GetAll(pages)));
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountReadDTO>> GetById(string id)
        {
            return Ok(_mapper.Map<AccountReadDTO>(await _accRepo.GetByID(id)));
        }


        [HttpPost("sign-in")]
        public async Task<ActionResult<AccountCBDTO>> SignIn(AccountLogin sa)
        {
            //if (!ModelState.IsValid) return BadRequest(sa);
            try
            {
                var acc = await _accRepo.SignIn(sa.email, sa.password);
                if (acc == null) return BadRequest("Email or Password is incorrect");
                var accessToken = _tokenService.GenerateAccessToken(acc.EmployeeId, acc.Email, acc.RoleId == 1 ? "Admin" : "Employee");
                var refreshToken = _tokenService.GenerateRefreshToken();
                var accCB = _mapper.Map<AccountCBDTO>(acc);
                var option = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMonths(1));
                accCB.AccessToken = accessToken;
                accCB.RefreshToken = refreshToken;
                var accStr = JsonConvert.SerializeObject(acc);
                var accByte = Encoding.UTF8.GetBytes(accStr);
                await _distributedCache.SetAsync(accCB.RefreshToken, accByte, option);
                return Ok(accCB);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           


        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet("sign-out/{refreshToken}")]
        public async Task<ActionResult> SignOut(string refreshToken)
        {
            await _distributedCache.RemoveAsync(refreshToken);
            return Ok("Sign Out Successfully");
        }


        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp(AccountCreateDTO acc)
        {
            if (!ModelState.IsValid) return BadRequest(acc);
            var result = await _accRepo.SignUp(acc);
            if(result)
            {
                return Ok();
            }
            return BadRequest("Something went wrong...");
        }


        [Authorize(Roles = "Admin, Employee")]
        [HttpPut]
        public async Task<ActionResult> Update(AccountCreateDTO m)
        {
            if (!ModelState.IsValid) return BadRequest(m);

            var result = await _accRepo.Update(m);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Something went wrong...");
        }

        [HttpGet("IpAdress-Client")]
        public ActionResult GetIpAddress()
        {
            string ipAddress = string.Empty;

            IPAddress ip = Request.HttpContext.Connection.RemoteIpAddress;
            if(ip != null)
            {
                if(ip.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    ip = Dns.GetHostEntry(ip).AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork);
                }
                ipAddress = ip.ToString();
            }
            return Ok(ipAddress);
        }

    }
}
