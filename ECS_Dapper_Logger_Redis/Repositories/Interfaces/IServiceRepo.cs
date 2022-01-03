using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Interfaces
{
    public interface IServiceRepo
    {
        Task<List<Service>> GetAll();

        Task<Service> GetByID(string id);

        Task<bool> Create(Service s);

        Task<bool> Delete(string id);

        Task<bool> Update(Service s);
    }
}
