using ECS_Dapper_Logger_Redis.DTOs.Account;
using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Interfaces
{
    public interface IAccountRepo
    {
        Task<List<Account>> GetAll(int pages);

        Task<Account> GetByID(string id);

        Task<Account> SignIn(string email, string password);

        Task<bool> SignUp(AccountCreateDTO acc);

        Task<bool> Update(AccountCreateDTO acc);
    }
}
