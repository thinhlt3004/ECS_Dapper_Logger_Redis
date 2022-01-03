using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Interfaces
{
    public interface IRoleRepo
    {
        Task<List<Role>> GetAll();

        Task<Role> GetByID(int id);
    }
}
