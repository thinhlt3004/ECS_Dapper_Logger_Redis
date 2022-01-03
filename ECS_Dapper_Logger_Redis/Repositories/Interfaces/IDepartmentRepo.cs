using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Interfaces
{
    public interface IDepartmentRepo
    {
        Task<List<Department>> GetAll();

        Task<Department> GetByID(string id);

        Task<bool> Create(Department d);

        Task<bool> Update(Department d);

        Task<bool> Delete(string id);
    }
}
