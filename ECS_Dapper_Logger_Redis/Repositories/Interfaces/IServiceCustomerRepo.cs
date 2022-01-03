using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Interfaces
{
    public interface IServiceCustomerRepo
    {
        Task<List<ServiceCustomer>> GetAll();

        Task<ServiceCustomer> GetByID(int id);

        Task<bool> Create(ServiceCustomer s);

        Task<bool> Update(int id);

        Task<bool> Delete(int id);

    }
}
