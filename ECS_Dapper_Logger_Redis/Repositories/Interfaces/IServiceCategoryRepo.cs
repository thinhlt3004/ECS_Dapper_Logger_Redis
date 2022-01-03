using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Interfaces
{
    public interface IServiceCategoryRepo
    {
        Task<List<ServiceCategory>> GetAll();

        Task<ServiceCategory> GetByID(int id);
    }
}
