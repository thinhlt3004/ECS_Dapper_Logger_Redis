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

namespace ECS_Dapper_Logger_Redis.Repositories.Implements
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IDbConnection _dbCtx;
        private readonly IEmailSender _emailSender;
        public AccountRepo(Connection dbCtx, IEmailSender emailSender)
        {
            _dbCtx = dbCtx.ConnectionDB;
            _emailSender = emailSender;
        }

        public async Task<List<Account>> GetAll()
        {
            using (IDbConnection dbCon = _dbCtx)
            {
                string query = @"SELECT * FROM Account";
                dbCon.Open();
                return (List<Account>)await dbCon.QueryAsync<Account>(query);
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
