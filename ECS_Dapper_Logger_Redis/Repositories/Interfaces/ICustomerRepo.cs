using ECS_Dapper_Logger_Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.Repositories.Interfaces
{
    public interface ICustomerRepo
    {
        Task<List<Customer>> GetAll();
        Task<Customer> GetByID(int id);
    }
}
