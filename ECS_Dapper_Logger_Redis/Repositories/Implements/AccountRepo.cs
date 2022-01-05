using Dapper;
using ECS_Dapper_Logger_Redis.DAL;
using ECS_Dapper_Logger_Redis.DTOs.Account;
using ECS_Dapper_Logger_Redis.Models;
using ECS_Dapper_Logger_Redis.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using ECS_Dapper_Logger_Redis.DTOs.MailTemplate;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;

namespace ECS_Dapper_Logger_Redis.Repositories.Implements
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IDbConnection _dbCtx;
        private readonly IEmailSender _emailSender;
        private readonly IDistributedCache _distributedCache;
        public AccountRepo(Connection dbCtx, IEmailSender emailSender, IDistributedCache distributedCache)
        {
            _dbCtx = dbCtx.ConnectionDB;
            _emailSender = emailSender;
            _distributedCache = distributedCache;
        }

        public async Task<List<Account>> GetAll(int pages)
        {
            var getAccounts = await _distributedCache.GetAsync($"get-all-account/{pages}");
            if(getAccounts != null)
            {
                var stringAccounts = Encoding.UTF8.GetString(getAccounts);
                var accountRes = JsonConvert.DeserializeObject<List<Account>>(stringAccounts);
                return accountRes;
            }
            using (IDbConnection dbCon = _dbCtx)
            {
                int prevPage = pages - 1;
                int totalPrev = prevPage * 2;
                string query = @"SELECT * FROM Account ORDER BY EmployeeId ASC OFFSET (@totalPrev) ROWS FETCH NEXT (2) ROWS ONLY";
                dbCon.Open();
                var accountList = (List<Account>)await dbCon.QueryAsync<Account>(query, new
                {
                    totalPrev = totalPrev
                });
                var option = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(5));
                var serializeAccount = JsonConvert.SerializeObject(accountList);
                var byteAccounts = Encoding.UTF8.GetBytes(serializeAccount);
                await  _distributedCache.SetAsync($"get-all-account/{pages}", byteAccounts, option);
                return accountList;
            }
        }

        public async Task<Account> GetByID(string id)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                string query = @"SELECT * FROM Account WHERE EmployeeID = @id";
                dbCon.Open();
                return (Account)await dbCon.QuerySingleOrDefaultAsync<Account>(query, new { id = id });
            }
        }

        public async Task<Account> SignIn(string email, string password)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                string query = @"SELECT * FROM Account WHERE Email = @email";
                dbCon.Open();
                Account account = await dbCon.QuerySingleOrDefaultAsync<Account>(query, new { email = email });
                if (account == null) return null;
                if (account != null && BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
                {
                    return account;
                }
                return null;
            }
        }

        public async Task<bool> SignUp(AccountCreateDTO acc)
        {
            using(IDbConnection dbCon = _dbCtx)
            {
                string query = @"INSERT INTO Account (EmployeeId, UserName, FullName, Email, RoleId, PhoneNumber, Department, Image, EmailConfirm, ConfirmToken, PasswordHash) VALUES (@EmployeeId, @UserName, @FullName, @Email, @RoleId, @PhoneNumber, @Department, @Image, @EmailConfirm, @ConfirmToken, @PasswordHash)";
                dbCon.Open();
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString().Substring(0, 8));
                var token = Guid.NewGuid().ToString();
                var result = await dbCon.ExecuteAsync(query, new
                {
                    EmployeeId = acc.EmployeeId,
                    Username = acc.UserName,
                    FullName = acc.FullName,
                    Email = acc.Email,
                    RoleId = acc.RoleId,
                    PhoneNumber = acc.PhoneNumber,
                    Department = acc.Department,
                    Image = acc.Image,
                    EmailConfirm = false,
                    ConfirmToken = token,
                    PasswordHash = passwordHash
                });
                if(result > 0)
                {
                    var mailCon = new MailContruct
                    {
                        Fullname = acc.FullName,
                        Email = acc.Email,
                        Password = passwordHash,
                        Token = token
                    };
                    await _emailSender.SendEmailAsync(mailCon.Email, "Confirm Account", mailCon.GetTemplate());
                }
                return result > 0;
            }
        }

        public async Task<bool> Update(AccountCreateDTO acc)
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                string sQuery = @"Update Account SET UserName=@UserName, FullName=@FullName,Email=@Email, RoleId=@RoleId,PhoneNumber=@PhoneNumber,Department=@Department,Image=@Image WHERE EmployeeId=@EmployeeId";
                dbCon.Open();
                var result = await dbCon.ExecuteAsync(sQuery, new
                {
                    EmployeeId = acc.EmployeeId,
                    UserName = acc.UserName,
                    FullName = acc.FullName,
                    Email = acc.Email,
                    RoleId = acc.RoleId,
                    PhoneNumber = acc.PhoneNumber,
                    Department = acc.Department,
                    Image = acc.Image
                });
                return result > 0;
            }
        }
    }
}
